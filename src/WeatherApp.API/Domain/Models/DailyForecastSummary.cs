using WeatherApp.API.Domain.ValueObjects;

namespace WeatherApp.API.Domain.Models;

/// <summary>Aggregated weather summary for a full day — highs, lows, totals, and precipitation chances.</summary>
public sealed record DailyForecastSummary
{
    public double MaxTemperatureCelsius { get; init; }
    public double MinTemperatureCelsius { get; init; }
    public double AvgTemperatureCelsius { get; init; }

    public double MaxWindKph { get; init; }
    public double TotalPrecipitationMm { get; init; }
    public double TotalSnowCm { get; init; }
    public double AvgVisibilityKm { get; init; }
    public int AvgHumidity { get; init; }

    /// <summary>Probability of rain occurring during the day (0–100).</summary>
    public int ChanceOfRain { get; init; }
    /// <summary>Probability of snow occurring during the day (0–100).</summary>
    public int ChanceOfSnow { get; init; }

    public WeatherCondition Condition { get; init; } = null!;
    public double UvIndex { get; init; }

    private DailyForecastSummary() { }

    public static DailyForecastSummary Create(
        double maxTemperatureCelsius,
        double minTemperatureCelsius,
        double avgTemperatureCelsius,
        double maxWindKph,
        double totalPrecipitationMm,
        double totalSnowCm,
        double avgVisibilityKm,
        int avgHumidity,
        int chanceOfRain,
        int chanceOfSnow,
        WeatherCondition condition,
        double uvIndex)
    {
        ArgumentNullException.ThrowIfNull(condition);

        if (avgHumidity is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(avgHumidity), "Humidity must be between 0 and 100.");

        if (chanceOfRain is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(chanceOfRain), "Chance of rain must be between 0 and 100.");

        if (chanceOfSnow is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(chanceOfSnow), "Chance of snow must be between 0 and 100.");

        return new DailyForecastSummary
        {
            MaxTemperatureCelsius = maxTemperatureCelsius,
            MinTemperatureCelsius = minTemperatureCelsius,
            AvgTemperatureCelsius = avgTemperatureCelsius,
            MaxWindKph = maxWindKph,
            TotalPrecipitationMm = totalPrecipitationMm,
            TotalSnowCm = totalSnowCm,
            AvgVisibilityKm = avgVisibilityKm,
            AvgHumidity = avgHumidity,
            ChanceOfRain = chanceOfRain,
            ChanceOfSnow = chanceOfSnow,
            Condition = condition,
            UvIndex = uvIndex
        };
    }
}
