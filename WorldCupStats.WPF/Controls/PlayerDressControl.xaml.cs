using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.WPF.Controls
{
    public partial class PlayerDressControl : UserControl
    {
        public PlayerDressControl()
        {
            InitializeComponent();
            this.DataContext = this;
            
            // Show popup on mouse enter, hide on mouse leave for the entire control
            this.MouseEnter += (s, e) => PlayerPopup.IsOpen = true;
            this.MouseLeave += (s, e) => PlayerPopup.IsOpen = false;
        }

        #region Dependency Properties

        public static readonly DependencyProperty PlayerNameProperty =
            DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerDressControl),
                new PropertyMetadata(string.Empty, OnPlayerNameChanged));

        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register("FirstName", typeof(string), typeof(PlayerDressControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string), typeof(PlayerDressControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(int), typeof(PlayerDressControl),
                new PropertyMetadata(0));

        public static readonly DependencyProperty PhotoUrlProperty =
            DependencyProperty.Register("PhotoUrl", typeof(string), typeof(PlayerDressControl),
                new PropertyMetadata("/Resources/Images/default.jpg", OnPhotoUrlChanged));

        public static readonly DependencyProperty PictureFileNameProperty =
            DependencyProperty.Register("PictureFileName", typeof(string), typeof(PlayerDressControl),
                new PropertyMetadata(null, OnPictureFileNameChanged));

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Position), typeof(PlayerDressControl),
                new PropertyMetadata(Position.Forward));

        private static void OnPlayerNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerDressControl control && e.NewValue is string fullName)
            {
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
        }

        private static void OnPictureFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerDressControl control)
            {
                try
                {
                    string fileName = e.NewValue as string;
                    control.PhotoUrl = fileName != null ? 
                        FileUtils.GetPicturePath(fileName) : 
                        "/Resources/Images/default.jpg";
                }
                catch
                {
                    control.PhotoUrl = "/Resources/Images/default.jpg";
                }
            }
        }

        private static void OnPhotoUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // This is now handled by OnPictureFileNameChanged
        }

        public string PlayerName
        {
            get { return (string)GetValue(PlayerNameProperty); }
            set { SetValue(PlayerNameProperty, value); }
        }

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            private set { SetValue(FirstNameProperty, value); }
        }

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            private set { SetValue(LastNameProperty, value); }
        }

        public int ShirtNumber
        {
            get { return (int)GetValue(ShirtNumberProperty); }
            set { SetValue(ShirtNumberProperty, value); }
        }

        public string PhotoUrl
        {
            get { return (string)GetValue(PhotoUrlProperty); }
            private set { SetValue(PhotoUrlProperty, value); }
        }

        public string PictureFileName
        {
            get { return (string)GetValue(PictureFileNameProperty); }
            set { SetValue(PictureFileNameProperty, value); }
        }

        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        #endregion
    }
} 