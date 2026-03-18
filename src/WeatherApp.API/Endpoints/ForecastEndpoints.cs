using WeatherApp.API.Providers;

namespace WeatherApp.API.Endpoints;

internal static class ForecastEndpoints
{
	internal static IEndpointRouteBuilder MapForecastEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/forecast", async (int? id, double? lat, double? lon, IWeatherProvider provider, CancellationToken ct) =>
		{
			WeatherQuery? weatherQuery = id.HasValue ? WeatherQuery.For(id.Value)
					: (lat.HasValue && lon.HasValue) ? WeatherQuery.For(lat.Value, lon.Value)
					: null;

			if (weatherQuery is null)
				return Results.BadRequest("Provide either 'id' or both 'lat' and 'lon'.");

			var forecast = await provider.GetForecastAsync(weatherQuery, ct);
			return Results.Ok(forecast);
		});

		return app;
	}
}
