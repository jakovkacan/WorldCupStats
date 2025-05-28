using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface ISettingsRepository
{
	T? GetValue<T>();
	void SetValue<T>(T value);
	void CreateSettingsFile(ChampionshipType? type = null, Language? language = null, DisplayMode? displayMode = null);
	bool IsInitialized();
	void SetPlayerPicture(string playerName, string picturePath);
	void RemovePlayerPicture(string playerName);
	void AddToFavorites(Player player);
	void RemoveFromFavorites(Player player);
	bool IsFavorite(Player player);
	Settings GetInstance();
}