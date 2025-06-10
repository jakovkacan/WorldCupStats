using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

/// <summary>
/// Defines a repository for managing application settings, including retrieval, modification, and persistence of
/// settings.
/// </summary>
/// <remarks>This interface provides methods for interacting with application settings, such as retrieving and
/// updating values,  managing player-related data, and handling favorites. It also includes functionality for
/// initialization and  language change detection.</remarks>
public interface ISettingsRepository
{
	T? GetValue<T>();
	void SetValue<T>(T value);
	void CreateSettingsFile(ChampionshipType? type = null, Language? language = null, DisplayMode? displayMode = null);
	bool IsInitialized();
	string SetPlayerPicture(string playerName, string picturePath);
	void RemovePlayerPicture(string playerName);
	void AddToFavorites(Player player);
	void RemoveFromFavorites(Player player);
	bool IsFavorite(Player player);
	Settings GetInstance();
	bool LanguageChanged();
	void SetTeam2(Team team);
	Team? GetTeam2();
}