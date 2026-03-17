namespace WeatherApp.API.Domain.ValueObjects;

/// <summary>The condition at a point in time — shared across current, hourly, and daily forecasts.</summary>
public sealed record WeatherCondition
{
    /// <summary>WeatherAPI condition code, e.g. 1003 for "Partly Cloudy".</summary>
    public int Code { get; init; }

    /// <summary>Human-readable description, e.g. "Partly cloudy".</summary>
    public string Text { get; init; } = string.Empty;

    /// <summary>URL for the condition icon, e.g. "//cdn.weatherapi.com/weather/64x64/day/116.png".</summary>
    public string IconUrl { get; init; } = string.Empty;

    private WeatherCondition() { }

    public static WeatherCondition Create(int code, string text, string iconUrl)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Condition text is required.", nameof(text));

        if (string.IsNullOrWhiteSpace(iconUrl))
            throw new ArgumentException("Condition icon URL is required.", nameof(iconUrl));

        return new WeatherCondition
        {
            Code = code,
            Text = text,
            IconUrl = iconUrl
        };
    }
}
