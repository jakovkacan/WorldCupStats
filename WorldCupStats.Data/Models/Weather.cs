using System.Text.Json.Serialization;

namespace WorldCupStats.Data.Models;

public class Weather
{
	[JsonPropertyName("humidity")]
	public int Humidity { get; set; }
	[JsonPropertyName("temp_celsius")]
	public int Temperature { get; set; }
	[JsonPropertyName("wind_speed")]
	public int WindSpeed { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(Humidity)}: {Humidity}, {nameof(Temperature)}: {Temperature}, {nameof(WindSpeed)}: {WindSpeed}, {nameof(Description)}: {Description}";
	}
}