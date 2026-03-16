using WeatherApp.API.Providers;

namespace WeatherApp.API.Endpoints;

internal static class ForecastEndpoints
{
    internal static IEndpointRouteBuilder MapForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/forecast", async (string? query, IWeatherProvider provider, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(query))
                return Results.BadRequest("Provide a city name via 'query'.");

            var weatherQuery = WeatherQuery.For(query);
            var forecast = await provider.GetForecastAsync(weatherQuery, ct);
            return Results.Ok(forecast);
        });

        return app;
    }
}
