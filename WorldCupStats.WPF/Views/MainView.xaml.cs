﻿using System.Windows;
using System.Windows.Controls;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
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
			Title = WPF.Resources.Resources.MainView_Title;
			LoadSettings(); // Apply display mode settings on startup
		}

		private void ShowLoading() => LoadingOverlay.Visibility = Visibility.Visible;
		private void HideLoading() => LoadingOverlay.Visibility = Visibility.Collapsed;

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await LoadTeams();
		}

		private async Task LoadTeams()
		{
			try
			{
				ShowLoading();
				
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
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		//change Team 1 selection
		private async void cbTeam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (cbTeam1.SelectedIndex < 0) return;
				var selectedTeam = _teams[cbTeam1.SelectedIndex];
				if (selectedTeam == _selectedTeam1) return; // No change in selection

				ShowLoading();

				_selectedTeam1 = selectedTeam;
				_settings.SetValue(selectedTeam);
				
				ClearVisualization();

				await LoadOpponentTeams(selectedTeam);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		//change Team 2 selection
		private async void cbTeam2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (cbTeam2.SelectedIndex < 0) return;
				if (_selectedTeam1 == null) return;

				var opponents = (List<Team>)cbTeam2.Tag;
				var selectedTeam = opponents[cbTeam2.SelectedIndex];

				await LoadMatchDetails(selectedTeam);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		private async Task LoadOpponentTeams(Team selectedTeam)
		{
			try
			{
				ShowLoading();

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

				// Load saved opponent team from settings if exists
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
							await LoadMatchDetails(team);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		private async Task LoadMatchDetails(Team team2)
		{
			try
			{
				ShowLoading();

				_selectedTeam2 = team2;
				_settings.SetTeam2(_selectedTeam2);

				// Get match details for selected teams
				_currentMatch = await _repository.GetMatchAsync(
					_selectedTeam1!.FifaCode ?? _selectedTeam1.Code!,
					_selectedTeam2.FifaCode ?? _selectedTeam2.Code!);

				if (_currentMatch == null) return;

				// Update score display
				txtScore.Text = $"{_currentMatch.Team1Goals ?? 0} : {_currentMatch.Team2Goals ?? 0}";
				txtScore.Visibility = Visibility.Visible;
				
				VisualizePlayers(_currentMatch);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		private async Task ShowTeamStatistics(Team team)
		{
			try
			{
				ShowLoading();

				var statistics = await _repository.GetTeamStatistics(team);

				var teamInfoWindow = new TeamInfoView(statistics);
				teamInfoWindow.Owner = this;

				HideLoading();
				teamInfoWindow.ShowDialog();
			}
			catch (Exception ex)
			{
				HideLoading();
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				HideLoading();
			}
		}

		private async void btnTeam1Info_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (cbTeam1.SelectedItem != null)
					await ShowTeamStatistics(_selectedTeam1!);
				else
					MessageBox.Show(
						WPF.Resources.Resources.MainView_NoTeamSelected_Message,
						WPF.Resources.Resources.MainView_NoTeamSelected_Title,
						MessageBoxButton.OK,
						MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private async void btnTeam2Info_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (cbTeam2.SelectedItem != null)
					await ShowTeamStatistics(_selectedTeam2!);
				else
					MessageBox.Show(
						WPF.Resources.Resources.MainView_NoTeamSelected_Message,
						WPF.Resources.Resources.MainView_NoTeamSelected_Title,
						MessageBoxButton.OK,
						MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, WPF.Resources.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private async void btnSettings_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var settingsWindow = new SettingsView(_settings);
				settingsWindow.Owner = this;

				var previousType = _settings.GetValue<ChampionshipType>();
				settingsWindow.ShowDialog();

				if (!settingsWindow.SettingsSaved) return;

				LoadSettings();

				// If championship type changed, reload teams
				var currentType = _settings.GetValue<ChampionshipType>();
				if (currentType == previousType) return;

				await LoadTeams();
				ResetTeamSelections();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ResetTeamSelections()
		{
			cbTeam1.SelectedIndex = -1;
			cbTeam2.SelectedIndex = -1;
			cbTeam2.IsEnabled = false;
			txtScore.Visibility = Visibility.Collapsed;
			PlayerCanvas.Children.Clear();
		}

		private void LoadSettings()
		{
			var displayMode = _settings.GetValue<DisplayMode>();

			if (displayMode == DisplayMode.Fullscreen)
			{
				WindowState = WindowState.Maximized;
				WindowStyle = WindowStyle.None;
			}
			else
			{
				WindowState = WindowState.Normal;
				WindowStyle = WindowStyle.SingleBorderWindow;

				switch (displayMode)
				{
					case DisplayMode.WindowedSmall:
						Width = 800;
						Height = 600;
						break;
					case DisplayMode.WindowedMedium:
						Width = 1000;
						Height = 700;
						break;
					case DisplayMode.WindowedLarge:
						Width = 1200;
						Height = 800;
						break;
					case DisplayMode.Fullscreen:
					default:
						throw new ArgumentOutOfRangeException();
				}

				// Center the window
				Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
				Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
			}
		}

		private void VisualizePlayers(Match match)
		{
			//clear previous players
			ClearVisualization();

			// Team 1
			var team1Players = match.Team1StartingEleven!.ToList();
			var team1Positions = team1Players
				.GroupBy(p => p.Position)
				.ToDictionary(g => g.Key, g => g.ToList());

			// Add goalkeeper
			if (team1Positions.TryGetValue(Position.Goalie, out var goalies) && goalies.Count > 0)
			{
				AddPlayer(new PlayerInfo()
				{
					Name = goalies[0].Name,
					Number = goalies[0].ShirtNumber,
					Position = Position.Goalie,
					PictureFileName = goalies[0].PictureFileName,
					IsCaptain = goalies[0].IsCapitan,
					Goals = goalies[0].GoalsScored,
					YellowCards = goalies[0].YellowCards
				}, 0, 1, false);
			}

			// Add defenders
			if (team1Positions.TryGetValue(Position.Defender, out var defenders))
			{
				for (int i = 0; i < defenders.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = defenders[i].Name,
						Number = defenders[i].ShirtNumber,
						Position = Position.Defender,
						PictureFileName = defenders[i].PictureFileName,
						IsCaptain = defenders[i].IsCapitan,
						Goals = defenders[i].GoalsScored,
						YellowCards = defenders[i].YellowCards
					}, i, defenders.Count, false);
				}
			}

			// Add midfielders
			if (team1Positions.TryGetValue(Position.Midfield, out var midfielders))
			{
				for (int i = 0; i < midfielders.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = midfielders[i].Name,
						Number = midfielders[i].ShirtNumber,
						Position = Position.Midfield,
						PictureFileName = midfielders[i].PictureFileName,
						IsCaptain = midfielders[i].IsCapitan,
						Goals = midfielders[i].GoalsScored,
						YellowCards = midfielders[i].YellowCards
					}, i, midfielders.Count, false);
				}
			}

			// Add forwards
			if (team1Positions.TryGetValue(Position.Forward, out var forwards))
			{
				for (int i = 0; i < forwards.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = forwards[i].Name,
						Number = forwards[i].ShirtNumber,
						Position = Position.Forward,
						PictureFileName = forwards[i].PictureFileName,
						IsCaptain = forwards[i].IsCapitan,
						Goals = forwards[i].GoalsScored,
						YellowCards = forwards[i].YellowCards
					}, i, forwards.Count, false);
				}
			}

			// Team 2
			var team2Players = match.Team2StartingEleven!.ToList();
			var team2Positions = team2Players
				.GroupBy(p => p.Position)
				.ToDictionary(g => g.Key, g => g.ToList());

			// Add goalkeeper
			if (team2Positions.TryGetValue(Position.Goalie, out var awayGoalies) && awayGoalies.Count > 0)
			{
				AddPlayer(new PlayerInfo()
				{
					Name = awayGoalies[0].Name,
					Number = awayGoalies[0].ShirtNumber,
					Position = Position.Goalie,
					PictureFileName = awayGoalies[0].PictureFileName,
					IsCaptain = awayGoalies[0].IsCapitan,
					Goals = awayGoalies[0].GoalsScored,
					YellowCards = awayGoalies[0].YellowCards
				}, 0, 1, true);
			}

			// Add defenders
			if (team2Positions.TryGetValue(Position.Defender, out var awayDefenders))
			{
				for (int i = 0; i < awayDefenders.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = awayDefenders[i].Name,
						Number = awayDefenders[i].ShirtNumber,
						Position = Position.Defender,
						PictureFileName = awayDefenders[i].PictureFileName,
						IsCaptain = awayDefenders[i].IsCapitan,
						Goals = awayDefenders[i].GoalsScored,
						YellowCards = awayDefenders[i].YellowCards
					}, i, awayDefenders.Count, true);
				}
			}

			// Add midfielders
			if (team2Positions.TryGetValue(Position.Midfield, out var awayMidfielders))
			{
				for (int i = 0; i < awayMidfielders.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = awayMidfielders[i].Name,
						Number = awayMidfielders[i].ShirtNumber,
						Position = Position.Midfield,
						PictureFileName = awayMidfielders[i].PictureFileName,
						IsCaptain = awayMidfielders[i].IsCapitan,
						Goals = awayMidfielders[i].GoalsScored,
						YellowCards = awayMidfielders[i].YellowCards
					}, i, awayMidfielders.Count, true);
				}
			}

			// Add forwards
			if (team2Positions.TryGetValue(Position.Forward, out var awayForwards))
			{
				for (int i = 0; i < awayForwards.Count; i++)
				{
					AddPlayer(new PlayerInfo()
					{
						Name = awayForwards[i].Name,
						Number = awayForwards[i].ShirtNumber,
						Position = Position.Forward,
						PictureFileName = awayForwards[i].PictureFileName,
						IsCaptain = awayForwards[i].IsCapitan,
						Goals = awayForwards[i].GoalsScored,
						YellowCards = awayForwards[i].YellowCards
					}, i, awayForwards.Count, true);
				}
			}
		}

		private void ClearVisualization() => PlayerCanvas.Children.Clear();

		private void AddPlayer(PlayerInfo player, int positionIndex, int totalPlayersInPosition, bool isOpponent)
		{
			var control = new PlayerDressControl
			{
				PlayerName = player.Name,
				ShirtNumber = player.Number,
				Position = player.Position,
				PictureFileName = player.PictureFileName,
				IsOpponent = isOpponent,
				Tag = player // Store the full player info in the Tag property
			};

			var position = PositionHelper.GetPositionOnField(player.Position, positionIndex, totalPlayersInPosition, isOpponent);

			// Center the control by subtracting half its width
			Canvas.SetLeft(control, 1920 * position.X - 100);
			Canvas.SetTop(control, 1226 * position.Y - 60);

			PlayerCanvas.Children.Add(control);
		}
	}
}