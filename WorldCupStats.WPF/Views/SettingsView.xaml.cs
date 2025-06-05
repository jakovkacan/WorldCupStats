using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			_type = rbTypeMen.IsChecked == true ? ChampionshipType.Men : ChampionshipType.Women;
			_language = rbLangEn.IsChecked == true ? Data.Models.Language.EN : Data.Models.Language.HR;
			_displayMode = rbDisplayFullscreen.IsChecked == true ? DisplayMode.Fullscreen : DisplayMode.WindowedMedium;

			var result = MessageBox.Show("Are you sure you want to save?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
				Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
