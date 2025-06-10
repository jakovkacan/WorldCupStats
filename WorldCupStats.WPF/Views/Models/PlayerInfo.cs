using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Views;

public class PlayerInfo
{
	public string Name { get; set; }
	public int Number { get; set; }
	public Position Position { get; set; }
	public string? PictureFileName { get; set; }
	public bool IsCaptain { get; set; }
	public int Goals { get; set; }
	public int YellowCards { get; set; }
}