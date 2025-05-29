using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WinForms.Forms;
using WorldCupStats.WinForms.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorldCupStats.WinForms.Controls
{
	public partial class PlayerControl : UserControl
	{
		private readonly Player _player;
		private readonly ResourceManager _rm;
		public PlayerControl(Player player)
		{
			_player = player;
			_rm = new ResourceManager("WorldCupStats.WinForms.Controls.PlayerControl", typeof(PlayerControl).Assembly);
			InitializeComponent();
		}

		public event EventHandler AddToFavoritesClicked;
		public event EventHandler RemovedFromFavoritesClicked;
		public event EventHandler SetPlayerPictureClicked;
		public event EventHandler RemovePlayerPictureClicked;

		private void PlayerControl_Load(object sender, EventArgs e)
		{
			toolTip.SetToolTip(this.pbCapitan, _rm.GetString("Capitan"));
			toolTip.SetToolTip(this.pbFavorite, _rm.GetString("FavoritePlayer"));

			lblPlayerName.Text = _player.Name;
			lblPlayerPosition.Text = _player.Position.ToString();
			lblPlayerNumber.Text = $"{_rm.GetString("Number")}: {_player.ShirtNumber.ToString()}";
			pbCapitan.Visible = _player.IsCapitan;
			pbFavorite.Visible = _player.IsFavorite;
			cmsOptionRemovePicture.Visible = false;
			cmsOptionSetPicture.Text = _rm.GetString("cmsOptionSetPicture.Text");

			try
			{
				if (_player.PictureFileName != null)
				{
					pbPlayerPicture.Image = Image.FromFile(FileUtils.GetPicturePath(_player.PictureFileName));
					cmsOptionRemovePicture.Visible = true;
					cmsOptionSetPicture.Text = _rm.GetString("ChangePicture");
				}
			}
			catch (Exception ex)
			{
				MessageBoxUtils.ShowError(ex.Message);
			}

			cmsOptionAdd.Visible = !_player.IsFavorite;
			cmsOptionRemove.Visible = _player.IsFavorite;

			foreach (Control ctrl in this.Controls)
			{
				ctrl.MouseDown += PlayerControl_MouseDown;
			}
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
			SetPlayerPictureClicked?.Invoke(this, new PlayerEventArgs(_player));
		}

		private void cmsOptionRemovePicture_Click(object sender, EventArgs e)
		{
			RemovePlayerPictureClicked?.Invoke(this, new PlayerEventArgs(_player));
		}

		private void PlayerControl_MouseDown(object? sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				return;

			DoDragDrop(this._player, DragDropEffects.Move);
		}
	}
}
