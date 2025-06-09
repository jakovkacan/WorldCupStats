using System.Runtime.InteropServices;
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
	async Task<IEnumerable<Team>> GetAllOpponentTeamsAsync(string fifaCode)
	{
		var matches = await GetAllMatchesAsync(fifaCode);
		var teams = matches.Select(m =>
		{
			var code = m.HomeTeam.FifaCode ?? m.HomeTeam.Code;
			return code == fifaCode ? m.AwayTeam : m.HomeTeam;
		}).Distinct().ToList();

		var allTeams = await GetAllTeamsAsync();
		teams = allTeams.Where(t => teams.Any(ot => ot.Country == t.Country)).ToList();

		return teams;
	}
	async Task<Match> GetMatchAsync(string fifaCodeTeam1, string fifaCodeTeam2)
	{
		//find match by team FIFA codes
		var matches = await GetAllMatchesAsync(fifaCodeTeam1);
		var match = matches.FirstOrDefault(m =>
		{
			var codeHome = m.HomeTeam.FifaCode ?? m.HomeTeam.Code;
			var codeAway = m.AwayTeam.FifaCode ?? m.AwayTeam.Code;
			return (codeHome == fifaCodeTeam1 && codeAway == fifaCodeTeam2) ||
			       (codeHome == fifaCodeTeam2 && codeAway == fifaCodeTeam1);
		})!;

		// determine which team is team1 and which is team2 and set score
		var codeHome = match.HomeTeam.FifaCode ?? match.HomeTeam.Code;

		var homeTeamGoals = match.HomeTeamEvents.Count(e => e.EventType == EventType.Goal);
		var awayTeamGoals = match.AwayTeamEvents.Count(e => e.EventType == EventType.Goal);

		var settings = GetSettingsInstance();

		if (codeHome == fifaCodeTeam1)
		{
			match.Team1Goals = homeTeamGoals;
			match.Team2Goals = awayTeamGoals;
			match.Team1StartingEleven = PlayerUtils.UpdatePlayerFavoritesPictures(match.HomeTeamStatistics.StartingEleven, settings.FavoritePlayers, settings.PlayerPictures);
			match.Team2StartingEleven = PlayerUtils.UpdatePlayerFavoritesPictures(match.AwayTeamStatistics.StartingEleven, settings.FavoritePlayers, settings.PlayerPictures);

			match.Team1StartingEleven = UpdatePlayerStatistics(match.Team1StartingEleven, match.HomeTeamEvents);
			match.Team2StartingEleven = UpdatePlayerStatistics(match.Team2StartingEleven, match.AwayTeamEvents);
		}
		else
		{
			match.Team1Goals = awayTeamGoals;
			match.Team2Goals = homeTeamGoals;
			match.Team1StartingEleven = PlayerUtils.UpdatePlayerFavoritesPictures(match.AwayTeamStatistics.StartingEleven, settings.FavoritePlayers, settings.PlayerPictures);
			match.Team2StartingEleven = PlayerUtils.UpdatePlayerFavoritesPictures(match.HomeTeamStatistics.StartingEleven, settings.FavoritePlayers, settings.PlayerPictures);

			match.Team2StartingEleven = UpdatePlayerStatistics(match.Team2StartingEleven, match.HomeTeamEvents);
			match.Team1StartingEleven = UpdatePlayerStatistics(match.Team1StartingEleven, match.AwayTeamEvents);

		}

		return match;
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

	async Task<TeamStatistics> GetTeamStatistics(Team team)
	{
		var statistics = new TeamStatistics()
		{
			Country = team.Country,
			FifaCode = team.FifaCode ?? team.Code!,
		};

		var matches = GetAllMatchesAsync(statistics.FifaCode).Result.ToList();
		statistics.MatchCount = matches.Count();
		if (statistics.MatchCount == 0)
			return statistics;

		matches.ForEach(m =>
		{
			if (m.WinnerFifaCode == "Draw")
				statistics.Draws++;
			else if (m.WinnerFifaCode == statistics.FifaCode)
				statistics.Wins++;
			else
				statistics.Losses++;

			if (m.HomeTeam.FifaCode == statistics.FifaCode)
			{
				statistics.GoalsScored += m.HomeTeamEvents.Count(e => e.EventType == EventType.Goal);
				statistics.GoalsConceded += m.AwayTeamEvents.Count(e => e.EventType == EventType.Goal);
			}
			else
			{
				statistics.GoalsScored += m.AwayTeamEvents.Count(e => e.EventType == EventType.Goal);
				statistics.GoalsConceded += m.HomeTeamEvents.Count(e => e.EventType == EventType.Goal);
			}

		});

		return statistics;
	}

	private IEnumerable<Player> UpdatePlayerStatistics(IEnumerable<Player> players, IEnumerable<Event> events)
	{
		//player statistics
		events.ToList().ForEach(e =>
		{
			switch (e.EventType)
			{
				case EventType.Goal:
					players.First(p => p.Name == e.PlayerName).GoalsScored++;
					break;
				case EventType.YellowCard:
					players.First(p => p.Name == e.PlayerName).YellowCards++;
					break;
				default:
					// Other event types are not tracked in this context
					break;
			}
		});

		return players;
	}
}