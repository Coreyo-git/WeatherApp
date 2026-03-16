namespace WeatherApp.API.Domain;

public sealed record Location
{
	/// <summary>The city or town name, e.g. "New York".</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>The state or province, e.g. "New York" or "Ontario". May be empty for some regions.</summary>
	public string Region { get; init; } = string.Empty;
	public string Country { get; init; } = string.Empty;
	public double Latitude { get; init; }
	public double Longitude { get; init; }
	
	/// <summary>IANA timezone identifier for this location, e.g. "Australia/Brisbane".</summary>
	public string TimeZoneId { get; init; } = string.Empty;

	private Location() { }

	public static Location Create(double latitude, double longitude, string name, string region, string country, string timeZoneId)
	{
		if (latitude is < -90 or > 90)
			throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");

		if (longitude is < -180 or > 180)
			throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Location name is required.", nameof(name));

		if (string.IsNullOrWhiteSpace(timeZoneId))
			throw new ArgumentException("TimeZoneId is required.", nameof(timeZoneId));

		return new Location
		{
			Latitude = latitude,
			Longitude = longitude,
			Name = name,
			Region = region,
			Country = country,
			TimeZoneId = timeZoneId
		};
	}
}