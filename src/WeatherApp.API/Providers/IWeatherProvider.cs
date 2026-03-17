using WeatherApp.API.Domain;
using WeatherApp.API.Domain.Models;

namespace WeatherApp.API.Providers;

public interface IWeatherProvider
{
	Task<WeatherForecast> GetForecastAsync(WeatherQuery query, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<CitySearchResult>> SearchCitiesAsync(CitySearchQuery query, CancellationToken cancellationToken = default);
}
