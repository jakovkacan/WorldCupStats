using System.Windows;
using System.Windows.Media.Imaging;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.WPF.Views
{
	public partial class PlayerInfoView : Window
	{
		public PlayerInfoView(PlayerInfo player)
		{
			InitializeComponent();

			// Set player information
			PlayerNameText.Text = player.Name;
			NumberText.Text = player.Number.ToString();
			PositionText.Text = player.Position.ToString();
			CaptainText.Text = player.IsCaptain ? WPF.Resources.Resources.Yes : WPF.Resources.Resources.No;
			GoalsText.Text = player.Goals.ToString();
			YellowCardsText.Text = player.YellowCards.ToString();

			// Set player image
			try
			{
				var imagePath = FileUtils.GetPicturePath(player.PictureFileName);
				PlayerImage.Source = new BitmapImage(new Uri(imagePath));
			}
			catch
			{
				PlayerImage.Source = new BitmapImage(new Uri("pack://application:,,,/WorldCupStats.WPF;component/Resources/Images/default.jpg"));
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
		
	}
}