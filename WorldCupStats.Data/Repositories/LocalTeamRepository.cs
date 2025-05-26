using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Repositories;

public class LocalTeamRepository : ITeamRepository
{
	private readonly string _filePath;
	public LocalTeamRepository(IConfiguration config)
	{
		_filePath = config["DataConfig:LocalDataPaths:Teams"];
	}
	public async Task<IEnumerable<Team>> GetAllTeamsAsync()
	{
		if (!File.Exists(_filePath))
			throw new FileNotFoundException("Local team data not found");
		using var stream = File.OpenRead(_filePath);
		return await JsonSerializer.DeserializeAsync<IEnumerable<Team>>(stream);
	}

	public Task<Team> GetTeamByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Team> GetTeamByFifaCode(string fifaCode)
	{
		throw new NotImplementedException();
	}
}