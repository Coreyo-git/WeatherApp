namespace WeatherApp.API.Providers;

/// <summary>Query parameters for a city search weather API request.</summary>
public sealed record WeatherQuery
{
    /// <summary>The city name passed to the weather API.</summary>
    public string Query { get; init; } = string.Empty;

    /// <summary>Optional language for condition text.</summary>
    public Language? Lang { get; init; }

    private WeatherQuery() { }

    public static WeatherQuery For(string input, Language? lang = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("An Id or URL is required.", nameof(input));

        return new WeatherQuery { Query = input, Lang = lang };
    }
}
