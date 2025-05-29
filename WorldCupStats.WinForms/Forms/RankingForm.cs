using System.Drawing.Printing;
using System.Resources;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WinForms.Utils;

namespace WorldCupStats.WinForms.Forms
{
	public partial class RankingForm : Form
	{
		private readonly Ranking _ranking;
		private readonly ResourceManager _rm;

		public RankingForm(Ranking ranking)
		{
			_ranking = ranking ?? throw new ArgumentNullException(nameof(ranking));
			_rm = new ResourceManager("WorldCupStats.WinForms.Forms.RankingForm", typeof(RankingForm).Assembly);
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
				HeaderText = _rm.GetString("Picture"),
				ImageLayout = DataGridViewImageCellLayout.Zoom
			};
			dgvPlayerRanking.Columns.Add(imgCol);

			// Add other columns as needed
			dgvPlayerRanking.Columns.Add("Name", _rm.GetString("Name"));
			dgvPlayerRanking.Columns.Add("Goals", _rm.GetString("Goals"));
			dgvPlayerRanking.Columns.Add("YellowCards", _rm.GetString("YellowCards"));

			// Populate rows
			foreach (var pr in _ranking.PlayerRanking)
			{
				//var image = Properties.Resources.ResourceManager.GetObject("DefaultImage"); // If in Properties/Resources.resx
				var path = pr.Player.PictureFileName != null
					? FileUtils.GetPicturePath(pr.Player.PictureFileName)
					: null;
				System.Drawing.Image? playerImage = null;

				if (!string.IsNullOrEmpty(pr.Player.PictureFileName) && File.Exists(path))
				{
					playerImage = System.Drawing.Image.FromFile(path);
				}
				else
				{
					playerImage = Properties.Resources.PlaceholderPicture; // Use a default image if no picture is available
				}

				dgvPlayerRanking.Rows.Add(playerImage, pr.Player.Name, pr.GoalsScored, pr.YellowCards);
			}

			dgvMatchRanking.Columns.Clear();
			dgvMatchRanking.AutoGenerateColumns = false;
			dgvMatchRanking.AllowUserToAddRows = false;
			dgvMatchRanking.RowHeadersVisible = false;
			dgvMatchRanking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			dgvMatchRanking.Columns.Add("Venue", _rm.GetString("Venue"));
			dgvMatchRanking.Columns.Add("HomeTeam", _rm.GetString("HomeTeam"));
			dgvMatchRanking.Columns.Add("AwayTeam", _rm.GetString("AwayTeam"));
			dgvMatchRanking.Columns.Add("Attendance", _rm.GetString("Attendance"));

			foreach (var mr in _ranking.MatchRanking)
			{
				dgvMatchRanking.Rows.Add(mr.Venue, mr.HomeTeam.Country, mr.AwayTeam.Country, mr.Attendance);
			}

		}

		// Add a button or menu item to trigger printing
		private void btnPrint_Click(object sender, EventArgs e)
		{
			using var printDialog = new PrintDialog();

			printDialog.Document = printDocument;
			if (printDialog.ShowDialog() != DialogResult.OK) return;

			try
			{
				printDocument.Print();
			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}
		}

		private int _tableIndex = 0;
		// PrintPage event handler
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			switch (_tableIndex)
			{
				case 0:
					// Print the first table here (e.g., DataGridView1)
					PrintTable(dgvPlayerRanking, e.Graphics, "Player Ranking", [100, 150, 50, 50]);

					// Prepare for next page
					_tableIndex = 1;
					e.HasMorePages = true; // Triggers PrintPage again for next table
					break;
				case 1:
					// Print the second table here (e.g., DataGridView2)
					PrintTable(dgvMatchRanking, e.Graphics, "Match Ranking");

					// No more pages to print
					_tableIndex = 0; // Reset for next print job
					e.HasMorePages = false;
					break;
			}
		}
		private void printDocument_EndPrint(object sender, PrintEventArgs e)
		{
			MessageBoxUtils.ShowInfo(_rm.GetString("PrintCompleted"));
		}
		private static void PrintTable(DataGridView table, Graphics g, string? title = null, int[]? colWidths = null)
		{
			const int x = 50; // Start X position
			var y = 50; // Start Y position
			const int rowHeight = 40;
			const int colWidth = 150;

			if (colWidths == null)
			{
				colWidths = new int[table.Columns.Count];
				for (var i = 0; i < table.Columns.Count; i++)
				{
					colWidths[i] = colWidth; // Default width for each column
				}
			}

			// Draw heading title if provided
			if (!string.IsNullOrEmpty(title))
			{
				using var titleFont = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);

				var titleSize = g.MeasureString(title, titleFont);
				g.DrawString(title, titleFont, Brushes.Black, x, y);
				y += (int)titleSize.Height + 20; // Add space after title
			}

			var cellX = x;
			var colIndex = 0;
			foreach (DataGridViewColumn col in table.Columns)
			{
				using var headerFont = new System.Drawing.Font(table.Font, FontStyle.Bold);

				g.DrawRectangle(Pens.Black, cellX, y, colWidths[colIndex], rowHeight);

				var text = col.HeaderText;
				g.DrawString(text, headerFont, Brushes.Black,
					new RectangleF(cellX + 2, y + 2, colWidths[colIndex] - 4, rowHeight - 4));

				cellX += colWidths[colIndex];
				colIndex++;
			}

			y += rowHeight;
			colIndex = 0;

			foreach (DataGridViewRow row in table.Rows)
			{
				cellX = x;
				foreach (DataGridViewColumn col in table.Columns)
				{
					var cell = row.Cells[col.Index];

					// Draw cell border
					g.DrawRectangle(Pens.Black, cellX, y, colWidths[colIndex], rowHeight);

					if (col is DataGridViewImageColumn && cell.Value is System.Drawing.Image img)
					{
						// Calculate aspect-ratio-preserving rectangle
						var destRect = GetAspectRatioFitRectangle(
							img.Width, img.Height,
							cellX + 2, y + 2,
							colWidths[colIndex] - 4, rowHeight - 4
						);
						g.DrawImage(img, destRect);
					}
					else
					{
						// Draw text
						var text = cell.Value?.ToString() ?? "";
						g.DrawString(text, table.Font, Brushes.Black,
							new RectangleF(cellX + 2, y + 2, colWidths[colIndex] - 4, rowHeight - 4));
					}

					cellX += colWidths[colIndex];
					colIndex++;
				}

				y += rowHeight;
				colIndex = 0;
			}
		}
		private static Rectangle GetAspectRatioFitRectangle(int imgWidth, int imgHeight, int destX, int destY, int destWidth, int destHeight)
		{
			var ratio = Math.Min((float)destWidth / imgWidth, (float)destHeight / imgHeight);
			var width = (int)(imgWidth * ratio);
			var height = (int)(imgHeight * ratio);
			var x = destX + (destWidth - width) / 2;
			var y = destY + (destHeight - height) / 2;

			return new Rectangle(x, y, width, height);
		}


	}
}
