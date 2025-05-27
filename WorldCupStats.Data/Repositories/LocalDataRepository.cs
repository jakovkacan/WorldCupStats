using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Repositories;

public class LocalDataRepository : IDataRepository
{
	private readonly IConfiguration _config;
	public LocalDataRepository(IConfiguration config)
	{
		_config = config ?? throw new ArgumentNullException(nameof(config), "Configuration cannot be null");
	}

	public async Task<IEnumerable<Team>> GetAllTeamsAsync(ChampionshipType type)
	{
		var file = _config["DataConfig:LocalData:Teams"];

		if (string.IsNullOrEmpty(file))
			throw new InvalidOperationException("Local data file path for teams is not configured.");

		return await GetLocalContent<Team>(type, file);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type)
	{
		var file = _config["DataConfig:LocalData:Matches"];

		if (string.IsNullOrEmpty(file))
			throw new InvalidOperationException("Local data file path for matches is not configured.");

		return await GetLocalContent<Match>(type, file);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type, string fifaCode)
	{
		var matches = await GetAllMatchesAsync(type);

		return matches.Where(m =>
			{
				var codeHome = m.HomeTeam.FifaCode ?? m.HomeTeam.Code;
				var codeAway = m.AwayTeam.FifaCode ?? m.AwayTeam.Code;

				return string.Equals(codeHome, fifaCode, StringComparison.OrdinalIgnoreCase) ||
					   string.Equals(codeAway, fifaCode, StringComparison.OrdinalIgnoreCase);
			});
	}

	private static async Task<IEnumerable<T>> GetLocalContent<T>(ChampionshipType type, string filePath)
	{
		var baseDirectory = FileUtils.GetBaseDirectory();

		var fullPath = Path.Combine(baseDirectory, $@"WorldCupStats.Data\LocalData\{type.ToString().ToLowerInvariant()}\", filePath);

		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The file {fullPath} does not exist.");

		// Read the file content as a string
		var jsonString = File.ReadAllTextAsync(fullPath);

		//if (string.IsNullOrWhiteSpace(jsonString))
		//	throw new InvalidOperationException($"The file {fullPath} is empty or contains invalid JSON.");

		// Deserialize the JSON string to the specified type
		return JsonSerializer.Deserialize<IEnumerable<T>>(jsonString.Result) ?? [];
	}
}