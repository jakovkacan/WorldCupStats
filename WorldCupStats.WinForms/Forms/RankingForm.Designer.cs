namespace WorldCupStats.WinForms.Forms
{
	partial class RankingForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tcRanking = new TabControl();
			tpPlayerRanking = new TabPage();
			dgvPlayerRanking = new DataGridView();
			tpMatchRanking = new TabPage();
			dgvMatchRanking = new DataGridView();
			btnPrint = new Button();
			printDocument = new System.Drawing.Printing.PrintDocument();
			tcRanking.SuspendLayout();
			tpPlayerRanking.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPlayerRanking).BeginInit();
			tpMatchRanking.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvMatchRanking).BeginInit();
			SuspendLayout();
			// 
			// tcRanking
			// 
			tcRanking.Controls.Add(tpPlayerRanking);
			tcRanking.Controls.Add(tpMatchRanking);
			tcRanking.Location = new Point(12, 12);
			tcRanking.Name = "tcRanking";
			tcRanking.SelectedIndex = 0;
			tcRanking.Size = new Size(489, 426);
			tcRanking.TabIndex = 0;
			// 
			// tpPlayerRanking
			// 
			tpPlayerRanking.Controls.Add(dgvPlayerRanking);
			tpPlayerRanking.Location = new Point(4, 24);
			tpPlayerRanking.Name = "tpPlayerRanking";
			tpPlayerRanking.Padding = new Padding(3);
			tpPlayerRanking.Size = new Size(481, 398);
			tpPlayerRanking.TabIndex = 0;
			tpPlayerRanking.Text = "Players";
			tpPlayerRanking.UseVisualStyleBackColor = true;
			// 
			// dgvPlayerRanking
			// 
			dgvPlayerRanking.AllowUserToAddRows = false;
			dgvPlayerRanking.AllowUserToDeleteRows = false;
			dgvPlayerRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPlayerRanking.Dock = DockStyle.Fill;
			dgvPlayerRanking.Location = new Point(3, 3);
			dgvPlayerRanking.Name = "dgvPlayerRanking";
			dgvPlayerRanking.ReadOnly = true;
			dgvPlayerRanking.Size = new Size(475, 392);
			dgvPlayerRanking.TabIndex = 0;
			// 
			// tpMatchRanking
			// 
			tpMatchRanking.Controls.Add(dgvMatchRanking);
			tpMatchRanking.Location = new Point(4, 24);
			tpMatchRanking.Name = "tpMatchRanking";
			tpMatchRanking.Padding = new Padding(3);
			tpMatchRanking.Size = new Size(481, 398);
			tpMatchRanking.TabIndex = 1;
			tpMatchRanking.Text = "Matches";
			tpMatchRanking.UseVisualStyleBackColor = true;
			// 
			// dgvMatchRanking
			// 
			dgvMatchRanking.AllowUserToAddRows = false;
			dgvMatchRanking.AllowUserToDeleteRows = false;
			dgvMatchRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvMatchRanking.Dock = DockStyle.Fill;
			dgvMatchRanking.Location = new Point(3, 3);
			dgvMatchRanking.Name = "dgvMatchRanking";
			dgvMatchRanking.ReadOnly = true;
			dgvMatchRanking.Size = new Size(475, 392);
			dgvMatchRanking.TabIndex = 0;
			// 
			// btnPrint
			// 
			btnPrint.Location = new Point(419, 444);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new Size(75, 23);
			btnPrint.TabIndex = 1;
			btnPrint.Text = "Print";
			btnPrint.UseVisualStyleBackColor = true;
			btnPrint.Click += btnPrint_Click;
			// 
			// printDocument
			// 
			printDocument.PrintPage += printDocument_PrintPage;
			// 
			// RankingForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(517, 476);
			Controls.Add(btnPrint);
			Controls.Add(tcRanking);
			Name = "RankingForm";
			Text = "Ranking";
			Load += RankingForm_Load;
			tcRanking.ResumeLayout(false);
			tpPlayerRanking.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvPlayerRanking).EndInit();
			tpMatchRanking.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvMatchRanking).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private TabControl tcRanking;
		private TabPage tpPlayerRanking;
		private TabPage tpMatchRanking;
		private DataGridView dgvPlayerRanking;
		private DataGridView dgvMatchRanking;
		private Button btnPrint;
		private System.Drawing.Printing.PrintDocument printDocument;
	}
}