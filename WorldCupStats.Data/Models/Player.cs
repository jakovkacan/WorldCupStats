namespace WorldCupStats.Data.Models;


public enum Position
{
	Goalie,
	Defender,
	Midfield,
	Forward
}

public class Player
{
	public string Name { get; set; }
	public int ShirtNumber { get; set; }
	public Position Position { get; set; }
	public bool IsCapitan { get; set; }
	public string PicturePath { get; set; }
}