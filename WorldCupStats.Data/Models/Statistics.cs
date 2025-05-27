using System.Text.Json.Serialization;

namespace WorldCupStats.Data.Models;

public class Statistics
{
	[JsonPropertyName("attempts_on_goal")]
	public int AttemptsOnGoal { get; set; }
	[JsonPropertyName("on_target")]
	public int OnTarget { get; set; }
	[JsonPropertyName("off_target")]
	public int OffTarget { get; set; }
	[JsonPropertyName("blocked")]
	public int Blocked { get; set; }
	[JsonPropertyName("woodwork")]
	public int Woodwork { get; set; }
	[JsonPropertyName("corners")]
	public int Corners { get; set; }
	[JsonPropertyName("offsides")]
	public int Offsides { get; set; }
	[JsonPropertyName("possession")]
	public int Possession { get; set; }
	[JsonPropertyName("pass_accuracy")]
	public int PassAccuracy { get; set; } // in percentage
	[JsonPropertyName("num_passes")]
	public int Passes { get; set; }
	[JsonPropertyName("passes_completed")]
	public int PassesCompleted { get; set; }
	[JsonPropertyName("distance_covered")]
	public int DistanceCovered { get; set; } // in meters
	[JsonPropertyName("balls_recovered")]
	public int BallsRecovered { get; set; }
	[JsonPropertyName("fouls_committed")]
	public int? FoulsCommitted { get; set; }
	[JsonPropertyName("yellow_cards")]
	public int YellowCards { get; set; }
	[JsonPropertyName("red_cards")]
	public int RedCards { get; set; }
	[JsonPropertyName("tackles")]
	public int Tackles { get; set; }
	[JsonPropertyName("clearances")]
	public int Clearances { get; set; }
	[JsonPropertyName("tactics")]
	public string Tactics { get; set; }

	[JsonPropertyName("starting_eleven")]
	public IEnumerable<Player> StartingEleven { get; set; }
	[JsonPropertyName("substitutes")]
	public IEnumerable<Player> Substitutes { get; set; }

	public override string ToString()
	{
		return
			$"{nameof(AttemptsOnGoal)}: {AttemptsOnGoal}, {nameof(OnTarget)}: {OnTarget}, {nameof(OffTarget)}: {OffTarget}, {nameof(Blocked)}: {Blocked}, {nameof(Woodwork)}: {Woodwork}, {nameof(Corners)}: {Corners}, {nameof(Offsides)}: {Offsides}, {nameof(Possession)}: {Possession}, {nameof(PassAccuracy)}: {PassAccuracy}, {nameof(Passes)}: {Passes}, {nameof(PassesCompleted)}: {PassesCompleted}, {nameof(DistanceCovered)}: {DistanceCovered}, {nameof(BallsRecovered)}: {BallsRecovered}, {nameof(FoulsCommitted)}: {FoulsCommitted}, {nameof(YellowCards)}: {YellowCards}, {nameof(RedCards)}: {RedCards}, {nameof(Tackles)}: {Tackles}, {nameof(Clearances)}: {Clearances}, {nameof(Tactics)}: {Tactics}, {nameof(StartingEleven)}: {StartingEleven}, {nameof(Substitutes)}: {Substitutes}";
	}
}