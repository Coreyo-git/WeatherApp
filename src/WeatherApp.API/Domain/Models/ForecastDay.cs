namespace WeatherApp.API.Domain.Models;

/// <summary>A single forecast day — daily summary, astronomical data, and hourly snapshots.</summary>
public sealed record ForecastDay
{
    public DateOnly Date { get; init; }
    public DailyForecastSummary Summary { get; init; } = null!;
    public IReadOnlyList<WeatherSnapshot> Hours { get; init; } = [];

    // Astro
    public TimeOnly Sunrise { get; init; }
    public TimeOnly Sunset { get; init; }

    private ForecastDay() { }

    public static ForecastDay Create(
        DateOnly date,
        DailyForecastSummary summary,
        IReadOnlyList<WeatherSnapshot> hours,
        TimeOnly sunrise,
        TimeOnly sunset)
    {
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(hours);

        return new ForecastDay
        {
            Date = date,
            Summary = summary,
            Hours = hours,
            Sunrise = sunrise,
            Sunset = sunset
        };
    }
}
