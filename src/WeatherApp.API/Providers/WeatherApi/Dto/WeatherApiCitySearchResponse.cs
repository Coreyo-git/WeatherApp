namespace WeatherApp.API.Providers.WeatherApi.Dto;

// 
internal sealed record WeatherApiCitySearchResponse(
	int Id,
	string Name,
	string Region,
	string Country,
	double Latitude,
	double Longitude,
	string Url);