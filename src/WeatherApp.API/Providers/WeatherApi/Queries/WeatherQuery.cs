namespace WeatherApp.API.Providers;

/// <summary>Query parameters for a city search weather API request.</summary>
public sealed record WeatherQuery
{
	/// <summary>The city name passed to the weather API.</summary>
	public string Query { get; init; } = string.Empty;

	/// <summary>Optional language for condition text.</summary>
	public Language? Lang { get; init; }

	private WeatherQuery() { }

	public static WeatherQuery For(int id, Language? lang = null) =>
		new WeatherQuery { Query = $"id:{id.ToString()}", Lang = lang };

	public static WeatherQuery For(double lat, double lon, Language? lang = null) =>
		new WeatherQuery { Query = $"{lat},{lon}", Lang = lang };

}

