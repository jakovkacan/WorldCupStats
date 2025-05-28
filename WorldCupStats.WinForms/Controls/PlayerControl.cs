using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WinForms.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorldCupStats.WinForms.Controls
{
	public partial class PlayerControl : UserControl
	{
		public readonly Player Player;
		public PlayerControl(Player player)
		{
			Player = player;
			InitializeComponent();
		}

		public event EventHandler AddToFavoritesClicked;
		public event EventHandler RemovedFromFavoritesClicked;
		public event EventHandler SetPlayerPictureClicked;
		public event EventHandler RemovePlayerPictureClicked;

		private void PlayerControl_Load(object sender, EventArgs e)
		{
			toolTip.SetToolTip(this.pbCapitan, "Capitan");
			toolTip.SetToolTip(this.pbFavorite, "Favorite Player");

			lblPlayerName.Text = Player.Name;
			lblPlayerPosition.Text = Player.Position.ToString();
			lblPlayerNumber.Text = $"Number: {Player.ShirtNumber.ToString()}";
			pbCapitan.Visible = Player.IsCapitan;
			pbFavorite.Visible = Player.IsFavorite;
			cmsOptionRemovePicture.Visible = false;
			cmsOptionSetPicture.Text = "Add Picture";

			try
			{
				if (Player.PictureFileName != null)
				{
					pbPlayerPicture.Image = Image.FromFile(FileUtils.GetPicturePath(Player.PictureFileName));
					cmsOptionRemovePicture.Visible = true;
					cmsOptionSetPicture.Text = "Change Picture";
				}
			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}

			cmsOptionAdd.Visible = !Player.IsFavorite;
			cmsOptionRemove.Visible = Player.IsFavorite;

			foreach (Control ctrl in this.Controls)
			{
				ctrl.MouseDown += PlayerControl_MouseDown;
			}
		}

		private void cmsOptionAdd_Click(object sender, EventArgs e)
		{
			AddToFavoritesClicked?.Invoke(this, new PlayerEventArgs(Player));
		}

		private void cmsOptionRemove_Click(object sender, EventArgs e)
		{
			RemovedFromFavoritesClicked?.Invoke(this, new PlayerEventArgs(Player));
		}

		internal class PlayerEventArgs : EventArgs
		{
			public Player Player { get; }
			public PlayerEventArgs(Player player)
			{
				Player = player;
			}
		}

		private void cmsOptionSetPicture_Click(object sender, EventArgs e)
		{
			SetPlayerPictureClicked?.Invoke(this, new PlayerEventArgs(Player));
		}

		private void cmsOptionRemovePicture_Click(object sender, EventArgs e)
		{
			RemovePlayerPictureClicked?.Invoke(this, new PlayerEventArgs(Player));
		}

		private void PlayerControl_MouseDown(object? sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				return;

			DoDragDrop(this.Player, DragDropEffects.Move);
		}
	}
}
