using System.Net;
using WorldCupStats.Data.Interfaces;

namespace WorldCupStats.Data.Repositories;

public static class HttpClientFactory
{
	public static HttpClient CreateClient()
	{
		var handler = new HttpClientHandler
		{
			UseCookies = true,
			CookieContainer = new CookieContainer(),
			AllowAutoRedirect = true // Essential for Cloudflare challenges
		};

		var cookies = handler.CookieContainer.GetCookies(new Uri("https://target-site.com"));

		// Reload cookies
		foreach (Cookie cookie in cookies)
		{
			handler.CookieContainer.Add(cookie);
		}

		var client = new HttpClient(handler);

		client.DefaultRequestHeaders.UserAgent.ParseAdd(
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
			"(KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");


		return client;

	}
}