using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.WinForms.Controls;
using WorldCupStats.WinForms.Utils;

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
			progressBar.Visible = true;
			flpAllPlayers.Controls.Clear();
			flpFavoritePlayers.Controls.Clear();
			cbTeams.DataSource = null;

			try
			{
				var result = await _repository.GetAllTeamsAsync();
				_teams = result.ToList();
				cbTeams.DataSource = _teams.Select(
						t => $"{t.Country} ({t.FifaCode ?? t.Code})")
					.ToList();
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


		private void MainForm_Load(object sender, EventArgs e)
		{
			Initialize();
		}

		private async void cbTeams_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				progressBar.Visible = true;
				var selectedTeam = _teams[cbTeams.SelectedIndex];
				_settings.SetValue(selectedTeam);

				_players = await _repository.GetAllPlayersAsync(selectedTeam.FifaCode ?? selectedTeam.Code!);

				flpAllPlayers.Controls.Clear();
				foreach (var player in _players)
				{
					var playerControl = new PlayerControl(player);
					flpAllPlayers.Controls.Add(playerControl);
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

		private void btnSettings_Click(object sender, EventArgs e)
		{
			var settingsForm = new SettingsForm(_settings);
			settingsForm.ShowDialog(this);
			if (settingsForm.SettingsSaved)
				Initialize();
		}
	}
}
