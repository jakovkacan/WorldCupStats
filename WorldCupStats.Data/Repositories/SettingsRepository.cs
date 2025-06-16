using System.Text.Json;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Providers;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Repositories;

public class SettingsRepository : ISettingsRepository
{
	private Settings? _settings;
	private bool _languageChanged;

	public SettingsRepository()
	{
		if (SettingsFileExists())
			_settings = LoadSettings();
	}
	public bool LanguageChanged() => _languageChanged;
	public bool IsInitialized() => _settings != null;
	public static bool SettingsFileExists() => File.Exists(GetSettingsFilePath());

	public void CreateSettingsFile(ChampionshipType? type, Language? language, DisplayMode? displayMode)
	{
		var path = GetSettingsFilePath();

		if (File.Exists(path))
			File.Delete(path);

		var settings = new Settings
		{
			Type = type ?? ChampionshipType.Men,
			Language = language ?? Language.EN,
			DisplayMode = displayMode ?? DisplayMode.WindowedMedium,
			FavoriteTeam = null,
			FavoritePlayers = []
		};

		var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
		{
			WriteIndented = true,
		});

		File.WriteAllText(path, json);
		_settings = settings;
		ConfigurationProvider.UpdateAppSettingsLanguage(_settings.Language.ToSettingsString());
	}

	public Settings GetInstance()
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");
		return _settings;
	}

	public T? GetValue<T>()
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		if (typeof(T) == typeof(ChampionshipType))
			return (T)(object)_settings.Type;

		if (typeof(T) == typeof(Language))
			return (T)(object)_settings.Language;

		if (typeof(T) == typeof(DisplayMode))
			return (T)(object)_settings.DisplayMode;

		if (typeof(T) == typeof(Team))
			return (T)(object)_settings.FavoriteTeam!;

		//if (typeof(T) == typeof(Team2))
		//	return (T)(object)_settings.OpponentTeam;

		if (typeof(T) == typeof(IEnumerable<Player>))
			return (T)(object)_settings.FavoritePlayers!;

		throw new InvalidOperationException($"Unsupported type: {typeof(T).Name}");
	}

	public void SetValue<T>(T value)
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		if (EqualityComparer<T>.Default.Equals(value, GetValue<T>()))
			return; // No change, nothing to save

		_languageChanged = false; // Reset language change flag

		if (typeof(T) == typeof(ChampionshipType))
		{
			_settings.Type = (ChampionshipType)((object)value!)!;
			_settings.FavoriteTeam = null; // Reset favorite team when changing type
			_settings.FavoritePlayers.Clear(); // Reset favorite players when changing type
		}

		if (typeof(T) == typeof(Language))
		{
			_settings.Language = (Language)((object)value!)!;
			ConfigurationProvider.UpdateAppSettingsLanguage(_settings.Language.ToSettingsString());
			_languageChanged = true; // Flag to indicate language change
		}

		if (typeof(T) == typeof(DisplayMode))
			_settings.DisplayMode = (DisplayMode)((object)value!)!;

		if (typeof(T) == typeof(Team))
		{
			_settings.FavoriteTeam = (Team)((object)value!)!;
			_settings.OpponentTeam = null; // Reset opponent team when setting favorite team
			ResetFavorites();
		}

		//if (typeof(T) == typeof(Team2))
		//	_settings.OpponentTeam = (Team2)((object)value!)!;

		if (typeof(T) == typeof(List<Player>))
			_settings.FavoritePlayers = (List<Player>)((object)value!)!;

		SaveSettings();
	}

	// WPF Team2
	public Team? GetTeam2()
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		return _settings.OpponentTeam;
	}
	public void SetTeam2(Team team)
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		if (EqualityComparer<Team>.Default.Equals(team, GetTeam2()))
			return; // No change, nothing to save

		_settings.OpponentTeam = team;
		SaveSettings();
	}

	//player pictures
	public string? SetPlayerPicture(string playerName, string picturePath)
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		var fileName = FileUtils.CopyPicture(picturePath);

		var index = _settings.PlayerPictures.FindIndex(pp => pp.Name == playerName);

		if (index >= 0)
		{
			_settings.PlayerPictures[index].PictureFileName = fileName; // Replace existing
		}
		else
			_settings.PlayerPictures.Add(new PlayerPicture() // Add new
			{
				Name = playerName,
				PictureFileName = fileName
			});

		SaveSettings();
		return fileName;
	}

	public void RemovePlayerPicture(string playerName)
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		var index = _settings.PlayerPictures.FindIndex(pp => pp.Name == playerName);

		if (index == -1) return;

		_settings.PlayerPictures.RemoveAt(index);

		SaveSettings();
	}

	//favorite players
	public void AddToFavorites(Player player)
	{
		if (_settings?.FavoriteTeam == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		if (_settings.FavoritePlayers.Count >= 3)
			throw new InvalidOperationException(
				_settings.Language == Language.HR 
					? "Ne možete dodati više od 3 omiljena igrača." 
					: "Cannot add more than 3 favorite players.");

		player.IsFavorite = true; // Mark player as favorite

		if (_settings.FavoritePlayers.All(p => p.Name != player.Name)) // Avoid duplicates
			_settings.FavoritePlayers.Add(player);

		SaveSettings();
	}

	public void RemoveFromFavorites(Player player)
	{
		if (_settings?.FavoriteTeam == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		player.IsFavorite = false;

		if (_settings.FavoritePlayers.Any(p => p.Name == player.Name))
			_settings.FavoritePlayers.Remove(_settings.FavoritePlayers.First(p => p.Name == player.Name));

		SaveSettings();
	}

	private void ResetFavorites()
	{
		_settings.FavoritePlayers = new List<Player>(); // Clear favorite players
		SaveSettings();
	}

	public bool IsFavorite(Player player)
	{
		if (_settings?.FavoriteTeam == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		return _settings.FavoritePlayers.Any(p => p.Name == player.Name);
	}


	private static string GetSettingsFilePath() => Path.Combine(FileUtils.GetBaseDirectory(), "preferences.json");
	
	private static Settings? LoadSettings()
	{
		var path = GetSettingsFilePath();

		var json = File.ReadAllText(path);
		return JsonSerializer.Deserialize<Settings>(json);
	}
	private void SaveSettings()
	{
		var path = GetSettingsFilePath();
		var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
		{
			WriteIndented = true,
		});
		File.WriteAllText(path, json);
	}
	

	


}