using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.Data.Repositories;

namespace WorldCupStats.Data.Utils;

public class PlayerJsonConverter : JsonConverter<Player>
{
	private readonly ISettingsRepository _settings;

	public PlayerJsonConverter(ISettingsRepository settings)
	{
		_settings = settings;
	}

	public override Player? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Deserialize the player using the default serializer
		var player = JsonSerializer.Deserialize<Player>(ref reader, options);

		if (player != null && _settings.IsInitialized())
		{
			player.IsFavorite = _settings.IsFavorite(player);
		}

		return player;
	}

	public override void Write(Utf8JsonWriter writer, Player value, JsonSerializerOptions options)
	{
		// Use the default serializer for writing
		JsonSerializer.Serialize(writer, value, options);
	}
}