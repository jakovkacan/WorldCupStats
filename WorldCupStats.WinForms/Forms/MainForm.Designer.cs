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
			cbTeams = new ComboBox();
			lbFavoriteTeam = new Label();
			lblAllPlayers = new Label();
			lblFavoritePlayers = new Label();
			flpAllPlayers = new FlowLayoutPanel();
			flpFavoritePlayers = new FlowLayoutPanel();
			progressBar = new ProgressBar();
			btnSettings = new Button();
			SuspendLayout();
			// 
			// cbTeams
			// 
			cbTeams.FormattingEnabled = true;
			cbTeams.Location = new Point(131, 12);
			cbTeams.Name = "cbTeams";
			cbTeams.Size = new Size(211, 23);
			cbTeams.TabIndex = 0;
			cbTeams.SelectionChangeCommitted += cbTeams_SelectionChangeCommitted;
			// 
			// lbFavoriteTeam
			// 
			lbFavoriteTeam.AutoSize = true;
			lbFavoriteTeam.Location = new Point(12, 15);
			lbFavoriteTeam.Name = "lbFavoriteTeam";
			lbFavoriteTeam.Size = new Size(81, 15);
			lbFavoriteTeam.TabIndex = 1;
			lbFavoriteTeam.Text = "Favorite Team";
			// 
			// lblAllPlayers
			// 
			lblAllPlayers.AutoSize = true;
			lblAllPlayers.Location = new Point(12, 47);
			lblAllPlayers.Name = "lblAllPlayers";
			lblAllPlayers.Size = new Size(61, 15);
			lblAllPlayers.TabIndex = 3;
			lblAllPlayers.Text = "All Players";
			// 
			// lblFavoritePlayers
			// 
			lblFavoritePlayers.AutoSize = true;
			lblFavoritePlayers.Location = new Point(358, 47);
			lblFavoritePlayers.Name = "lblFavoritePlayers";
			lblFavoritePlayers.Size = new Size(89, 15);
			lblFavoritePlayers.TabIndex = 5;
			lblFavoritePlayers.Text = "Favorite Players";
			// 
			// flpAllPlayers
			// 
			flpAllPlayers.AllowDrop = true;
			flpAllPlayers.AutoScroll = true;
			flpAllPlayers.BorderStyle = BorderStyle.FixedSingle;
			flpAllPlayers.FlowDirection = FlowDirection.TopDown;
			flpAllPlayers.Location = new Point(12, 74);
			flpAllPlayers.Name = "flpAllPlayers";
			flpAllPlayers.Size = new Size(330, 360);
			flpAllPlayers.TabIndex = 6;
			flpAllPlayers.WrapContents = false;
			flpAllPlayers.DragDrop += flpAllPlayers_DragDrop;
			flpAllPlayers.DragEnter += flp_DragEnter;
			// 
			// flpFavoritePlayers
			// 
			flpFavoritePlayers.AllowDrop = true;
			flpFavoritePlayers.AutoScroll = true;
			flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
			flpFavoritePlayers.FlowDirection = FlowDirection.TopDown;
			flpFavoritePlayers.Location = new Point(358, 74);
			flpFavoritePlayers.Name = "flpFavoritePlayers";
			flpFavoritePlayers.Size = new Size(330, 360);
			flpFavoritePlayers.TabIndex = 7;
			flpFavoritePlayers.WrapContents = false;
			flpFavoritePlayers.DragDrop += flpFavoritePlayers_DragDrop;
			flpFavoritePlayers.DragEnter += flp_DragEnter;
			// 
			// progressBar
			// 
			progressBar.Location = new Point(532, 12);
			progressBar.MarqueeAnimationSpeed = 10;
			progressBar.Name = "progressBar";
			progressBar.Size = new Size(156, 23);
			progressBar.Style = ProgressBarStyle.Marquee;
			progressBar.TabIndex = 8;
			progressBar.Visible = false;
			// 
			// btnSettings
			// 
			btnSettings.Location = new Point(12, 440);
			btnSettings.Name = "btnSettings";
			btnSettings.Size = new Size(75, 23);
			btnSettings.TabIndex = 9;
			btnSettings.Text = "Settings";
			btnSettings.UseVisualStyleBackColor = true;
			btnSettings.Click += btnSettings_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(703, 468);
			Controls.Add(btnSettings);
			Controls.Add(progressBar);
			Controls.Add(flpFavoritePlayers);
			Controls.Add(flpAllPlayers);
			Controls.Add(lblFavoritePlayers);
			Controls.Add(lblAllPlayers);
			Controls.Add(lbFavoriteTeam);
			Controls.Add(cbTeams);
			Name = "MainForm";
			Text = "World Cup Stats";
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
	}
}