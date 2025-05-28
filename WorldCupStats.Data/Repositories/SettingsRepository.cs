using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Utils;
using System.Text.Json;

namespace WorldCupStats.Data.Repositories;

public class SettingsRepository : ISettingsRepository
{
	private Settings? _settings;

	public SettingsRepository()
	{
		if (SettingsFileExists())
			_settings = LoadSettings();
	}

	public T GetValue<T>()
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

		if (typeof(T) == typeof(ChampionshipType))
			_settings.Type = (ChampionshipType)((object)value!)!;

		if (typeof(T) == typeof(Language))
			_settings.Language = (Language)((object)value!)!;

		if (typeof(T) == typeof(DisplayMode))
			_settings.DisplayMode = (DisplayMode)((object)value!)!;

		if (typeof(T) == typeof(Team))
			_settings.FavoriteTeam = (Team)((object)value!)!;

		if (typeof(T) == typeof(List<Player>))
			_settings.FavoritePlayers = (List<Player>)((object)value!)!;

		SaveSettings();
	}

	public void SetPlayerPicture(string playerName, string picturePath)
	{
		if (_settings == null)
			throw new InvalidOperationException("Settings have not been initialized.");

		var fileName = FileUtils.CopyPicture(picturePath);

		var index = _settings.PlayerPictures.FindIndex(pp => pp.Name == playerName);

		if (index >= 0)
		{
			FileUtils.DeletePicture(_settings.PlayerPictures[index].PictureFileName); // Delete old picture
			_settings.PlayerPictures[index].PictureFileName = fileName; // Replace existing
		}
		else
			_settings.PlayerPictures.Add(new PlayerPicture() // Add new
			{
				Name = playerName,
				PictureFileName = fileName
			});

		SaveSettings();
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
			FavoritePlayers = new List<Player>()
		};

		var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
		{
			WriteIndented = true,
		});

		File.WriteAllText(path, json);
		_settings = settings;
	}

	public bool IsInitialized() => _settings != null;

	private static string GetSettingsFilePath() => Path.Combine(FileUtils.GetBaseDirectory(), "preferences.json");
	public static bool SettingsFileExists() => File.Exists(GetSettingsFilePath());

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