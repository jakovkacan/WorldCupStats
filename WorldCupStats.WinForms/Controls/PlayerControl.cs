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
		private readonly Player _player;
		public PlayerControl(Player player)
		{
			_player = player;
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

			lblPlayerName.Text = _player.Name;
			lblPlayerPosition.Text = _player.Position.ToString();
			lblPlayerNumber.Text = $"Shirt Number: {_player.ShirtNumber.ToString()}";
			pbCapitan.Visible = _player.IsCapitan;
			pbFavorite.Visible = _player.IsFavorite;
			cmsOptionRemovePicture.Visible = false;
			cmsOptionSetPicture.Text = "Add Picture";

			try
			{
				if (_player.PictureFileName != null)
				{
					pbPlayerPicture.Image = Image.FromFile(FileUtils.GetPicturePath(_player.PictureFileName));
					cmsOptionRemovePicture.Visible = true;
					cmsOptionSetPicture.Text = "Change Picture";
				}
			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}

			cmsOptionAdd.Visible = !_player.IsFavorite;
			cmsOptionRemove.Visible = _player.IsFavorite;
		}

		private void cmsOptionAdd_Click(object sender, EventArgs e)
		{
			AddToFavoritesClicked?.Invoke(this, new PlayerEventArgs(_player));
		}

		private void cmsOptionRemove_Click(object sender, EventArgs e)
		{
			RemovedFromFavoritesClicked?.Invoke(this, new PlayerEventArgs(_player));
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
			//pbPlayerPicture.Image = null;
			SetPlayerPictureClicked?.Invoke(this, new PlayerEventArgs(_player));
		}

		private void cmsOptionRemovePicture_Click(object sender, EventArgs e)
		{
			//pbPlayerPicture.Image = null;
			RemovePlayerPictureClicked?.Invoke(this, new PlayerEventArgs(_player));
		}
	}
}
