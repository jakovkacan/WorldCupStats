using Microsoft.Extensions.Configuration;

namespace WorldCupStats.Data.Providers;

public static class ConfigurationProvider
{
	public static IConfigurationRoot BuildConfiguration()
	{
		return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false)
			.Build();
	}
}