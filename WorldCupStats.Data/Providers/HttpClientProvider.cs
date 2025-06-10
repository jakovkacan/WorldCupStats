using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using WorldCupStats.Data.Interfaces;

namespace WorldCupStats.Data.Providers;

public class HttpClientProvider : IHttpClientFactory
{

	private readonly IConfiguration _config;
	public HttpClientProvider(IConfiguration config)
	{
		_config = config;
	}

	//sets the base URL and default headers for the HttpClient
	public HttpClient CreateClient()
	{
		var baseUrl = _config["DataConfig:ApiEndpoints:BaseUrl"];

		var client = new HttpClient();

		if (_config == null)
		{
			throw new InvalidOperationException("Configuration has not been initialized. Call HttpClientProvider.Initialize(config) first.");
		}

		client.BaseAddress = new Uri(baseUrl);

		client.DefaultRequestHeaders.Add("User-Agent", "Anything");
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		client.DefaultRequestHeaders.UserAgent.ParseAdd(
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
			"(KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

		return client;

	}
}