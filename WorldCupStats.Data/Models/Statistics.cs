namespace WorldCupStats.Data.Models;

public class Statistics
{
	public int AttemptsOnGoal { get; set; }
	public int OnTarget { get; set; }
	public int OffTarget { get; set; }
	public int Blocked { get; set; }
	public int Woodwork { get; set; }
	public int Corners { get; set; }
	public int Offsides { get; set; }
	public int Possession { get; set; }
	public int PassAccuracy { get; set; } // in percentage
	public int Passes { get; set; }
	public int PassesCompleted { get; set; }
	public int DistanceCovered { get; set; } // in meters
	public int BallsRecovered { get; set; }
	public int FoulsCommitted { get; set; }
	public int YellowCards { get; set; }
	public int RedCards { get; set; }
	public int Tackles { get; set; }
	public int Clearances { get; set; }
	public string Tactics { get; set; }

	public IEnumerable<Player> StartingEleven { get; set; }
	public IEnumerable<Player> Substitutes { get; set; }

}