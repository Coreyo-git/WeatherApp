namespace WeatherApp.API.Domain;

/// <summary>A single forecast day — daily summary, astronomical data, and hourly snapshots.</summary>
public sealed record ForecastDay
{
    public DateOnly Date { get; init; }
    public DailyForecastSummary Summary { get; init; } = null!;
    public IReadOnlyList<WeatherSnapshot> Hours { get; init; } = [];

    // Astro
    public TimeOnly Sunrise { get; init; }
    public TimeOnly Sunset { get; init; }
    public TimeOnly Moonrise { get; init; }
    public TimeOnly Moonset { get; init; }
    /// <summary>Moon phase description, e.g. "Waning Crescent".</summary>
    public string MoonPhase { get; init; } = string.Empty;
    /// <summary>Percentage of the moon illuminated (0–100).</summary>
    public int MoonIllumination { get; init; }

    private ForecastDay() { }

    public static ForecastDay Create(
        DateOnly date,
        DailyForecastSummary summary,
        IReadOnlyList<WeatherSnapshot> hours,
        TimeOnly sunrise,
        TimeOnly sunset,
        TimeOnly moonrise,
        TimeOnly moonset,
        string moonPhase,
        int moonIllumination)
    {
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(hours);

        if (string.IsNullOrWhiteSpace(moonPhase))
            throw new ArgumentException("Moon phase is required.", nameof(moonPhase));

        if (moonIllumination is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(moonIllumination), "Moon illumination must be between 0 and 100.");

        return new ForecastDay
        {
            Date = date,
            Summary = summary,
            Hours = hours,
            Sunrise = sunrise,
            Sunset = sunset,
            Moonrise = moonrise,
            Moonset = moonset,
            MoonPhase = moonPhase,
            MoonIllumination = moonIllumination
        };
    }
}
