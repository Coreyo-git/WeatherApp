namespace WeatherApp.API.Domain.Models;

public sealed record CitySearchResult
{
	public int Id { get; init; }
	/// <summary>The city or town name, e.g. "New York".</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>The state or province, e.g. "New York" or "Ontario". May be empty for some regions.</summary>
	public string Region { get; init; } = string.Empty;
	public string Country { get; init; } = string.Empty;
	public double Latitude { get; init; }
	public double Longitude { get; init; }


	private CitySearchResult() { }

	public static CitySearchResult Create(int id, string name, string region, string country, double latitude, double longitude)
	{
		if (latitude is < -90 or > 90)
			throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");

		if (longitude is < -180 or > 180)
			throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Location name is required.", nameof(name));

		return new CitySearchResult
		{
			Id = id,
			Latitude = latitude,
			Longitude = longitude,
			Name = name,
			Region = region,
			Country = country,
		};
	}
}