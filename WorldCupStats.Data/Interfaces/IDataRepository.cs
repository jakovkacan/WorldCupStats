using System.ComponentModel.DataAnnotations;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Interfaces;

public interface IDataRepository
{
	Task<IEnumerable<Team>> GetAllTeamsAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync(string fifaCode);

	async Task<IEnumerable<Player>> GetAllPlayersAsync(string fifaCode)
	{
		var match = GetAllMatchesAsync(fifaCode).Result.First();
		var settings = GetSettingsInstance();

		var statistics = match.HomeTeam.Code == fifaCode ? match.HomeTeamStatistics : match.AwayTeamStatistics;
		var players = statistics.StartingEleven.Union(statistics.Substitutes);

		return PlayerUtils.UpdatePlayerFavoritesPictures(players, settings.FavoritePlayers, settings.PlayerPictures);
	}
	Settings GetSettingsInstance();
}