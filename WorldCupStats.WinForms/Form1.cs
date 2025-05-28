using System.Text;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.WinForms.Forms;

namespace WorldCupStats.WinForms
{
	public partial class Form1 : Form
	{
		private readonly IDataRepository _repository;
		private readonly ISettingsRepository _settings;
		public Form1(IDataRepository repository, ISettingsRepository settings)
		{
			_repository = repository;
			_settings = settings;
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			IEnumerable<Team> response;
			try
			{
				progressBar1.Visible = true;
				response = await _repository.GetAllTeamsAsync();
				await Task.Delay(1000);
				if (response != null && response.Any())
				{
					var teams = string.Join(Environment.NewLine,
						response.Select(t => $"{t.Country} ({t.FifaCode}) {t.GroupId} {t.GroupLetter} {t.Id}"));
					MessageBox.Show(teams, "Teams", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("No teams found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			}
			finally
			{
				progressBar1.Visible = false;
			}
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var response = _repository.GetAllMatchesAsync().Result;
			if (response != null && response.Any())
			{
				var matches = string.Join(Environment.NewLine, response.Select(t => $"{t.Venue} ({t.Attendance}) {t.DateTime} {t.Weather.Description} {t.FifaId} {t.Officials.First()}"));

				var ss = new StringBuilder();
				foreach (var match in response)
				{
					ss.AppendLine(match.ToString());
				}
				//MessageBox.Show(matches, "Teams", MessageBoxButtons.OK, MessageBoxIcon.Information);
				richTextBox1.Text = ss.ToString();
			}
			else
			{
				MessageBox.Show("No teams found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			//open SettingsForm
			var settingsForm = new SettingsForm(_settings);
			settingsForm.ShowDialog(this);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			using var openFileDialog = new OpenFileDialog
			{
				Title = "Select an Image",
				Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string selectedFilePath = openFileDialog.FileName;
				// Use the selectedFilePath as needed, e.g., display or save it
				MessageBox.Show($"Selected file: {selectedFilePath}", "Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
				var playerName = textBox1.Text;
				try
				{
					_settings.SetPlayerPicture(playerName, selectedFilePath);
					MessageBox.Show($"Picture for {playerName} saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error saving picture for {playerName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					throw;
				}
			}
		}
	}
}
