using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WinForms.Properties;

namespace WorldCupStats.WinForms.Forms
{
	public partial class RankingForm : Form
	{
		private readonly Ranking _ranking;

		public RankingForm(Ranking ranking)
		{
			_ranking = ranking ?? throw new ArgumentNullException(nameof(ranking));
			InitializeComponent();
		}

		private void RankingForm_Load(object sender, EventArgs e)
		{
			dgvPlayerRanking.Columns.Clear();
			dgvPlayerRanking.AutoGenerateColumns = false;
			dgvPlayerRanking.AllowUserToAddRows = false;
			dgvPlayerRanking.RowHeadersVisible = false;
			dgvPlayerRanking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPlayerRanking.RowTemplate.Height = 100;

			// Add image column
			var imgCol = new DataGridViewImageColumn
			{
				Name = "Picture",
				HeaderText = "Picture",
				ImageLayout = DataGridViewImageCellLayout.Zoom
			};
			dgvPlayerRanking.Columns.Add(imgCol);

			// Add other columns as needed
			dgvPlayerRanking.Columns.Add("Name", "Name");
			dgvPlayerRanking.Columns.Add("Goals", "Goals");
			dgvPlayerRanking.Columns.Add("YellowCards", "Yellow Cards");

			// Populate rows
			foreach (var pr in _ranking.PlayerRanking)
			{
				//var image = Properties.Resources.ResourceManager.GetObject("DefaultImage"); // If in Properties/Resources.resx
				var path = pr.Player.PictureFileName != null
					? FileUtils.GetPicturePath(pr.Player.PictureFileName)
					: null;
				Image? playerImage = null;

				if (!string.IsNullOrEmpty(pr.Player.PictureFileName) && File.Exists(path))
				{
					playerImage = Image.FromFile(path);
				}

				dgvPlayerRanking.Rows.Add(playerImage, pr.Player.Name, pr.GoalsScored, pr.YellowCards);
			}

			dgvMatchRanking.Columns.Clear();
			dgvMatchRanking.AutoGenerateColumns = false;
			dgvMatchRanking.AllowUserToAddRows = false;
			dgvMatchRanking.RowHeadersVisible = false;
			dgvMatchRanking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			dgvMatchRanking.Columns.Add("Venue", "Venue");
			dgvMatchRanking.Columns.Add("HomeTeam", "Home Team");
			dgvMatchRanking.Columns.Add("AwayTeam", "Away Team");
			dgvMatchRanking.Columns.Add("Attendance", "Attendance");

			foreach (var mr in _ranking.MatchRanking)
			{
				dgvMatchRanking.Rows.Add(mr.Venue, mr.HomeTeam.Country, mr.AwayTeam.Country, mr.Attendance);
			}

		}

		// Add a button or menu item to trigger printing
		private void btnPrint_Click(object sender, EventArgs e)
		{
			using (PrintDialog printDialog = new PrintDialog())
			{
				printDialog.Document = printDocument;
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					printDocument.Print();
				}
			}
		}

		// PrintPage event handler
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			int startX = 50;
			int startY = 50;
			int rowHeight = 100;
			int imageSize = 80;
			int colSpacing = 10;

			Font font = new Font("Arial", 12);
			Brush brush = Brushes.Black;

			// Print headers
			e.Graphics.DrawString("Picture", font, brush, startX, startY);
			e.Graphics.DrawString("Name", font, brush, startX + imageSize + colSpacing, startY);
			e.Graphics.DrawString("Goals", font, brush, startX + imageSize + 120, startY);
			e.Graphics.DrawString("Yellow Cards", font, brush, startX + imageSize + 220, startY);

			int y = startY + rowHeight;

			foreach (DataGridViewRow row in dgvPlayerRanking.Rows)
			{
				if (row.IsNewRow) continue;

				// Draw image
				if (row.Cells["Picture"].Value is Image img)
				{
					e.Graphics.DrawImage(img, startX, y, imageSize, imageSize);
				}

				// Draw text
				e.Graphics.DrawString(row.Cells["Name"].Value?.ToString() ?? "", font, brush, startX + imageSize + colSpacing, y + (imageSize / 4));
				e.Graphics.DrawString(row.Cells["Goals"].Value?.ToString() ?? "", font, brush, startX + imageSize + 120, y + (imageSize / 4));
				e.Graphics.DrawString(row.Cells["YellowCards"].Value?.ToString() ?? "", font, brush, startX + imageSize + 220, y + (imageSize / 4));

				y += rowHeight;

				// Check for page break
				if (y + rowHeight > e.MarginBounds.Bottom)
				{
					e.HasMorePages = true;
					return;
				}
			}

			e.HasMorePages = false;
		}
	}
}
