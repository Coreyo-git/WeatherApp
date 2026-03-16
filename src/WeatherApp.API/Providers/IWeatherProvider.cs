using WeatherApp.API.Domain;

namespace WeatherApp.API.Providers;

public interface IWeatherProvider
{
    Task<WeatherForecast> GetForecastAsync(WeatherQuery query, CancellationToken cancellationToken = default);
}
