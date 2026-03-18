using FluentAssertions;
using WeatherApp.API.Providers.WeatherApi;
using WeatherApp.API.Providers.WeatherApi.Dto;

namespace WeatherApp.API.Tests.Providers;

public class WeatherApiMapperTests
{
    private static WeatherApiForecastResponse BuildSampleResponse() => new(
        Location: new WeatherApiLocationDto(
            Name: "Brisbane",
            Region: "Queensland",
            Country: "Australia",
            Lat: -27.5,
            Lon: 153.0167,
            TzId: "Australia/Brisbane"),
        Current: new WeatherApiCurrentDto(
            LastUpdatedEpoch: 1773634500,
            TempC: 27.3,
            IsDay: 1,
            Condition: new WeatherApiConditionDto("Partly cloudy", "//cdn.weatherapi.com/weather/64x64/day/116.png", 1003),
            WindKph: 15.8,
            WindDegree: 77,
            WindDir: "ENE",
            PressureMb: 1018,
            PrecipMm: 0,
            Humidity: 66,
            Cloud: 75,
            FeelslikeC: 28.2,
            WindchillC: 25.6,
            HeatindexC: 26.4,
            DewpointC: 15.4,
            VisKm: 10,
            Uv: 7.5,
            GustKph: 18.6),
        Forecast: new WeatherApiForecastDto([
            new WeatherApiForecastDayDto(
                Date: "2026-03-16",
                Day: new WeatherApiDayDto(
                    MaxtempC: 26.6,
                    MintempC: 19.7,
                    AvgtempC: 22.6,
                    MaxwindKph: 15.8,
                    TotalprecipMm: 0.32,
                    TotalsnowCm: 0,
                    AvgvisKm: 10,
                    Avghumidity: 63,
                    DailyChanceOfRain: 86,
                    DailyChanceOfSnow: 0,
                    Condition: new WeatherApiConditionDto("Patchy rain nearby", "//cdn.weatherapi.com/weather/64x64/day/176.png", 1063),
                    Uv: 1.7),
                Astro: new WeatherApiAstroDto(
                    Sunrise: "05:49 AM",
                    Sunset: "06:04 PM"))
        ]));

    [Fact]
    public void MapToForecast_MapsLocationCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());

        forecast.Location.Name.Should().Be("Brisbane");
        forecast.Location.Region.Should().Be("Queensland");
        forecast.Location.Country.Should().Be("Australia");
        forecast.Location.Latitude.Should().Be(-27.5);
        forecast.Location.Longitude.Should().Be(153.0167);
        forecast.Location.TimeZoneId.Should().Be("Australia/Brisbane");
    }

    [Fact]
    public void MapToForecast_MapsCurrentSnapshotCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());
        var current = forecast.Current;

        current.Time.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1773634500));
        current.IsDay.Should().BeTrue();
        current.TemperatureCelsius.Should().Be(27.3);
        current.FeelsLikeCelsius.Should().Be(28.2);
        current.WindSpeedKph.Should().Be(15.8);
        current.WindDirection.Should().Be("ENE");
        current.Humidity.Should().Be(66);
        current.UvIndex.Should().Be(7.5);
    }

    [Fact]
    public void MapToForecast_MapsCurrentConditionCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());

        forecast.Current.Condition.Code.Should().Be(1003);
        forecast.Current.Condition.Text.Should().Be("Partly cloudy");
    }

    [Fact]
    public void MapToForecast_MapsIsDay_WhenZero_ReturnsFalse()
    {
        var response = BuildSampleResponse() with
        {
            Current = BuildSampleResponse().Current with { IsDay = 0 }
        };

        WeatherApiMapper.MapToForecast(response).Current.IsDay.Should().BeFalse();
    }

    [Fact]
    public void MapToForecast_MapsDailySummaryCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());
        var summary = forecast.Today.Summary;

        summary.MaxTemperatureCelsius.Should().Be(26.6);
        summary.MinTemperatureCelsius.Should().Be(19.7);
        summary.ChanceOfRain.Should().Be(86);
        summary.Condition.Code.Should().Be(1063);
    }

    [Fact]
    public void MapToForecast_MapsAstroTimesCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());
        var today = forecast.Today;

        today.Sunrise.Should().Be(new TimeOnly(5, 49));
        today.Sunset.Should().Be(new TimeOnly(18, 4));
    }

    [Fact]
    public void MapToForecast_MapsDateCorrectly()
    {
        var forecast = WeatherApiMapper.MapToForecast(BuildSampleResponse());

        forecast.Today.Date.Should().Be(new DateOnly(2026, 3, 16));
    }
}
