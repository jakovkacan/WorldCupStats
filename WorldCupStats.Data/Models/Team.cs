using System.Text.Json.Serialization;

namespace WorldCupStats.Data.Models;

public class Team
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("country")]
	public string Country { get; set; }
	[JsonPropertyName("fifa_code")]
	public string FifaCode { get; set; }
	[JsonPropertyName("group_id")]
	public int GroupId { get; set; }
	[JsonPropertyName("group_letter")]
	public string GroupLetter { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(Id)}: {Id}, {nameof(Country)}: {Country}, {nameof(FifaCode)}: {FifaCode}, {nameof(GroupId)}: {GroupId}, {nameof(GroupLetter)}: {GroupLetter}";
	}
}