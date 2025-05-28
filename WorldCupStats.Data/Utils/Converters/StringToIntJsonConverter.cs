using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorldCupStats.Data.Utils.Converters;

public class StringToIntJsonConverter : JsonConverter<int>
{
	public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var str = reader.GetString();
			if (int.TryParse(str, out var value))
				return value;
			throw new JsonException($"Unable to convert \"{str}\" to int.");
		}
		if (reader.TokenType == JsonTokenType.Number)
		{
			return reader.GetInt32();
		}
		throw new JsonException($"Unexpected token {reader.TokenType} when parsing int.");
	}

	public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}
}