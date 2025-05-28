namespace WorldCupStats.WinForms.Controls
{
	partial class PlayerControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
			pbPlayerPicture = new PictureBox();
			lblPlayerName = new Label();
			lblPlayerNumber = new Label();
			lblPlayerPosition = new Label();
			pbFavorite = new PictureBox();
			pbCapitan = new PictureBox();
			toolTip = new ToolTip(components);
			cmsFavorite = new ContextMenuStrip(components);
			cmsOptionAdd = new ToolStripMenuItem();
			cmsOptionRemove = new ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)pbPlayerPicture).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbFavorite).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbCapitan).BeginInit();
			cmsFavorite.SuspendLayout();
			SuspendLayout();
			// 
			// pbPlayerPicture
			// 
			pbPlayerPicture.Anchor = AnchorStyles.Left;
			pbPlayerPicture.Image = (Image)resources.GetObject("pbPlayerPicture.Image");
			pbPlayerPicture.Location = new Point(8, 7);
			pbPlayerPicture.Name = "pbPlayerPicture";
			pbPlayerPicture.Size = new Size(85, 85);
			pbPlayerPicture.SizeMode = PictureBoxSizeMode.Zoom;
			pbPlayerPicture.TabIndex = 0;
			pbPlayerPicture.TabStop = false;
			// 
			// lblPlayerName
			// 
			lblPlayerName.Anchor = AnchorStyles.Left;
			lblPlayerName.AutoSize = true;
			lblPlayerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblPlayerName.Location = new Point(105, 15);
			lblPlayerName.Name = "lblPlayerName";
			lblPlayerName.Size = new Size(49, 19);
			lblPlayerName.TabIndex = 1;
			lblPlayerName.Text = "Name";
			lblPlayerName.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblPlayerNumber
			// 
			lblPlayerNumber.Anchor = AnchorStyles.Left;
			lblPlayerNumber.AutoSize = true;
			lblPlayerNumber.Location = new Point(105, 55);
			lblPlayerNumber.Name = "lblPlayerNumber";
			lblPlayerNumber.Size = new Size(76, 15);
			lblPlayerNumber.TabIndex = 2;
			lblPlayerNumber.Text = "Shirt number";
			lblPlayerNumber.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblPlayerPosition
			// 
			lblPlayerPosition.Anchor = AnchorStyles.Left;
			lblPlayerPosition.AutoSize = true;
			lblPlayerPosition.Location = new Point(105, 38);
			lblPlayerPosition.Name = "lblPlayerPosition";
			lblPlayerPosition.Size = new Size(50, 15);
			lblPlayerPosition.TabIndex = 3;
			lblPlayerPosition.Text = "Position";
			lblPlayerPosition.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// pbFavorite
			// 
			pbFavorite.Image = (Image)resources.GetObject("pbFavorite.Image");
			pbFavorite.Location = new Point(267, 7);
			pbFavorite.Name = "pbFavorite";
			pbFavorite.Size = new Size(32, 32);
			pbFavorite.SizeMode = PictureBoxSizeMode.Zoom;
			pbFavorite.TabIndex = 4;
			pbFavorite.TabStop = false;
			pbFavorite.Visible = false;
			// 
			// pbCapitan
			// 
			pbCapitan.Image = (Image)resources.GetObject("pbCapitan.Image");
			pbCapitan.Location = new Point(267, 60);
			pbCapitan.Name = "pbCapitan";
			pbCapitan.Size = new Size(32, 32);
			pbCapitan.SizeMode = PictureBoxSizeMode.Zoom;
			pbCapitan.TabIndex = 5;
			pbCapitan.TabStop = false;
			pbCapitan.Visible = false;
			// 
			// cmsFavorite
			// 
			cmsFavorite.Items.AddRange(new ToolStripItem[] { cmsOptionAdd, cmsOptionRemove });
			cmsFavorite.Name = "cmsFavorite";
			cmsFavorite.Size = new Size(197, 48);
			// 
			// cmsOptionAdd
			// 
			cmsOptionAdd.Name = "cmsOptionAdd";
			cmsOptionAdd.Size = new Size(196, 22);
			cmsOptionAdd.Text = "Add to Favorites";
			// 
			// cmsOptionRemove
			// 
			cmsOptionRemove.Name = "cmsOptionRemove";
			cmsOptionRemove.Size = new Size(196, 22);
			cmsOptionRemove.Text = "Remove from Favorites";
			// 
			// PlayerControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ControlLightLight;
			BorderStyle = BorderStyle.FixedSingle;
			ContextMenuStrip = cmsFavorite;
			Controls.Add(pbCapitan);
			Controls.Add(pbFavorite);
			Controls.Add(lblPlayerPosition);
			Controls.Add(lblPlayerNumber);
			Controls.Add(lblPlayerName);
			Controls.Add(pbPlayerPicture);
			Margin = new Padding(0);
			Name = "PlayerControl";
			Size = new Size(310, 100);
			Load += PlayerControl_Load;
			((System.ComponentModel.ISupportInitialize)pbPlayerPicture).EndInit();
			((System.ComponentModel.ISupportInitialize)pbFavorite).EndInit();
			((System.ComponentModel.ISupportInitialize)pbCapitan).EndInit();
			cmsFavorite.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pbPlayerPicture;
		private Label lblPlayerName;
		private Label lblPlayerNumber;
		private Label lblPlayerPosition;
		private PictureBox pbFavorite;
		private PictureBox pbCapitan;
		private ToolTip toolTip;
		private ContextMenuStrip cmsFavorite;
		private ToolStripMenuItem cmsOptionAdd;
		private ToolStripMenuItem cmsOptionRemove;
	}
}
