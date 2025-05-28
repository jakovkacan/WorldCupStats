using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Utils
{
	public class PositionJsonConverter : JsonConverter<Position>
	{
		public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			return value?.ToLower() switch
			{
				"goalie" => Position.Goalie,
				"defender" => Position.Defender,
				"midfield" => Position.Midfield,
				"forward" => Position.Forward,
				_ => throw new JsonException($"Unknown position: {value}")
			};
		}

		public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
		{
			var str = value switch
			{
				Position.Goalie => "goalie",
				Position.Defender => "defender",
				Position.Midfield => "midfield",
				Position.Forward => "forward",
				_ => throw new JsonException($"Unknown position: {value}")
			};
			writer.WriteStringValue(str);
		}
	}
}