using Microsoft.Extensions.Configuration;

namespace WorldCupStats.Data.Services;

public static class ConfigurationService
{
	public static IConfigurationRoot BuildConfiguration()
	{
		return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false)
			.Build();
	}

	//public static T GetSection<T>(string sectionName) where T : class, new()
	//{
	//	//var config = BuildConfiguration();
	//	//return config.GetSection(sectionName).Get<T>();
	//}
}