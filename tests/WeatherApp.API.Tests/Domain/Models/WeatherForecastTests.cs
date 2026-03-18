using FluentAssertions;
using WeatherApp.API.Domain;
using WeatherApp.API.Domain.Models;
using WeatherApp.API.Domain.ValueObjects;

namespace WeatherApp.API.Tests.Domain.Models;

public class WeatherForecastTests
{
    [Fact]
    public void Create_WithValidArguments_ReturnsForecast()
    {
        var location = BuildLocation();
        var current = BuildSnapshot();
        var today = BuildForecastDay();
        var days = new[] { today };

        var forecast = WeatherForecast.Create(location, current, today, days);

        forecast.Location.Should().Be(location);
        forecast.Current.Should().Be(current);
        forecast.Today.Should().Be(today);
        forecast.Days.Should().BeEquivalentTo(days);
    }

    [Fact]
    public void Create_WithNullLocation_Throws()
    {
        Action act = () => WeatherForecast.Create(null!, BuildSnapshot(), BuildForecastDay(), []);

        act.Should().Throw<ArgumentNullException>().WithParameterName("location");
    }

    [Fact]
    public void Create_WithNullCurrent_Throws()
    {
        Action act = () => WeatherForecast.Create(BuildLocation(), null!, BuildForecastDay(), []);

        act.Should().Throw<ArgumentNullException>().WithParameterName("current");
    }

    [Fact]
    public void Create_WithNullToday_Throws()
    {
        Action act = () => WeatherForecast.Create(BuildLocation(), BuildSnapshot(), null!, []);

        act.Should().Throw<ArgumentNullException>().WithParameterName("today");
    }

    [Fact]
    public void Create_WithNullDays_Throws()
    {
        Action act = () => WeatherForecast.Create(BuildLocation(), BuildSnapshot(), BuildForecastDay(), null!);

        act.Should().Throw<ArgumentNullException>().WithParameterName("days");
    }

    // --- builders ---

    private static Location BuildLocation() =>
        Location.Create(-27.5, 153.02, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

    private static WeatherCondition BuildCondition() =>
        WeatherCondition.Create(1003, "Partly cloudy", "//cdn.weatherapi.com/weather/64x64/day/116.png");

    private static WeatherSnapshot BuildSnapshot() =>
        WeatherSnapshot.Create(
            time: DateTimeOffset.UtcNow,
            isDay: true,
            condition: BuildCondition(),
            temperatureCelsius: 27.3,
            feelsLikeCelsius: 28.2,
            windChillCelsius: 25.6,
            heatIndexCelsius: 26.4,
            dewPointCelsius: 15.4,
            windSpeedKph: 15.8,
            gustKph: 18.6,
            windDegree: 77,
            windDirection: "ENE",
            pressureMb: 1018,
            precipitationMm: 0,
            cloudCoverage: 75,
            humidity: 66,
            visibilityKm: 10,
            uvIndex: 7.5);

    private static ForecastDay BuildForecastDay() =>
        ForecastDay.Create(
            date: DateOnly.FromDateTime(DateTime.Today),
            summary: DailyForecastSummary.Create(
                maxTemperatureCelsius: 26.6,
                minTemperatureCelsius: 19.7,
                avgTemperatureCelsius: 22.6,
                maxWindKph: 15.8,
                totalPrecipitationMm: 0.32,
                totalSnowCm: 0,
                avgVisibilityKm: 10,
                avgHumidity: 63,
                chanceOfRain: 86,
                chanceOfSnow: 0,
                condition: BuildCondition(),
                uvIndex: 1.7),
            hours: [],
            sunrise: new TimeOnly(5, 48),
            sunset: new TimeOnly(18, 22));
}
