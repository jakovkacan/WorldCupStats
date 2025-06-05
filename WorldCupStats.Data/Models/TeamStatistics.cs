namespace WorldCupStats.Data.Models;

public class TeamStatistics
{
	public string Country { get; set; }
	public string FifaCode { get; set; }
	public int MatchCount { get; set; }
	public int Wins { get; set; }
	public int Draws { get; set; }
	public int Losses { get; set; }
	public int GoalsScored { get; set; }
	public int GoalsConceded { get; set; }
	public int GoalDifference => GoalsScored - GoalsConceded;
}