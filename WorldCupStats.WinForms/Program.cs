using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Providers;
using WorldCupStats.Data.Repositories;
using WorldCupStats.WinForms.Forms;
using ConfigurationProvider = WorldCupStats.Data.Providers.ConfigurationProvider;

namespace WorldCupStats.WinForms
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{


			// Set up DI
			var services = new ServiceCollection();



			services.AddSingleton<ISettingsRepository, SettingsRepository>();

			// Register your forms
			services.AddTransient<RankingForm>();
			services.AddTransient<MainForm>();
			services.AddTransient<SettingsForm>();

			var serviceProvider = services.BuildServiceProvider();

			ApplicationConfiguration.Initialize();

			if (!SettingsRepository.SettingsFileExists())
			{
				var settingsForm = serviceProvider.GetRequiredService<SettingsForm>();
				Application.Run(settingsForm);

				if (!settingsForm.SettingsSaved) return;
			}

			// Build configuration
			var configuration = ConfigurationProvider.BuildConfiguration();

			services.AddSingleton<IConfiguration>(configuration);
			services.AddLogging();

			// Register your repository
			var useLocalData = configuration["DataConfig:UseLocalData"]?.ToLowerInvariant() == "true";

			if (useLocalData)
				services.AddTransient<IDataRepository, LocalDataRepository>();
			else
			{
				services.AddTransient<IHttpClientFactory, HttpClientProvider>();
				services.AddTransient<IDataRepository, ApiDataRepository>();
			}

			serviceProvider = services.BuildServiceProvider();

			var lang = configuration["Language"] ?? "en";
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);

			var mainForm = serviceProvider.GetRequiredService<MainForm>();
			Application.Run(mainForm);

		}
	}
}