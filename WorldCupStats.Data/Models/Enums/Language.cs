namespace WorldCupStats.Data.Models;

public enum Language
{
	EN,
	HR
}

public static class LanguageExtensions
{
	public static string ToDisplayString(this Language language)
	{
		return language switch
		{
			Language.HR => "Hrvatski",
			Language.EN => "English",
			_ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
		};
	}

	public static Language FromDisplayString(string displayString)
	{
		return displayString switch
		{
			"Hrvatski" => Language.HR,
			"English" => Language.EN,
			_ => throw new ArgumentException($"Invalid display string: {displayString}", nameof(displayString))
		};
	}

	public static string ToSettingsString(this Language language)
	{
		return language switch
		{
			Language.HR => "hr-HR",
			Language.EN => "en",
			_ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
		};
	}

}