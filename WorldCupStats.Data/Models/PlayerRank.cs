namespace WorldCupStats.Data.Models;

public class PlayerRank
{
	public Player Player { get; set; }
	public int YellowCards { get; set; }
	public int GoalsScored { get; set; }

	public override string ToString()
	{
		return
			$"{Player.Name}: {nameof(YellowCards)}: {YellowCards}, {nameof(GoalsScored)}: {GoalsScored}";
	}
}