using WeatherApp.API.Providers;

namespace WeatherApp.API.Endpoints;

internal static class CitySearchEndpoint
{
	internal static IEndpointRouteBuilder MapCitySearchEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/cities", async (string? query, IWeatherProvider provider, CancellationToken ct) =>
		{
			if (string.IsNullOrWhiteSpace(query))
				return Results.BadRequest("Provide a city name via 'query'.");

			var cityQuery = CitySearchQuery.For(query);
			var cities = await provider.SearchCitiesAsync(cityQuery, ct);
			return Results.Ok(cities);
		});

		return app;
	}
}
