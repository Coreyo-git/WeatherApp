using WeatherApp.API.Domain.ValueObjects;

namespace WeatherApp.API.Domain.Models;

/// <summary>Weather forecast — current conditions and a multi-day breakdown for a location.</summary>
public sealed record WeatherForecast
{
    public Location Location { get; init; } = null!;
    public WeatherSnapshot Current { get; init; } = null!;
    public ForecastDay Today { get; init; } = null!;
    public IReadOnlyList<ForecastDay> Days { get; init; } = [];

    private WeatherForecast() { }

    public static WeatherForecast Create(Location location, WeatherSnapshot current, ForecastDay today, IReadOnlyList<ForecastDay> days)
    {
        ArgumentNullException.ThrowIfNull(location);
        ArgumentNullException.ThrowIfNull(current);
        ArgumentNullException.ThrowIfNull(today);
        ArgumentNullException.ThrowIfNull(days);

        return new WeatherForecast
        {
            Location = location,
            Current = current,
            Today = today,
            Days = days
        };
    }
}
