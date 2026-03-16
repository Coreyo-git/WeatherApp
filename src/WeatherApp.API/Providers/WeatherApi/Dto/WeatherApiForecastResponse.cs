namespace WeatherApp.API.Providers.WeatherApi.Dto;

internal sealed record WeatherApiForecastResponse(
    WeatherApiLocationDto Location,
    WeatherApiCurrentDto Current,
    WeatherApiForecastDto Forecast);

internal sealed record WeatherApiLocationDto(
    string Name,
    string Region,
    string Country,
    double Lat,
    double Lon,
    string TzId);

internal sealed record WeatherApiCurrentDto(
    long LastUpdatedEpoch,
    double TempC,
    int IsDay,
    WeatherApiConditionDto Condition,
    double WindKph,
    int WindDegree,
    string WindDir,
    double PressureMb,
    double PrecipMm,
    int Humidity,
    int Cloud,
    double FeelslikeC,
    double WindchillC,
    double HeatindexC,
    double DewpointC,
    double VisKm,
    double Uv,
    double GustKph);

internal sealed record WeatherApiConditionDto(string Text, string Icon, int Code);

internal sealed record WeatherApiForecastDto(List<WeatherApiForecastDayDto> Forecastday);

internal sealed record WeatherApiForecastDayDto(
    string Date,
    WeatherApiDayDto Day,
    WeatherApiAstroDto Astro);

internal sealed record WeatherApiDayDto(
    double MaxtempC,
    double MintempC,
    double AvgtempC,
    double MaxwindKph,
    double TotalprecipMm,
    double TotalsnowCm,
    double AvgvisKm,
    int Avghumidity,
    int DailyChanceOfRain,
    int DailyChanceOfSnow,
    WeatherApiConditionDto Condition,
    double Uv);

internal sealed record WeatherApiAstroDto(
    string Sunrise,
    string Sunset,
    string Moonrise,
    string Moonset,
    string MoonPhase,
    int MoonIllumination);
