using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

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

		private async void cbTeam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbTeam1.SelectedIndex < 0) return;
			var selectedTeam = _teams[cbTeam1.SelectedIndex];
			if (selectedTeam == _selectedTeam1) return; // No change in selection

			try
			{
				_selectedTeam1 = selectedTeam;
				_settings.SetValue(selectedTeam);
				await LoadOpponentTeams(selectedTeam);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

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
	}
}