using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Repositories;

public class ApiDataRepository : IDataRepository
{
	private readonly HttpClient _client;
	private readonly ILogger<ApiDataRepository> _logger;
	private readonly IConfiguration _config;

	public ApiDataRepository(ILogger<ApiDataRepository> logger, IConfiguration config, IHttpClientFactory client)
	{
		_client = client.CreateClient();
		_logger = logger;
		_config = config;
	}

	public async Task<IEnumerable<Team>> GetAllTeamsAsync(ChampionshipType type)
	{
		var endpointUrl = _config["DataConfig:ApiEndpoints:Teams"];

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		return await GetHttpContent<Team>(type, endpointUrl);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type)
	{
		var endpointUrl = _config["DataConfig:ApiEndpoints:Matches"];

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		return await GetHttpContent<Match>(type, endpointUrl);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type, string fifaCode)
	{
		var endpointUrl = _config["DataConfig:ApiEndpoints:MatchesByCountry"];

		var url = string.Concat(endpointUrl, fifaCode);

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		return await GetHttpContent<Match>(type, url);
	}

	private async Task<IEnumerable<T>> GetHttpContent<T>(ChampionshipType type, string endpointUrl)
	{
		var response = _client.GetAsync($"{type.ToString().ToLower()}/{endpointUrl}").Result;
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<IEnumerable<T>>() ?? [];
	}

}