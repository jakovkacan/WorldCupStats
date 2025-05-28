using System.Text.Json.Serialization;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Models;

public class Player
{
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("shirt_number")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int ShirtNumber { get; set; }
	[JsonPropertyName("position")]
	[JsonConverter(typeof(PositionJsonConverter))]
	public Position Position { get; set; }
	[JsonPropertyName("captain")]
	public bool IsCapitan { get; set; }
	public bool IsFavorite { get; set; } = false;
	public string? PictureFileName { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(Name)}: {Name}, {nameof(ShirtNumber)}: {ShirtNumber}, {nameof(Position)}: {Position}, {nameof(IsCapitan)}: {IsCapitan}, {nameof(PictureFileName)}: {PictureFileName}";
	}
}