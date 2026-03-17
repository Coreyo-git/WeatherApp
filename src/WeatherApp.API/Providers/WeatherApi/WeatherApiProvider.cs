using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherApp.API.Domain.Models;
using WeatherApp.API.Infrastructure.Configuration;
using WeatherApp.API.Providers.WeatherApi.Dto;

namespace WeatherApp.API.Providers.WeatherApi;

internal sealed class WeatherApiProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    private readonly WeatherApiOptions _options;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

	public WeatherApiProvider(HttpClient httpClient, IOptions<WeatherApiOptions> options)
	{
		_httpClient = httpClient;
		_options = options.Value;
	}

	
	// TODO Handle Response codes from WeatherAPI
	// TODO Logging in general 
	public async Task<WeatherForecast> GetForecastAsync(WeatherQuery query, CancellationToken cancellationToken = default)
	{
		var url = $"forecast.json?key={_options.ApiKey}&q=id:{query.Query}&days=7&aqi=no&alerts=no";

		var response = await _httpClient.GetFromJsonAsync<WeatherApiForecastResponse>(url, JsonOptions, cancellationToken)
			?? throw new InvalidOperationException("WeatherAPI forecast returned an empty response.");

		return WeatherApiMapper.MapToForecast(response);
	}
	
	public async Task<IReadOnlyList<CitySearchResult>> SearchCitiesAsync(CitySearchQuery query, CancellationToken cancellationToken = default)
	{
		var url = $"search.json?key={_options.ApiKey}&q={query.Query}";

		var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<WeatherApiCitySearchResponse>>(url, JsonOptions, cancellationToken)
			?? throw new InvalidOperationException("WeatherAPI city search returned an empty response.");

		IReadOnlyList<CitySearchResult> citySearchResults = response
			.Select(r => WeatherApiMapper.MapToCitySearchResult(r)).ToList();

		return citySearchResults;
	}
}
