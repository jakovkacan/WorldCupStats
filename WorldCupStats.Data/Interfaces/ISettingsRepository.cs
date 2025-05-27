using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface ISettingsRepository
{
	T GetValue<T>();
	void SetValue<T>(T value);
	void CreateSettingsFile(ChampionshipType? type = null, Language? language = null, DisplayMode? displayMode = null);
	bool IsInitialized();
}