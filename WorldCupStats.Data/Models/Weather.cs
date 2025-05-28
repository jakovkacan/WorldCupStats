using System.Text.Json.Serialization;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Models;

public class Weather
{
	[JsonPropertyName("humidity")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int Humidity { get; set; }
	[JsonPropertyName("temp_celsius")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int Temperature { get; set; }
	[JsonPropertyName("wind_speed")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int WindSpeed { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(Humidity)}: {Humidity}, {nameof(Temperature)}: {Temperature}, {nameof(WindSpeed)}: {WindSpeed}, {nameof(Description)}: {Description}";
	}
}