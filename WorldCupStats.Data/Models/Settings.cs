namespace WorldCupStats.Data.Models;

public class Settings
{
	public ChampionshipType Type { get; set; }
	public Language Language { get; set; }
	public DisplayMode DisplayMode { get; set; }
	public Team? FavoriteTeam { get; set; }
	public IEnumerable<Player>? FavoritePlayers { get; set; }
}