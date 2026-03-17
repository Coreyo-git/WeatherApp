using WeatherApp.API.Domain.ValueObjects;

namespace WeatherApp.API.Domain.Models;

/// <summary>Today's weather forecast — current conditions and the full day breakdown for a location.</summary>
public sealed record WeatherForecast
{
    public Location Location { get; init; } = null!;
    public WeatherSnapshot Current { get; init; } = null!;
    public ForecastDay Today { get; init; } = null!;

    private WeatherForecast() { }

    public static WeatherForecast Create(Location location, WeatherSnapshot current, ForecastDay today)
    {
        ArgumentNullException.ThrowIfNull(location);
        ArgumentNullException.ThrowIfNull(current);
        ArgumentNullException.ThrowIfNull(today);

        return new WeatherForecast
        {
            Location = location,
            Current = current,
            Today = today
        };
    }
}
