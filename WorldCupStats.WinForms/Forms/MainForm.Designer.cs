namespace WorldCupStats.WinForms.Forms
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			cbTeams = new ComboBox();
			lbFavoriteTeam = new Label();
			lblAllPlayers = new Label();
			lblFavoritePlayers = new Label();
			flpAllPlayers = new FlowLayoutPanel();
			flpFavoritePlayers = new FlowLayoutPanel();
			progressBar = new ProgressBar();
			btnSettings = new Button();
			btnRanking = new Button();
			SuspendLayout();
			// 
			// cbTeams
			// 
			resources.ApplyResources(cbTeams, "cbTeams");
			cbTeams.FormattingEnabled = true;
			cbTeams.Name = "cbTeams";
			cbTeams.SelectionChangeCommitted += cbTeams_SelectionChangeCommitted;
			// 
			// lbFavoriteTeam
			// 
			resources.ApplyResources(lbFavoriteTeam, "lbFavoriteTeam");
			lbFavoriteTeam.Name = "lbFavoriteTeam";
			// 
			// lblAllPlayers
			// 
			resources.ApplyResources(lblAllPlayers, "lblAllPlayers");
			lblAllPlayers.Name = "lblAllPlayers";
			// 
			// lblFavoritePlayers
			// 
			resources.ApplyResources(lblFavoritePlayers, "lblFavoritePlayers");
			lblFavoritePlayers.Name = "lblFavoritePlayers";
			// 
			// flpAllPlayers
			// 
			resources.ApplyResources(flpAllPlayers, "flpAllPlayers");
			flpAllPlayers.AllowDrop = true;
			flpAllPlayers.BorderStyle = BorderStyle.FixedSingle;
			flpAllPlayers.Name = "flpAllPlayers";
			flpAllPlayers.DragDrop += flpAllPlayers_DragDrop;
			flpAllPlayers.DragEnter += flp_DragEnter;
			// 
			// flpFavoritePlayers
			// 
			resources.ApplyResources(flpFavoritePlayers, "flpFavoritePlayers");
			flpFavoritePlayers.AllowDrop = true;
			flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
			flpFavoritePlayers.Name = "flpFavoritePlayers";
			flpFavoritePlayers.DragDrop += flpFavoritePlayers_DragDrop;
			flpFavoritePlayers.DragEnter += flp_DragEnter;
			// 
			// progressBar
			// 
			resources.ApplyResources(progressBar, "progressBar");
			progressBar.MarqueeAnimationSpeed = 10;
			progressBar.Name = "progressBar";
			progressBar.Style = ProgressBarStyle.Marquee;
			// 
			// btnSettings
			// 
			resources.ApplyResources(btnSettings, "btnSettings");
			btnSettings.Name = "btnSettings";
			btnSettings.UseVisualStyleBackColor = true;
			btnSettings.Click += btnSettings_Click;
			// 
			// btnRanking
			// 
			resources.ApplyResources(btnRanking, "btnRanking");
			btnRanking.Name = "btnRanking";
			btnRanking.UseVisualStyleBackColor = true;
			btnRanking.Click += btnRanking_Click;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnRanking);
			Controls.Add(btnSettings);
			Controls.Add(progressBar);
			Controls.Add(flpFavoritePlayers);
			Controls.Add(flpAllPlayers);
			Controls.Add(lblFavoritePlayers);
			Controls.Add(lblAllPlayers);
			Controls.Add(lbFavoriteTeam);
			Controls.Add(cbTeams);
			Name = "MainForm";
			FormClosing += MainForm_FormClosing;
			Load += MainForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cbTeams;
		private Label lbFavoriteTeam;
		private Label lblAllPlayers;
		private Label lblFavoritePlayers;
		private FlowLayoutPanel flpAllPlayers;
		private FlowLayoutPanel flpFavoritePlayers;
		private ProgressBar progressBar;
		private Button btnSettings;
		private Button btnRanking;
	}
}