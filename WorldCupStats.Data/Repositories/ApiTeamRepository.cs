using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Repositories;

public class ApiTeamRepository : ITeamRepository
{
	private readonly HttpClient _client;
	private readonly ILogger<ApiTeamRepository> _logger;
	private readonly Conf
	public ApiTeamRepository(ILogger<ApiTeamRepository> logger)
	{
		_client = HttpClientFactory.CreateClient();
		_logger = logger;
	}

	public async Task<IEnumerable<Team>> GetAllTeamsAsync() 
	{
		try
		{
			var response = await _client.GetAsync("teams");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<IEnumerable<Team>>();
		}
		catch (HttpRequestException ex)
		{
			_logger.LogError(ex, "API request failed");
			throw new Exception("Failed to retrieve team data", ex);
		}
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