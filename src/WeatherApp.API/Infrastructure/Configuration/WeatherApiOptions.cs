using System.ComponentModel.DataAnnotations;

namespace WeatherApp.API.Infrastructure.Configuration;

public sealed class WeatherApiOptions
{
	public const string SectionName = "WeatherApi";

	[Required]
	[MinLength(5)]
    public string ApiKey { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = "https://api.weatherapi.com/v1";
}
