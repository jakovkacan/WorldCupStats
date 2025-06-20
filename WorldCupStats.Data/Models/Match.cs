﻿using System.Text.Json.Serialization;
using WorldCupStats.Data.Utils;

namespace WorldCupStats.Data.Models;

public class Match
{
	[JsonPropertyName("venue")]
	public string Venue { get; set; }
	[JsonPropertyName("location")]
	public string Location { get; set; }
	[JsonPropertyName("fifa_id")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int FifaId { get; set; }
	[JsonPropertyName("attendance")]
	[JsonConverter(typeof(StringToIntJsonConverter))]
	public int Attendance { get; set; }
	[JsonPropertyName("officials")]
	public IEnumerable<string> Officials { get; set; }
	[JsonPropertyName("weather")]
	public Weather Weather { get; set; }
	[JsonPropertyName("home_team")]
	public Team HomeTeam { get; set; }
	[JsonPropertyName("away_team")]
	public Team AwayTeam { get; set; }
	[JsonPropertyName("home_team_events")]
	public IEnumerable<Event> HomeTeamEvents { get; set; }
	[JsonPropertyName("away_team_events")]
	public IEnumerable<Event> AwayTeamEvents { get; set; }
	[JsonPropertyName("home_team_statistics")]
	public Statistics HomeTeamStatistics { get; set; }
	[JsonPropertyName("away_team_statistics")]
	public Statistics AwayTeamStatistics { get; set; }
	[JsonPropertyName("datetime")]
	public DateTime DateTime { get; set; }
	[JsonPropertyName("stage_name")]
	public string Stage { get; set; }
	[JsonPropertyName("winner_code")]
	public string WinnerFifaCode { get; set; }

	//WPF visualization properties
	public int? Team1Goals { get; set; }
	public int? Team2Goals { get; set; }
	public IEnumerable<Player>? Team1StartingEleven { get; set; }
	public IEnumerable<Player>? Team2StartingEleven { get; set; }


	public override string ToString()
	{
		return
			$"{nameof(Venue)}: {Venue}, {nameof(Location)}: {Location}, {nameof(FifaId)}: {FifaId}, {nameof(Attendance)}: {Attendance}, {nameof(Officials)}: {Officials}, {nameof(Weather)}: {Weather}, {nameof(HomeTeam)}: {HomeTeam}, {nameof(AwayTeam)}: {AwayTeam}, {nameof(Team1Goals)}: {Team1Goals}, {nameof(Team2Goals)}: {Team2Goals}, {nameof(HomeTeamEvents)}: {HomeTeamEvents}, {nameof(AwayTeamEvents)}: {AwayTeamEvents}, {nameof(HomeTeamStatistics)}: {HomeTeamStatistics}, {nameof(AwayTeamStatistics)}: {AwayTeamStatistics}, {nameof(DateTime)}: {DateTime}, {nameof(Stage)}: {Stage}, {nameof(WinnerFifaCode)}: {WinnerFifaCode}";
	}
}