using FluentAssertions;
using WeatherApp.API.Domain;
using WeatherApp.API.Domain.Models;
using WeatherApp.API.Domain.ValueObjects;

namespace WeatherApp.API.Tests.Domain.Models;

public class CitySearchResultTests
{
	[Fact]
	public void Create_WithValidArguments_ReturnsCitySearchResults()
	{
		var citySearchResult = CitySearchResult.Create(1, "Brisbane", "Queensland", "Australia", -27.47, 153.02);

		citySearchResult.Id.Should().Be(1);
		citySearchResult.Name.Should().Be("Brisbane");
		citySearchResult.Region.Should().Be("Queensland");
		citySearchResult.Country.Should().Be("Australia");
		citySearchResult.Latitude.Should().Be(-27.47);
		citySearchResult.Longitude.Should().Be(153.02);
	}

	[Theory]
	[InlineData(-91)]
	[InlineData(91)]
	public void Create_WithInvalidLatitude_ThrowsArgumentOutOfRangeException(double latitude)
	{
		Action act = () => CitySearchResult.Create(1, "Brisbane", "Queensland", "Australia", latitude, 153.02);

		act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("latitude");
	}

	[Theory]
	[InlineData(-181)]
	[InlineData(181)]
	public void Create_WithInvalidLongitude_ThrowsArgumentOutOfRangeException(double longitude)
	{
		Action act = () => CitySearchResult.Create(1, "Brisbane", "Queensland", "Australia", - 27.47, longitude);

		act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("longitude");
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Create_WithEmptyName_ThrowsArgumentException(string name)
	{
		Action act = () => CitySearchResult.Create(1, name, "Queensland", "Australia", -27.47, 153.02);

		act.Should().Throw<ArgumentException>().WithParameterName("name");
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Create_WithEmptyRegion_ReturnsCitySearchResult(string region)
	{
		var citySearchResult = CitySearchResult.Create(1, "Brisbane", region, "Australia", -27.47, 153.02);

		citySearchResult.Region.Should().Be(region);
	}

	[Theory]
	[InlineData(90)]
	[InlineData(-90)]
	public void Create_WithBoundaryLatitude_ReturnsCitySearchResult(double latitude)
	{
		var citySearchResult = CitySearchResult.Create(1, "Brisbane", "Queensland", "Australia", latitude, 153.02);

		citySearchResult.Latitude.Should().Be(latitude);
	}

	[Theory]
	[InlineData(180)]
	[InlineData(-180)]
	public void Create_WithBoundaryLongitude_ReturnsCitySearchResult(double longitude)
	{
		var citySearchResult = CitySearchResult.Create(1, "Brisbane", "Queensland", "Australia", -27.47, longitude);

		citySearchResult.Longitude.Should().Be(longitude);
	}
}