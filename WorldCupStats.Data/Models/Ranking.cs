namespace WorldCupStats.Data.Models;

public class Ranking
{
	public IList<PlayerRank> PlayerRanking { get; set; } = new List<PlayerRank>();
	public IList<Match> MatchRanking { get; set; } = new List<Match>();


}

public static class RankingExtensions
{
	public static void AddPlayers(this Ranking ranking, IEnumerable<Player> players)
	{
		players.ToList().ForEach(p =>
		{
			if (ranking.PlayerRanking.Any(pr => pr.Player.Name == p.Name))
				return; // Player already exists in ranking

			ranking.PlayerRanking.Add(new PlayerRank
			{
				Player = p,
			});
		});
	}

	public static void AddStat(this Ranking ranking, EventType type, string name)
	{
		if (ranking.PlayerRanking.Any(pr => pr.Player.Name == name))
		{
			var playerRank = ranking.PlayerRanking.First(pr => pr.Player.Name == name);
			switch (type)
			{
				case EventType.Goal:
					playerRank.GoalsScored++;
					break;
				case EventType.YellowCard:
					playerRank.YellowCards++;
					break;
				default:
					{
						throw new InvalidOperationException($"Event type {type} is not supported for ranking.");
						break;
					}
			}
		}
		else
		{
			throw new InvalidOperationException($"Player {name} not found in ranking.");
		}
	}
}

