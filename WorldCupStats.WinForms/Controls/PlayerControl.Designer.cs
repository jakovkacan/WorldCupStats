using WorldCupStats.WinForms.Forms;

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
			cmsOptionSetPicture = new ToolStripMenuItem();
			cmsOptionRemovePicture = new ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)pbPlayerPicture).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbFavorite).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbCapitan).BeginInit();
			cmsFavorite.SuspendLayout();
			SuspendLayout();
			// 
			// pbPlayerPicture
			// 
			resources.ApplyResources(pbPlayerPicture, "pbPlayerPicture");
			pbPlayerPicture.Name = "pbPlayerPicture";
			pbPlayerPicture.TabStop = false;
			// 
			// lblPlayerName
			// 
			resources.ApplyResources(lblPlayerName, "lblPlayerName");
			lblPlayerName.Name = "lblPlayerName";
			// 
			// lblPlayerNumber
			// 
			resources.ApplyResources(lblPlayerNumber, "lblPlayerNumber");
			lblPlayerNumber.Name = "lblPlayerNumber";
			// 
			// lblPlayerPosition
			// 
			resources.ApplyResources(lblPlayerPosition, "lblPlayerPosition");
			lblPlayerPosition.Name = "lblPlayerPosition";
			// 
			// pbFavorite
			// 
			resources.ApplyResources(pbFavorite, "pbFavorite");
			pbFavorite.Name = "pbFavorite";
			pbFavorite.TabStop = false;
			// 
			// pbCapitan
			// 
			resources.ApplyResources(pbCapitan, "pbCapitan");
			pbCapitan.Name = "pbCapitan";
			pbCapitan.TabStop = false;
			// 
			// cmsFavorite
			// 
			cmsFavorite.Items.AddRange(new ToolStripItem[] { cmsOptionAdd, cmsOptionRemove, cmsOptionSetPicture, cmsOptionRemovePicture });
			cmsFavorite.Name = "cmsFavorite";
			resources.ApplyResources(cmsFavorite, "cmsFavorite");
			// 
			// cmsOptionAdd
			// 
			cmsOptionAdd.Name = "cmsOptionAdd";
			resources.ApplyResources(cmsOptionAdd, "cmsOptionAdd");
			cmsOptionAdd.Click += cmsOptionAdd_Click;
			// 
			// cmsOptionRemove
			// 
			cmsOptionRemove.Name = "cmsOptionRemove";
			resources.ApplyResources(cmsOptionRemove, "cmsOptionRemove");
			cmsOptionRemove.Click += cmsOptionRemove_Click;
			// 
			// cmsOptionSetPicture
			// 
			cmsOptionSetPicture.Name = "cmsOptionSetPicture";
			resources.ApplyResources(cmsOptionSetPicture, "cmsOptionSetPicture");
			cmsOptionSetPicture.Click += cmsOptionSetPicture_Click;
			// 
			// cmsOptionRemovePicture
			// 
			cmsOptionRemovePicture.Name = "cmsOptionRemovePicture";
			resources.ApplyResources(cmsOptionRemovePicture, "cmsOptionRemovePicture");
			cmsOptionRemovePicture.Click += cmsOptionRemovePicture_Click;
			// 
			// PlayerControl
			// 
			resources.ApplyResources(this, "$this");
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
			Name = "PlayerControl";
			Load += PlayerControl_Load;
			MouseDown += PlayerControl_MouseDown;
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
		private ToolStripMenuItem cmsOptionSetPicture;
		private ToolStripMenuItem cmsOptionRemovePicture;
	}
}
