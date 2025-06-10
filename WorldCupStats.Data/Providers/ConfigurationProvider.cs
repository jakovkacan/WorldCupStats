using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorldCupStats.Data.Utils;

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

	//updates the appsettings.json file with the new language setting (necessary for startup config)
	public static void UpdateAppSettingsLanguage(string newLanguage)
	{
		const string filePath = "appsettings.json";
		var json = File.ReadAllText(filePath);

		using var doc = JsonDocument.Parse(json);
		var root = doc.RootElement.Clone();

		var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;
		dict["Language"] = newLanguage;

		var updatedJson = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(filePath, updatedJson);

		var absoluteFilePath = $@"{Directory.GetParent(Directory.GetParent(FileUtils.GetBaseDirectory()).FullName)}\{filePath}";
		File.WriteAllText(absoluteFilePath, updatedJson);
	}
}