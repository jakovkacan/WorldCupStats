using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.Configuration;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Providers;
using WorldCupStats.Data.Repositories;
using WorldCupStats.WPF.Views;
using ConfigurationProvider = WorldCupStats.Data.Providers.ConfigurationProvider;

namespace WorldCupStats.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static ServiceProvider? ServiceProvider { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var services = new ServiceCollection();

			// Build configuration and register it BEFORE other services that depend on it
			var configuration = ConfigurationProvider.BuildConfiguration();
			services.AddSingleton<IConfiguration>(configuration);

			// Register repositories and services
			services.AddSingleton<ISettingsRepository, SettingsRepository>();

			// Register views
			services.AddTransient<MainView>();
			services.AddTransient<SettingsView>();

			services.AddLogging();

			// Register data repository based on configuration
			var useLocalData = configuration["DataConfig:UseLocalData"]?.ToLowerInvariant() == "true";

			if (useLocalData)
				services.AddTransient<IDataRepository, LocalDataRepository>();
			else
			{
				services.AddTransient<IHttpClientFactory, HttpClientProvider>();
				services.AddTransient<IDataRepository, ApiDataRepository>();
			}

			// Build service provider ONCE, after all registrations
			ServiceProvider = services.BuildServiceProvider();

			if (!SettingsRepository.SettingsFileExists())
			{
				var settingsView = ServiceProvider.GetRequiredService<SettingsView>();
				settingsView.ShowDialog();

				if (!settingsView.SettingsSaved)
				{
					Shutdown();
					return;
				}
			}

			// Set culture based on settings
			var lang = configuration["Language"] ?? "en";
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);

			// Show main window
			var mainView = ServiceProvider.GetRequiredService<MainView>();
			mainView.Show();
		}
	}
}
