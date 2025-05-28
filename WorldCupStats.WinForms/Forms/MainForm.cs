using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WinForms.Controls;
using WorldCupStats.WinForms.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorldCupStats.WinForms.Forms
{
	public partial class MainForm : Form
	{
		private readonly IDataRepository _repository;
		private readonly ISettingsRepository _settings;
		public MainForm(IDataRepository repository, ISettingsRepository settings)
		{
			_repository = repository;
			_settings = settings;
			InitializeComponent();
		}

		private IList<Team> _teams;
		private IEnumerable<Player> _players;

		private async void Initialize()
		{
			try
			{
				ClearForm();
				progressBar.Visible = true;

				var result = await _repository.GetAllTeamsAsync();
				_teams = result.ToList();
				cbTeams.DataSource = _teams.Select(
						t => $"{t.Country} ({t.FifaCode ?? t.Code})")
					.ToList();

				// Load favorite team from settings
				var savedFavoriteTeam = _settings.GetValue<Team>();

				if (savedFavoriteTeam != null)
				{
					var index = _teams.IndexOf(_teams.First(t => t.Id == savedFavoriteTeam.Id));
					if (index < 0) return;

					cbTeams.SelectedIndex = index;
					TeamChanged();
				}
				else
				{
					cbTeams.SelectedIndex = 0;
				}

			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}
			finally
			{
				progressBar.Visible = false;
			}
		}

		private async void TeamChanged()
		{
			try
			{
				ClearForm();
				progressBar.Visible = true;

				var selectedTeam = _teams[cbTeams.SelectedIndex];
				_settings.SetValue(selectedTeam);

				_players = await _repository.GetAllPlayersAsync(selectedTeam.FifaCode ?? selectedTeam.Code!);

				DrawPlayers();
			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}
			finally
			{
				progressBar.Visible = false;
			}
		}

		private void DrawPlayers()
		{
			foreach (var player in _players)
			{
				var playerControl = new PlayerControl(player);
				playerControl.AddToFavoritesClicked += PlayerControl_AddToFavoritesClicked;
				playerControl.RemovedFromFavoritesClicked += PlayerControl_RemovedFromFavoritesClicked;
				playerControl.SetPlayerPictureClicked += PlayerControl_SetPlayerPictureClicked;
				playerControl.RemovePlayerPictureClicked += PlayerControl_RemovePlayerPictureClicked;

				if (player.IsFavorite)
					flpFavoritePlayers.Controls.Add(playerControl);
				else
					flpAllPlayers.Controls.Add(playerControl);
			}
		}

		private void ClearForm()
		{
			flpAllPlayers.Controls.Clear();
			flpFavoritePlayers.Controls.Clear();
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			Initialize();
		}

		private void cbTeams_SelectionChangeCommitted(object sender, EventArgs e)
		{
			TeamChanged();
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			var settingsForm = new SettingsForm(_settings);
			settingsForm.ShowDialog(this);

			if (settingsForm.SettingsSaved)
				Initialize();
		}
		private void PlayerControl_AddToFavoritesClicked(object? sender, EventArgs e)
		{
			if (e is PlayerControl.PlayerEventArgs playerEventArgs)
				_settings.AddToFavorites(playerEventArgs.Player);

			ClearForm();
			DrawPlayers();
		}

		private void PlayerControl_RemovedFromFavoritesClicked(object? sender, EventArgs e)
		{
			if (e is PlayerControl.PlayerEventArgs playerEventArgs)
				_settings.RemoveFromFavorites(playerEventArgs.Player);

			ClearForm();
			DrawPlayers();
		}

		private void PlayerControl_SetPlayerPictureClicked(object? sender, EventArgs e)
		{
			if (e is PlayerControl.PlayerEventArgs playerEventArgs)
			{
				var playerName = playerEventArgs.Player.Name;

				using var openFileDialog = new OpenFileDialog
				{
					Title = "Select an Image",
					Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
				};

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					var selectedFilePath = openFileDialog.FileName;

					try
					{
						var filename = _settings.SetPlayerPicture(playerName, selectedFilePath);
						_players.First(p => p.Name == playerName)
							.PictureFileName = filename;
					}
					catch (Exception ex)
					{
						MessageBoxUtils.ShowError($"Error setting picture for {playerName}: {ex.Message}");
					}
				}
			}

			ClearForm();
			DrawPlayers();
		}

		private void PlayerControl_RemovePlayerPictureClicked(object? sender, EventArgs e)
		{
			if (e is PlayerControl.PlayerEventArgs playerEventArgs)
			{
				var playerName = playerEventArgs.Player.Name;
				try
				{
					_settings.RemovePlayerPicture(playerName);
					_players.First(p => p.Name == playerName).PictureFileName = null;
				}
				catch (Exception ex)
				{
					MessageBoxUtils.ShowError($"Error removing picture for {playerName}: {ex.Message}");
				}
			}

			ClearForm();
			DrawPlayers();
		}

	}
}
