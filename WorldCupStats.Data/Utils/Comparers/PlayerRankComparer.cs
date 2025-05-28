using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Utils;

public class PlayerRankComparer : IComparer<PlayerRank>
{
	public int Compare(PlayerRank x, PlayerRank y)
	{
		if (x == null && y == null) return 0;
		if (x == null) return 1;
		if (y == null) return -1;

		// Compare GoalsScored descending
		var goalsCompare = y.GoalsScored.CompareTo(x.GoalsScored);
		return goalsCompare != 0 ? goalsCompare :
			// Compare YellowCards ascending
			x.YellowCards.CompareTo(y.YellowCards);
	}
}