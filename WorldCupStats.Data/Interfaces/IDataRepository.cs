using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface IDataRepository
{
	Task<IEnumerable<Team>> GetAllTeamsAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync(string fifaCode);

	async Task<IEnumerable<Player>> GetAllPlayersAsync(string fifaCode)
	{
		var match = GetAllMatchesAsync(fifaCode).Result.First();

		var statistics = match.HomeTeam.Code == fifaCode ? match.HomeTeamStatistics : match.AwayTeamStatistics;

		return statistics.StartingEleven.Union(statistics.Substitutes);
	}
}