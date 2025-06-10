using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Repositories;

public class ApiDataRepository : IDataRepository
{
	private readonly HttpClient _client;
	private readonly ILogger<ApiDataRepository> _logger;
	private readonly IConfiguration _config;
	private readonly ISettingsRepository _settings;

	public ApiDataRepository(ILogger<ApiDataRepository> logger, IConfiguration config, IHttpClientFactory client, ISettingsRepository settings)
	{
		_client = client.CreateClient();
		_logger = logger;
		_config = config;
		_settings = settings;
	}

	public async Task<IEnumerable<Team>> GetAllTeamsAsync()
	{

		//await Task.Delay(3000);

		var endpointUrl = _config["DataConfig:ApiEndpoints:Teams"];

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		return await GetHttpContent<Team>(endpointUrl);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync()
	{
		var endpointUrl = _config["DataConfig:ApiEndpoints:Matches"];

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		var content = await GetHttpContent<Match>(endpointUrl);

		return await GetHttpContent<Match>(endpointUrl);
	}

	public async Task<IEnumerable<Match>> GetAllMatchesAsync(string fifaCode)
	{
		var endpointUrl = _config["DataConfig:ApiEndpoints:MatchesByCountry"];

		var url = string.Concat(endpointUrl, fifaCode);

		if (string.IsNullOrEmpty(endpointUrl))
			throw new InvalidOperationException("API base URL or endpoint is not configured.");

		return await GetHttpContent<Match>(url);
	}

	private async Task<IEnumerable<T>> GetHttpContent<T>(string endpointUrl)
	{
		var type = _settings.GetValue<ChampionshipType>();
		var response = _client.GetAsync($"{type.ToString().ToLower()}/{endpointUrl}").Result;
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<IEnumerable<T>>() ?? [];
	}

	public Settings GetSettingsInstance() => _settings.GetInstance();

}