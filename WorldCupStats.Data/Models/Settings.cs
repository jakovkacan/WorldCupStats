namespace WorldCupStats.Data.Models;

public class Settings
{
	public ChampionshipType Type { get; set; }
	public Language Language { get; set; }
	public DisplayMode DisplayMode { get; set; }
	public Team? FavoriteTeam { get; set; }
	public List<Player> FavoritePlayers { get; set; } = [];
	public List<PlayerPicture> PlayerPictures { get; set; } = [];
}