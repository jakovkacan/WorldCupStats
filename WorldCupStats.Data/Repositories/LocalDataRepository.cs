﻿using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Repositories;

public class LocalDataRepository : IDataRepository
{
	private readonly IConfiguration _config;
	private readonly ISettingsRepository _settings;
	public LocalDataRepository(IConfiguration config, ISettingsRepository settings)
	{
		_config = config ?? throw new ArgumentNullException(nameof(config), "Configuration cannot be null");
		_settings = settings ?? throw new ArgumentNullException(nameof(settings), "Settings repository cannot be null");
	}

	public async Task<IEnumerable<Team>> GetAllTeamsAsync()
	{
		var file = _config["DataConfig:LocalData:Teams"];

		if (string.IsNullOrEmpty(file))
			throw new InvalidOperationException("Local data file path for teams is not configured.");

		return await GetLocalContent<Team>(file);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync()
	{
		var file = _config["DataConfig:LocalData:Matches"];

		if (string.IsNullOrEmpty(file))
			throw new InvalidOperationException("Local data file path for matches is not configured.");

		return await GetLocalContent<Match>(file);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(string fifaCode)
	{
		var matches = await GetAllMatchesAsync();

		return matches.Where(m =>
			{
				var codeHome = m.HomeTeam.FifaCode ?? m.HomeTeam.Code;
				var codeAway = m.AwayTeam.FifaCode ?? m.AwayTeam.Code;

				return string.Equals(codeHome, fifaCode, StringComparison.OrdinalIgnoreCase) ||
					   string.Equals(codeAway, fifaCode, StringComparison.OrdinalIgnoreCase);
			});
	}

	private async Task<IEnumerable<T>> GetLocalContent<T>(string filePath)
	{
		var type = _settings.GetValue<ChampionshipType>();
		var baseDirectory = FileUtils.GetBaseDirectory();

		var fullPath = Path.Combine(baseDirectory, $@"{type.ToString().ToLowerInvariant()}\", filePath);

		if (!File.Exists(fullPath))
			throw new FileNotFoundException($"The file {fullPath} does not exist.");

		// Read the file content as a string
		var jsonString = File.ReadAllTextAsync(fullPath);

		// Deserialize the JSON string to the specified type
		return JsonSerializer.Deserialize<IEnumerable<T>>(jsonString.Result) ?? [];
	}
	public Settings GetSettingsInstance() => _settings.GetInstance();
}