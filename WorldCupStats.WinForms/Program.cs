using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Providers;
using WorldCupStats.Data.Repositories;
using WorldCupStats.Data.Providers;
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
			// Build configuration
			var configuration = ConfigurationProvider.BuildConfiguration();

			// Set up DI
			var services = new ServiceCollection();

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

			services.AddTransient<ISettingsRepository, SettingsRepository>();

			// Register your forms
			services.AddTransient<Form1>();
			services.AddTransient<SettingsForm>();

			var serviceProvider = services.BuildServiceProvider();

			ApplicationConfiguration.Initialize();

			var lang = configuration["Settings:Language"] ?? "en";
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

			if (!SettingsRepository.SettingsFileExists())
			{
				var settingsForm = serviceProvider.GetRequiredService<SettingsForm>();
				Application.Run(settingsForm);

				if (!settingsForm.SettingsSaved) return;

				var mainForm = serviceProvider.GetRequiredService<Form1>();
				Application.Run(mainForm);
			}
			else
			{
				var mainForm = serviceProvider.GetRequiredService<Form1>();
				Application.Run(mainForm);
			}
		}
	}
}