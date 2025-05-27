using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Repositories;

public class LocalDataRepository : IDataRepository
{
	private readonly string _filePath;
	public LocalDataRepository(IConfiguration config)
	{
		_filePath = config["DataConfig:LocalDataPaths:Teams"];
	}
	public async Task<IEnumerable<Team>> GetAllTeamsAsync(ChampionshipType type)
	{
		if (!File.Exists(_filePath))
			throw new FileNotFoundException("Local team data not found");
		using var stream = File.OpenRead(_filePath);
		return await JsonSerializer.DeserializeAsync<IEnumerable<Team>>(stream);
	}

	public Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type, string fifaCode)
	{
		throw new NotImplementedException();
	}

	public Task<Team> GetTeamByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Team> GetTeamByFifaCode(string fifaCode)
	{
		throw new NotImplementedException();
	}

	public string GetUrl(ChampionshipType type)
	{
		throw new NotImplementedException();
	}
}