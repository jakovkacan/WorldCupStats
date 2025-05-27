using System.Text;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WinForms
{
	public partial class Form1 : Form
	{
		private readonly IDataRepository _repository;
		public Form1(IDataRepository repository)
		{
			_repository = repository;
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{

			var response = _repository.GetAllTeamsAsync(ChampionshipType.Men).Result;
			if (response != null && response.Any())
			{
				var teams = string.Join(Environment.NewLine, response.Select(t => $"{t.Country} ({t.FifaCode}) {t.GroupId} {t.GroupLetter} {t.Id}"));
				MessageBox.Show(teams, "Teams", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("No teams found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var response = _repository.GetAllMatchesAsync(ChampionshipType.Men, "ENG").Result;
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
	}
}
