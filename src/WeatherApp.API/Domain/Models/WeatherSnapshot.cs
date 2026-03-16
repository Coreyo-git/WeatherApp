namespace WeatherApp.API.Domain;

/// <summary>
/// Weather at a specific point in time. Used for both current conditions and individual hourly entries.
/// Rain/snow probability fields are only populated for hourly forecasts, not current conditions.
/// </summary>
public sealed record WeatherSnapshot
{
    public DateTimeOffset Time { get; init; }
    public bool IsDay { get; init; }
    public WeatherCondition Condition { get; init; } = null!;

    public double TemperatureCelsius { get; init; }
    public double FeelsLikeCelsius { get; init; }
    public double WindChillCelsius { get; init; }
    public double HeatIndexCelsius { get; init; }
    public double DewPointCelsius { get; init; }

    public double WindSpeedKph { get; init; }
    public double GustKph { get; init; }
    /// <summary>Wind direction in degrees (0–360).</summary>
    public int WindDegree { get; init; }
    /// <summary>Cardinal wind direction, e.g. "ENE".</summary>
    public string WindDirection { get; init; } = string.Empty;

    public double PressureMb { get; init; }
    public double PrecipitationMm { get; init; }
    /// <summary>Percentage of sky covered by cloud (0–100).</summary>
    public int CloudCoverage { get; init; }
    /// <summary>Relative humidity percentage (0–100).</summary>
    public int Humidity { get; init; }
    public double VisibilityKm { get; init; }
    public double UvIndex { get; init; }

    /// <summary>Hourly only. Probability of rain (0–100).</summary>
    public int? ChanceOfRain { get; init; }
    /// <summary>Hourly only. Probability of snow (0–100).</summary>
    public int? ChanceOfSnow { get; init; }

    private WeatherSnapshot() { }

    public static WeatherSnapshot Create(
        DateTimeOffset time,
        bool isDay,
        WeatherCondition condition,
        double temperatureCelsius,
        double feelsLikeCelsius,
        double windChillCelsius,
        double heatIndexCelsius,
        double dewPointCelsius,
        double windSpeedKph,
        double gustKph,
        int windDegree,
        string windDirection,
        double pressureMb,
        double precipitationMm,
        int cloudCoverage,
        int humidity,
        double visibilityKm,
        double uvIndex,
        int? chanceOfRain = null,
        int? chanceOfSnow = null)
    {
        ArgumentNullException.ThrowIfNull(condition);

        if (string.IsNullOrWhiteSpace(windDirection))
            throw new ArgumentException("Wind direction is required.", nameof(windDirection));

        if (windDegree is < 0 or > 360)
            throw new ArgumentOutOfRangeException(nameof(windDegree), "Wind degree must be between 0 and 360.");

        if (humidity is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(humidity), "Humidity must be between 0 and 100.");

        if (cloudCoverage is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(cloudCoverage), "Cloud coverage must be between 0 and 100.");

        return new WeatherSnapshot
        {
            Time = time,
            IsDay = isDay,
            Condition = condition,
            TemperatureCelsius = temperatureCelsius,
            FeelsLikeCelsius = feelsLikeCelsius,
            WindChillCelsius = windChillCelsius,
            HeatIndexCelsius = heatIndexCelsius,
            DewPointCelsius = dewPointCelsius,
            WindSpeedKph = windSpeedKph,
            GustKph = gustKph,
            WindDegree = windDegree,
            WindDirection = windDirection,
            PressureMb = pressureMb,
            PrecipitationMm = precipitationMm,
            CloudCoverage = cloudCoverage,
            Humidity = humidity,
            VisibilityKm = visibilityKm,
            UvIndex = uvIndex,
            ChanceOfRain = chanceOfRain,
            ChanceOfSnow = chanceOfSnow
        };
    }
}
