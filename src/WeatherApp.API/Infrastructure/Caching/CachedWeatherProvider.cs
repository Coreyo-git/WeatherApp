using Microsoft.Extensions.Caching.Memory;
using WeatherApp.API.Domain.Models;
using WeatherApp.API.Providers;

namespace WeatherApp.API.Infrastructure.Caching;

public sealed class CachedWeatherProvider : IWeatherProvider
{
	private readonly IWeatherProvider _inner;
	private readonly IMemoryCache _cache;
	private readonly ILogger<CachedWeatherProvider> _logger;

	public CachedWeatherProvider(IWeatherProvider inner, IMemoryCache cache, ILogger<CachedWeatherProvider> logger)
	{
		_inner = inner;
		_cache = cache;
		_logger = logger;
	}

	public async Task<WeatherForecast> GetForecastAsync(WeatherQuery query, CancellationToken cancellationToken = default)
	{
		if(query.Query.StartsWith("lat:"))
		{
			_logger.LogDebug("Forecast cache SKIP for lat/lon query '{Query}'", query.Query);
			return await _inner.GetForecastAsync(query, cancellationToken);
		}
		var forecastKey = $"forecast:{query.Query}";
		var forecastHit = _cache.TryGetValue(forecastKey, out _);
		_logger.LogDebug("Forecast cache {Result} for query '{Query}'", forecastHit ? "HIT" : "MISS", query.Query);

		WeatherForecast weatherForecast = (await _cache.GetOrCreateAsync(forecastKey,
			async cacheEntry =>
			{
				var weatherFromInner = await _inner.GetForecastAsync(query, cancellationToken);

				// configure invalidation
				cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);

				// The value to cache is the return of the delegate
				return weatherFromInner;
			}))!;

		return weatherForecast;
	}

	public async Task<IReadOnlyList<CitySearchResult>> SearchCitiesAsync(CitySearchQuery query, CancellationToken cancellationToken = default)
	{
		IReadOnlyList<CitySearchResult> citySearchResults = (await _cache.GetOrCreateAsync($"cities:{query.Query}",
			async cacheEntry =>
			{
				var cityResultsFromInner = await _inner.SearchCitiesAsync(query, cancellationToken);

				// configure invalidation
				cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);

				// The value to cache is the return of the delegate
				return cityResultsFromInner;
			}))!;

		return citySearchResults;
	}
}