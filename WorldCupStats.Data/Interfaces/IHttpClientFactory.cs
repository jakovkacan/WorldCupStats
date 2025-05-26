namespace WorldCupStats.Data.Interfaces;

public interface IHttpClientFactory
{
	HttpClient CreateClient();
}