using System.Text.Json.Serialization;

namespace WorldCupStats.Data.Models;

public class Event
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("type_of_event")]
	[JsonConverter(typeof(EventTypeJsonConverter))]
	public EventType EventType { get; set; }
	[JsonPropertyName("player")]
	public string PlayerName { get; set; }
	[JsonPropertyName("time")]
	public string Time { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(Id)}: {Id}, {nameof(EventType)}: {EventType}, {nameof(PlayerName)}: {PlayerName}, {nameof(Time)}: {Time}";
	}
}