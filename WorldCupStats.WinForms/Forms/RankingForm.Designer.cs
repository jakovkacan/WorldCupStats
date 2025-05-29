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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingForm));
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
			resources.ApplyResources(tcRanking, "tcRanking");
			tcRanking.Name = "tcRanking";
			tcRanking.SelectedIndex = 0;
			// 
			// tpPlayerRanking
			// 
			tpPlayerRanking.Controls.Add(dgvPlayerRanking);
			resources.ApplyResources(tpPlayerRanking, "tpPlayerRanking");
			tpPlayerRanking.Name = "tpPlayerRanking";
			tpPlayerRanking.UseVisualStyleBackColor = true;
			// 
			// dgvPlayerRanking
			// 
			dgvPlayerRanking.AllowUserToAddRows = false;
			dgvPlayerRanking.AllowUserToDeleteRows = false;
			dgvPlayerRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			resources.ApplyResources(dgvPlayerRanking, "dgvPlayerRanking");
			dgvPlayerRanking.Name = "dgvPlayerRanking";
			dgvPlayerRanking.ReadOnly = true;
			// 
			// tpMatchRanking
			// 
			tpMatchRanking.Controls.Add(dgvMatchRanking);
			resources.ApplyResources(tpMatchRanking, "tpMatchRanking");
			tpMatchRanking.Name = "tpMatchRanking";
			tpMatchRanking.UseVisualStyleBackColor = true;
			// 
			// dgvMatchRanking
			// 
			dgvMatchRanking.AllowUserToAddRows = false;
			dgvMatchRanking.AllowUserToDeleteRows = false;
			dgvMatchRanking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			resources.ApplyResources(dgvMatchRanking, "dgvMatchRanking");
			dgvMatchRanking.Name = "dgvMatchRanking";
			dgvMatchRanking.ReadOnly = true;
			// 
			// btnPrint
			// 
			resources.ApplyResources(btnPrint, "btnPrint");
			btnPrint.Name = "btnPrint";
			btnPrint.UseVisualStyleBackColor = true;
			btnPrint.Click += btnPrint_Click;
			// 
			// printDocument
			// 
			printDocument.PrintPage += printDocument_PrintPage;
			// 
			// RankingForm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnPrint);
			Controls.Add(tcRanking);
			Name = "RankingForm";
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