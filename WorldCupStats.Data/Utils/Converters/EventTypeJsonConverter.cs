using System.Text.Json;
using System.Text.Json.Serialization;
using WorldCupStats.Data.Models;

public class EventTypeJsonConverter : JsonConverter<EventType>
{
	public override EventType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetString();
		return value switch
		{
			"goal" => EventType.Goal,
			"yellow-card" => EventType.YellowCard,
			"red-card" => EventType.RedCard,
			"yellow-card-second" => EventType.SecondYellowCard,
			"substitution-in" => EventType.SubstitutionIn,
			"substitution-out" => EventType.SubstitutionOut,
			"offside" => EventType.Offside,
			"penalty" => EventType.Penalty,
			"goal-penalty" => EventType.PenaltyGoal,
			"goal-own" => EventType.OwnGoal,
			_ => throw new JsonException($"Unknown event type: {value}")
		};
	}

	public override void Write(Utf8JsonWriter writer, EventType value, JsonSerializerOptions options)
	{
		var str = value switch
		{
			EventType.Goal => "goal",
			EventType.YellowCard => "yellow-card",
			EventType.RedCard => "red-card",
			EventType.SecondYellowCard => "yellow-card-second",
			EventType.SubstitutionIn => "substitution-in",
			EventType.SubstitutionOut => "substitution-out",
			EventType.Offside => "offside",
			EventType.Penalty => "penalty",
			EventType.PenaltyGoal => "goal-penalty",
			EventType.OwnGoal => "goal-own",
			_ => throw new JsonException($"Unknown event type: {value}")
		};
		writer.WriteStringValue(str);
	}
}