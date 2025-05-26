namespace WorldCupStats.Data.Models;

public enum EventType
{
	Goal,
	YellowCard,
	RedCard,
	SubstitutionIn,
	SubstitutionOut,
	Offside,
	Penalty
}

public class Event
{
	public int Id { get; set; }
	public EventType EventType { get; set; }
	public Player Player { get; set; }
	public string Time { get; set; }

}