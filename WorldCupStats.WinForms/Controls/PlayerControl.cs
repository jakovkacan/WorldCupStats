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

		private void PlayerControl_Load(object sender, EventArgs e)
		{
			toolTip.SetToolTip(this.pbCapitan, "Capitan");
			toolTip.SetToolTip(this.pbFavorite, "Favorite Player");

			lblPlayerName.Text = _player.Name;
			lblPlayerPosition.Text = _player.Position.ToString();
			lblPlayerNumber.Text = $"Shirt Number: {_player.ShirtNumber.ToString()}";
			pbCapitan.Visible = _player.IsCapitan;
			pbFavorite.Visible = _player.IsFavorite;

			cmsOptionAdd.Visible = !_player.IsFavorite;
			cmsOptionRemove.Visible = _player.IsFavorite;
		}
	}
}
