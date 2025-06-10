using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using WorldCupStats.WPF.Views;

namespace WorldCupStats.WPF.Controls
{
	public partial class PlayerDressControl : UserControl
	{
		//data binding properties
		public static readonly DependencyProperty PlayerNameProperty =
			DependencyProperty.Register(nameof(PlayerName), typeof(string), typeof(PlayerDressControl),
				new PropertyMetadata(string.Empty, OnPlayerNameChanged));

		public static readonly DependencyProperty FirstNameProperty =
			DependencyProperty.Register(nameof(FirstName), typeof(string), typeof(PlayerDressControl),
				new PropertyMetadata(string.Empty));

		public static readonly DependencyProperty LastNameProperty =
			DependencyProperty.Register(nameof(LastName), typeof(string), typeof(PlayerDressControl),
				new PropertyMetadata(string.Empty));

		public static readonly DependencyProperty ShirtNumberProperty =
			DependencyProperty.Register(nameof(ShirtNumber), typeof(int), typeof(PlayerDressControl),
				new PropertyMetadata(0));

		public static readonly DependencyProperty PhotoUrlProperty =
			DependencyProperty.Register(nameof(PhotoUrl), typeof(string), typeof(PlayerDressControl),
				new PropertyMetadata("/Resources/Images/default.jpg"));

		public static readonly DependencyProperty PictureFileNameProperty =
			DependencyProperty.Register(nameof(PictureFileName), typeof(string), typeof(PlayerDressControl),
				new PropertyMetadata(null, OnPictureFileNameChanged));

		public static readonly DependencyProperty PositionProperty =
			DependencyProperty.Register(nameof(Position), typeof(Position), typeof(PlayerDressControl),
				new PropertyMetadata(Position.Forward));

		public static readonly DependencyProperty IsOpponentProperty =
			DependencyProperty.Register(nameof(IsOpponent), typeof(bool), typeof(PlayerDressControl),
				new PropertyMetadata(false, OnIsOpponentChanged));

		private static void OnPlayerNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not PlayerDressControl control || e.NewValue is not string fullName) return;

			// Split the full name into first and last names
			var nameParts = fullName.Split(' ');
			if (nameParts.Length > 1)
			{
				control.FirstName = nameParts[0];
				control.LastName = string.Join(" ", nameParts.Skip(1));
			}
			else
			{
				control.FirstName = fullName;
				control.LastName = string.Empty;
			}
		}

		private static void OnPictureFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is PlayerDressControl control)
			{
				try
				{
					// Attempt to get the picture path from the file name
					control.PhotoUrl = e.NewValue is string fileName ?
						FileUtils.GetPicturePath(fileName) :
						"/Resources/Images/default.jpg";
				}
				catch
				{
					control.PhotoUrl = "/Resources/Images/default.jpg";
				}
			}
		}

		private static void OnIsOpponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is PlayerDressControl control)
				control.UpdateAppearance();
		}

		public PlayerDressControl()
		{
			InitializeComponent();
			this.DataContext = this;

			// Show popup on mouse enter, hide on mouse leave for the entire control
			this.MouseEnter += (s, e) => PlayerPopup.IsOpen = true;
			this.MouseLeave += (s, e) => PlayerPopup.IsOpen = false;

			// Add click event handler
			this.MouseLeftButtonDown += PlayerDressControl_MouseLeftButtonDown;

			UpdateAppearance();
		}

		// Method to update the appearance based on the team (opponent or not)
		private void UpdateAppearance()
		{
			if (IsOpponent)
			{
				DressImage.Source = new ImageSourceConverter().ConvertFromString(
					"pack://application:,,,/WorldCupStats.WPF;component/Resources/Images/dress_white.png"
				) as ImageSource;
				NumberText.Foreground = Brushes.Black;
			}
			else
			{
				DressImage.Source = new ImageSourceConverter().ConvertFromString(
					"pack://application:,,,/WorldCupStats.WPF;component/Resources/Images/dress_black.png"
				) as ImageSource;
				NumberText.Foreground = Brushes.White;
			}
		}

		private void PlayerDressControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.Tag is not PlayerInfo playerInfo) return;

			var window = new PlayerInfoView(playerInfo);
			window.Owner = Window.GetWindow(this);
			window.ShowDialog();
		}

		public string PlayerName
		{
			get => (string)GetValue(PlayerNameProperty);
			set => SetValue(PlayerNameProperty, value);
		}

		public string FirstName
		{
			get => (string)GetValue(FirstNameProperty);
			private set => SetValue(FirstNameProperty, value);
		}

		public string LastName
		{
			get => (string)GetValue(LastNameProperty);
			private set => SetValue(LastNameProperty, value);
		}

		public int ShirtNumber
		{
			get => (int)GetValue(ShirtNumberProperty);
			set => SetValue(ShirtNumberProperty, value);
		}

		public string PhotoUrl
		{
			get => (string)GetValue(PhotoUrlProperty);
			private set => SetValue(PhotoUrlProperty, value);
		}

		public string PictureFileName
		{
			get => (string)GetValue(PictureFileNameProperty);
			set => SetValue(PictureFileNameProperty, value);
		}

		public Position Position
		{
			get => (Position)GetValue(PositionProperty);
			set => SetValue(PositionProperty, value);
		}

		public bool IsOpponent
		{
			get => (bool)GetValue(IsOpponentProperty);
			init => SetValue(IsOpponentProperty, value);
		}
	}
}