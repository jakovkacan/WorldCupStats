using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WPF.Controls;
using WorldCupStats.WPF.Helpers;

namespace WorldCupStats.WPF.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainView : Window
	{
		private readonly IDataRepository _repository;
		private readonly ISettingsRepository _settings;
		private IList<Team> _teams;
		private Team? _selectedTeam1;
		private Team? _selectedTeam2;
		private Match? _currentMatch;

		public MainView(IDataRepository repository, ISettingsRepository settings)
		{
			_repository = repository;
			_settings = settings;
			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				// Get all teams
				var result = await _repository.GetAllTeamsAsync();
				_teams = result.ToList();

				// Populate team 1 combobox
				cbTeam1.ItemsSource = _teams.Select(t => $"{t.Country} ({t.FifaCode ?? t.Code})");

				// Load favorite team from settings if exists
				var savedFavoriteTeam = _settings.GetValue<Team>();
				if (savedFavoriteTeam != null)
				{
					var team = _teams.FirstOrDefault(t => t.Id == savedFavoriteTeam.Id);
					_selectedTeam1 = team;
					if (team != null)
					{
						var index = _teams.IndexOf(team);
						if (index >= 0)
						{
							cbTeam1.SelectedIndex = index;
							await LoadOpponentTeams(team);
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//change Team 1 selection
		private async void cbTeam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbTeam1.SelectedIndex < 0) return;
			var selectedTeam = _teams[cbTeam1.SelectedIndex];
			if (selectedTeam == _selectedTeam1) return; // No change in selection

			try
			{
				_selectedTeam1 = selectedTeam;
				_settings.SetValue(selectedTeam);
				ClearVisualization();
				await LoadOpponentTeams(selectedTeam);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		//change Team 2 selection
		private async void cbTeam2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbTeam2.SelectedIndex < 0) return;
			if (_selectedTeam1 == null) return;

			var opponents = (List<Team>)cbTeam2.Tag;
			var selectedTeam = opponents[cbTeam2.SelectedIndex];

			try
			{
				_selectedTeam2 = selectedTeam;
				_settings.SetTeam2(_selectedTeam2);
				_currentMatch = await _repository.GetMatchAsync(
					_selectedTeam1.FifaCode ?? _selectedTeam1.Code!,
					_selectedTeam2.FifaCode ?? _selectedTeam2.Code!);

				if (_currentMatch != null)
				{
					txtScore.Text = $"{_currentMatch.Team1Goals ?? 0} : {_currentMatch.Team2Goals ?? 0}";
					txtScore.Visibility = Visibility.Visible;
					VisualizePlayers(_currentMatch);
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private async Task LoadOpponentTeams(Team selectedTeam)
		{
			try
			{
				// Clear previous selection and disable combobox
				cbTeam2.SelectedIndex = -1;
				cbTeam2.ItemsSource = null;
				cbTeam2.IsEnabled = false;
				txtScore.Visibility = Visibility.Collapsed;

				// Get opponent teams
				var opponents = (await _repository.GetAllOpponentTeamsAsync(selectedTeam.FifaCode ?? selectedTeam.Code!)).ToList();
				
				// Store the opponents list for later use
				cbTeam2.Tag = opponents;
				
				// Populate team 2 combobox
				cbTeam2.ItemsSource = opponents.Select(t => $"{t.Country} ({t.FifaCode ?? t.Code})");
				cbTeam2.IsEnabled = true;

				var savedOpponentTeam = _settings.GetTeam2();
				if (savedOpponentTeam != null)
				{
					var team = opponents.FirstOrDefault(t => t.Id == savedOpponentTeam.Id);
					if (team != null)
					{
						var index = opponents.IndexOf(team);
						if (index >= 0)
						{
							cbTeam2.SelectedIndex = index;
							cbTeam2_SelectionChanged(cbTeam2, null!); // Load match details
						}
					}
				}

			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnTeam1Info_Click(object sender, RoutedEventArgs e)
		{
			if (cbTeam1.SelectedItem != null)
			{
				var statistics = _repository.GetTeamStatistics(_selectedTeam1!).Result;
				var teamInfoWindow = new TeamInfoView(statistics);
				teamInfoWindow.Owner = this;
				teamInfoWindow.ShowDialog();
			}
			else
			{
				MessageBox.Show("Please select a team first.", "No Team Selected", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void btnTeam2Info_Click(object sender, RoutedEventArgs e)
		{
			if (cbTeam2.SelectedItem != null)
			{
				var statistics = _repository.GetTeamStatistics(_selectedTeam2!).Result;
				var teamInfoWindow = new TeamInfoView(statistics);
				teamInfoWindow.Owner = this;
				teamInfoWindow.ShowDialog();
			}
			else
			{
				MessageBox.Show("Please select a team first.", "No Team Selected", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void VisualizePlayers(Match match)
		{
			//clear previous players
			ClearVisualization();

			var team1Players = match.Team1StartingEleven!.ToList();
			var team1Positions = team1Players
				.GroupBy(p => p.Position)
				.ToDictionary(g => g.Key, g => g.ToList());

			// Add goalkeeper
			if (team1Positions.TryGetValue(Position.Goalie, out var goalies) && goalies.Count > 0)
			{
				AddHomePlayer(new PlayerInfo()
				{
					Name = goalies[0].Name,
					Number = goalies[0].ShirtNumber,
					Position = Position.Goalie,
					PictureFileName = goalies[0].PictureFileName
				}, 0, 1);
			}

			// Add defenders
			if (team1Positions.TryGetValue(Position.Defender, out var defenders))
			{
				for (int i = 0; i < defenders.Count; i++)
				{
					AddHomePlayer(new PlayerInfo()
					{
						Name = defenders[i].Name,
						Number = defenders[i].ShirtNumber,
						Position = Position.Defender,
						PictureFileName = defenders[i].PictureFileName
					}, i, defenders.Count);
				}
			}

			// Add midfielders
			if (team1Positions.TryGetValue(Position.Midfield, out var midfielders))
			{
				for (int i = 0; i < midfielders.Count; i++)
				{
					AddHomePlayer(new PlayerInfo()
					{
						Name = midfielders[i].Name,
						Number = midfielders[i].ShirtNumber,
						Position = Position.Midfield,
						PictureFileName = midfielders[i].PictureFileName
					}, i, midfielders.Count);
				}
			}

			// Add forwards
			if (team1Positions.TryGetValue(Position.Forward, out var forwards))
			{
				for (int i = 0; i < forwards.Count; i++)
				{
					AddHomePlayer(new PlayerInfo()
					{
						Name = forwards[i].Name,
						Number = forwards[i].ShirtNumber,
						Position = Position.Forward,
						PictureFileName = forwards[i].PictureFileName
					}, i, forwards.Count);
				}
			}


			var team2Players = match.Team2StartingEleven!.ToList();
			var team2Positions = team2Players
				.GroupBy(p => p.Position)
				.ToDictionary(g => g.Key, g => g.ToList());

			// Add goalkeeper
			if (team2Positions.TryGetValue(Position.Goalie, out var awayGoalies) && awayGoalies.Count > 0)
			{
				AddAwayPlayer(new PlayerInfo()
				{
					Name = awayGoalies[0].Name,
					Number = awayGoalies[0].ShirtNumber,
					Position = Position.Goalie,
					PictureFileName = awayGoalies[0].PictureFileName
				}, 0, 1);
			}

			// Add defenders
			if (team2Positions.TryGetValue(Position.Defender, out var awayDefenders))
			{
				for (int i = 0; i < awayDefenders.Count; i++)
				{
					AddAwayPlayer(new PlayerInfo()
					{
						Name = awayDefenders[i].Name,
						Number = awayDefenders[i].ShirtNumber,
						Position = Position.Defender,
						PictureFileName = awayDefenders[i].PictureFileName
					}, i, awayDefenders.Count);
				}
			}

			// Add midfielders
			if (team2Positions.TryGetValue(Position.Midfield, out var awayMidfielders))
			{
				for (int i = 0; i < awayMidfielders.Count; i++)
				{
					AddAwayPlayer(new PlayerInfo()
					{
						Name = awayMidfielders[i].Name,
						Number = awayMidfielders[i].ShirtNumber,
						Position = Position.Midfield,
						PictureFileName = awayMidfielders[i].PictureFileName
					}, i, awayMidfielders.Count);
				}
			}

			// Add forwards
			if (team2Positions.TryGetValue(Position.Forward, out var awayForwards))
			{
				for (int i = 0; i < awayForwards.Count; i++)
				{
					AddAwayPlayer(new PlayerInfo()
					{
						Name = awayForwards[i].Name,
						Number = awayForwards[i].ShirtNumber,
						Position = Position.Forward,
						PictureFileName = awayForwards[i].PictureFileName
					}, i, awayForwards.Count);
				}
			}

		}

		private void ClearVisualization() => PlayerCanvas.Children.Clear();

		private void AddHomePlayer(PlayerInfo player, int positionIndex, int totalPlayersInPosition)
		{
			var control = new PlayerDressControl
			{
				PlayerName = player.Name,
				ShirtNumber = player.Number,
				Position = player.Position,
				PictureFileName = player.PictureFileName
			};

			Point position = PositionHelper.GetPositionOnField(player.Position, positionIndex, totalPlayersInPosition);
			
			// Center the control by subtracting half its width (200/2 = 100)
			Canvas.SetLeft(control, 1920 * position.X - 100);
			Canvas.SetTop(control, 1226 * position.Y - 60);

			PlayerCanvas.Children.Add(control);
		}

		private void AddAwayPlayer(PlayerInfo player, int positionIndex, int totalPlayersInPosition)
		{
			var control = new OpponentPlayerDressControl
			{
				PlayerName = player.Name,
				ShirtNumber = player.Number,
				Position = player.Position,
				PictureFileName = player.PictureFileName
			};

			Point position = OpponentPositionHelper.GetPositionOnField(player.Position, positionIndex, totalPlayersInPosition);
			
			// Center the control by subtracting half its width (200/2 = 100)
			Canvas.SetLeft(control, 1920 * position.X - 100);
			Canvas.SetTop(control, 1226 * position.Y - 60);

			PlayerCanvas.Children.Add(control);
		}
	}

	public class PlayerInfo
	{
		public string Name { get; set; }
		public int Number { get; set; }
		public Position Position { get; set; }
		public string? PictureFileName { get; set; }
	}
}