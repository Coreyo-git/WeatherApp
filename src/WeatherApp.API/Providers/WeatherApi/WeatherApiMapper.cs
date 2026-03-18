using System.Globalization;
using WeatherApp.API.Domain.Models;
using WeatherApp.API.Domain.ValueObjects;
using WeatherApp.API.Providers.WeatherApi.Dto;

namespace WeatherApp.API.Providers.WeatherApi;

internal static class WeatherApiMapper
{
	internal static CitySearchResult MapToCitySearchResult(WeatherApiCitySearchResponse response)
	{
		return CitySearchResult.Create(
			response.Id,
			response.Name,
			response.Region,
			response.Country,
			response.Latitude,
			response.Longitude);
	}

    internal static WeatherForecast MapToForecast(WeatherApiForecastResponse response)
    {
        var location = MapLocation(response.Location);
        var current = MapSnapshot(response.Current);
        var days = response.Forecast.Forecastday.Select(MapForecastDay).ToList();

        return WeatherForecast.Create(location, current, days[0], days);
    }

    private static Location MapLocation(WeatherApiLocationDto dto) =>
        Location.Create(dto.Lat, dto.Lon, dto.Name, dto.Region, dto.Country, dto.TzId);

    private static WeatherSnapshot MapSnapshot(WeatherApiCurrentDto dto) =>
        WeatherSnapshot.Create(
            time: DateTimeOffset.FromUnixTimeSeconds(dto.LastUpdatedEpoch),
            isDay: dto.IsDay == 1,
            condition: MapCondition(dto.Condition),
            temperatureCelsius: dto.TempC,
            feelsLikeCelsius: dto.FeelslikeC,
            windChillCelsius: dto.WindchillC,
            heatIndexCelsius: dto.HeatindexC,
            dewPointCelsius: dto.DewpointC,
            windSpeedKph: dto.WindKph,
            gustKph: dto.GustKph,
            windDegree: dto.WindDegree,
            windDirection: dto.WindDir,
            pressureMb: dto.PressureMb,
            precipitationMm: dto.PrecipMm,
            cloudCoverage: dto.Cloud,
            humidity: dto.Humidity,
            visibilityKm: dto.VisKm,
            uvIndex: dto.Uv);

    private static WeatherCondition MapCondition(WeatherApiConditionDto dto) =>
        WeatherCondition.Create(dto.Code, dto.Text, dto.Icon);

    private static ForecastDay MapForecastDay(WeatherApiForecastDayDto dto)
    {
        var summary = DailyForecastSummary.Create(
            maxTemperatureCelsius: dto.Day.MaxtempC,
            minTemperatureCelsius: dto.Day.MintempC,
            avgTemperatureCelsius: dto.Day.AvgtempC,
            maxWindKph: dto.Day.MaxwindKph,
            totalPrecipitationMm: dto.Day.TotalprecipMm,
            totalSnowCm: dto.Day.TotalsnowCm,
            avgVisibilityKm: dto.Day.AvgvisKm,
            avgHumidity: dto.Day.Avghumidity,
            chanceOfRain: dto.Day.DailyChanceOfRain,
            chanceOfSnow: dto.Day.DailyChanceOfSnow,
            condition: MapCondition(dto.Day.Condition),
            uvIndex: dto.Day.Uv);

        return ForecastDay.Create(
            date: DateOnly.Parse(dto.Date, CultureInfo.InvariantCulture),
            summary: summary,
            hours: [],
            sunrise: ParseAstroTime(dto.Astro.Sunrise),
            sunset: ParseAstroTime(dto.Astro.Sunset),
            moonrise: ParseAstroTime(dto.Astro.Moonrise),
            moonset: ParseAstroTime(dto.Astro.Moonset),
            moonPhase: dto.Astro.MoonPhase,
            moonIllumination: dto.Astro.MoonIllumination);
    }

	private static TimeOnly ParseAstroTime(string time) =>
		TimeOnly.ParseExact(time, "h:mm tt", CultureInfo.InvariantCulture);
}
