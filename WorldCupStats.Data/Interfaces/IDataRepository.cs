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

	async Task<Ranking> GetRanking(string fifaCode)
	{
		var matches = GetAllMatchesAsync(fifaCode).Result.ToList();
		var initPlayers = GetAllPlayersAsync(fifaCode).Result.ToList();
		if (!matches.Any())
			throw new InvalidOperationException("No matches found for the specified FIFA code.");

		var ranking = new Ranking
		{
			MatchRanking = matches,
		};

		ranking.AddPlayers(initPlayers);

		matches.ForEach(m =>
		{
			var homeCode = m.HomeTeam.FifaCode ?? m.HomeTeam.Code;

			var events = homeCode == fifaCode ? m.HomeTeamEvents.ToList() : m.AwayTeamEvents.ToList();
			var statistics = homeCode == fifaCode ? m.HomeTeamStatistics : m.AwayTeamStatistics;
			var players = statistics.StartingEleven.Union(statistics.Substitutes).ToList();

			ranking.AddPlayers(players);

			if (events == null || !events.Any() || statistics == null)
				return;
			
			events.ForEach(e =>
			{
				switch (e.EventType)
				{
					case EventType.Goal:
						ranking.AddStat(EventType.Goal, e.PlayerName);
						break;
					case EventType.YellowCard:
						ranking.AddStat(EventType.YellowCard, e.PlayerName);
						break;
					case EventType.RedCard:
					case EventType.SecondYellowCard:
					case EventType.SubstitutionIn:
					case EventType.SubstitutionOut:
					case EventType.Offside:
					case EventType.Penalty:
					case EventType.PenaltyGoal:
					case EventType.OwnGoal:
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(e.EventType), $"Unsupported event type: {e.EventType}");
						break;
				}
			});

		});

		ranking.PlayerRanking = ranking.PlayerRanking.OrderBy(p => p, new PlayerRankComparer()).ToList();
		ranking.MatchRanking = ranking.MatchRanking.OrderByDescending(m => m.Attendance).ToList();

		return ranking;
	}
}