namespace WorldCupStats.Data.Models;

public class Match
{
	public string Venue { get; set; }
	public string Location { get; set; }
	public int FifaId { get; set; }
	public int Attendance { get; set; }
	public IEnumerable<string> Officials { get; set; }
	public Weather Weather { get; set; }
	public Team HomeTeam { get; set; }
	public Team AwayTeam { get; set; }
	public int HomeTeamGoals { get; set; }
	public int AwayTeamGoals { get; set; }
	public IEnumerable<Event> HomeTeamEvents { get; set; }
	public IEnumerable<Event> AwayTeamEvents { get; set; }
	public Statistics HomeTeamStatistics { get; set; }
	public Statistics AwayTeamStatistics { get; set; }
	public DateTime DateTime { get; set; }
	public string Stage { get; set; }
	public string WinnerFifaCode { get; set; }
}