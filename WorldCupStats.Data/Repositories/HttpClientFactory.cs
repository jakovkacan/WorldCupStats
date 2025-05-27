using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using WorldCupStats.Data.Interfaces;

namespace WorldCupStats.Data.Repositories;

public class HttpClientFactory : IHttpClientFactory
{

	private readonly IConfiguration _config;
	public HttpClientFactory(IConfiguration config)
	{
		_config = config;
	}

	public HttpClient CreateClient()
	{
		var baseUrl = _config["DataConfig:ApiEndpoints:BaseUrl"];

		//var handler = new HttpClientHandler
		//{
		//	UseCookies = true,
		//	CookieContainer = new CookieContainer(),
		//	AllowAutoRedirect = true // Essential for Cloudflare challenges
		//};

		//var cookies = handler.CookieContainer.GetCookies(new Uri(baseUrl));

		// Reload cookies
		//foreach (Cookie cookie in cookies)
		//{
		//	handler.CookieContainer.Add(cookie);
		//}

		//var client = new HttpClient(handler);
		var client = new HttpClient();

		if (_config == null)
		{
			throw new InvalidOperationException("Configuration has not been initialized. Call HttpClientFactory.Initialize(config) first.");
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