using Microsoft.Extensions.Options;
using WeatherApp.API.Endpoints;
using WeatherApp.API.Infrastructure.Configuration;
using WeatherApp.API.Providers;
using WeatherApp.API.Providers.WeatherApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddOptions<WeatherApiOptions>()
    .BindConfiguration(WeatherApiOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddHttpClient<IWeatherProvider, WeatherApiProvider>((sp, client) =>
    {
        var options = sp.GetRequiredService<IOptions<WeatherApiOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseUrl + "/");
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapForecastEndpoints();
app.MapCitySearchEndpoints();

app.Run();
