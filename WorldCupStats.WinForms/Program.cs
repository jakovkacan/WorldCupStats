using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Repositories;
using WorldCupStats.Data.Services;

namespace WorldCupStats.WinForms
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Build configuration
			var configuration = ConfigurationService.BuildConfiguration();

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
				services.AddTransient<IHttpClientFactory, HttpClientFactory>();
				services.AddTransient<IDataRepository, ApiDataRepository>();
			}

			// Register your forms
			services.AddTransient<Form1>();

			var serviceProvider = services.BuildServiceProvider();

			ApplicationConfiguration.Initialize();

			var lang = configuration["Settings:Language"] ?? "en-US";
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);


			// Resolve and run the main form
			var mainForm = serviceProvider.GetRequiredService<Form1>();
			Application.Run(mainForm);

		}
	}
}