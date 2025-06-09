using System;
using System.Diagnostics;
using System.Windows;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Views
{
	/// <summary>
	/// Interaction logic for SettingsView.xaml
	/// </summary>
	public partial class SettingsView : Window
	{
		private readonly ISettingsRepository _settings;
		private readonly bool _isStartup;
		private ChampionshipType _type;
		private Language _language;
		private DisplayMode _displayMode;

		public bool SettingsSaved { get; private set; } = false;

		public SettingsView(ISettingsRepository settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_isStartup = !_settings.IsInitialized();
			InitializeComponent();
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (_isStartup)
			{
				_type = ChampionshipType.Men;
				_language = Data.Models.Language.EN;
				_displayMode = DisplayMode.WindowedMedium;

				rbTypeMen.IsChecked = true;
				rbLangEn.IsChecked = true;
				rbDisplayWindowed.IsChecked = true;
				rbSizeMedium.IsChecked = true;
			}
			else
			{
				_type = _settings.GetValue<ChampionshipType>();
				_language = _settings.GetValue<Language>();
				_displayMode = _settings.GetValue<DisplayMode>();

				rbTypeMen.IsChecked = _type == ChampionshipType.Men;
				rbTypeWomen.IsChecked = _type == ChampionshipType.Women;
				rbLangEn.IsChecked = _language == Data.Models.Language.EN;
				rbLangHr.IsChecked = _language == Data.Models.Language.HR;
				rbDisplayFullscreen.IsChecked = _displayMode == DisplayMode.Fullscreen;
				rbDisplayWindowed.IsChecked = _displayMode != DisplayMode.Fullscreen;

				if (_displayMode != DisplayMode.Fullscreen)
				{
					switch (_displayMode)
					{
						case DisplayMode.WindowedSmall:
							rbSizeSmall.IsChecked = true;
							break;
						case DisplayMode.WindowedMedium:
							rbSizeMedium.IsChecked = true;
							break;
						case DisplayMode.WindowedLarge:
							rbSizeLarge.IsChecked = true;
							break;
					}
				}
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			_type = rbTypeMen.IsChecked == true ? ChampionshipType.Men : ChampionshipType.Women;
			_language = rbLangEn.IsChecked == true ? Data.Models.Language.EN : Data.Models.Language.HR;
			
			if (rbDisplayFullscreen.IsChecked == true)
			{
				_displayMode = DisplayMode.Fullscreen;
			}
			else
			{
				if (rbSizeSmall.IsChecked == true)
				{
					_displayMode = DisplayMode.WindowedSmall;
				}
				else if (rbSizeMedium.IsChecked == true)
				{
					_displayMode = DisplayMode.WindowedMedium;
				}
				else
				{
					_displayMode = DisplayMode.WindowedLarge;
				}
			}

			var result = MessageBox.Show("Are you sure you want to save these settings?", "Confirm Settings", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result != MessageBoxResult.Yes) return;

			if (_isStartup)
			{
				_settings.CreateSettingsFile(_type, _language, _displayMode);
			}
			else
			{
				_settings.SetValue(_type);
				_settings.SetValue(_language);
				_settings.SetValue(_displayMode);
			}

			SettingsSaved = true;
			if (_settings.LanguageChanged())
			{
				Process.Start(Environment.ProcessPath!);
				Application.Current.Shutdown();
			}
			else
			{
				ApplyWindowSettings();
				Close();
			}
		}

		private void ApplyWindowSettings()
		{
			var mainWindow = Application.Current.MainWindow;
			if (mainWindow == null) return;

			if (_displayMode == DisplayMode.Fullscreen)
			{
				mainWindow.WindowState = WindowState.Maximized;
				mainWindow.WindowStyle = WindowStyle.None;
			}
			else
			{
				mainWindow.WindowState = WindowState.Normal;
				mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;

				switch (_displayMode)
				{
					case DisplayMode.WindowedSmall:
						mainWindow.Width = 800;
						mainWindow.Height = 600;
						break;
					case DisplayMode.WindowedMedium:
						mainWindow.Width = 1000;
						mainWindow.Height = 700;
						break;
					case DisplayMode.WindowedLarge:
						mainWindow.Width = 1200;
						mainWindow.Height = 800;
						break;
				}

				// Center the window
				mainWindow.Left = (SystemParameters.PrimaryScreenWidth - mainWindow.Width) / 2;
				mainWindow.Top = (SystemParameters.PrimaryScreenHeight - mainWindow.Height) / 2;
			}
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
