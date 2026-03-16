using FluentAssertions;
using WeatherApp.API.Domain;

namespace WeatherApp.API.Tests.Domain.ValueObjects;

public class LocationTests
{
    [Fact]
    public void Create_WithValidArguments_ReturnsLocation()
    {
        var location = Location.Create(-27.47, 153.02, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

        location.Latitude.Should().Be(-27.47);
        location.Longitude.Should().Be(153.02);
        location.Name.Should().Be("Brisbane");
        location.Region.Should().Be("Queensland");
        location.Country.Should().Be("Australia");
        location.TimeZoneId.Should().Be("Australia/Brisbane");
    }

    [Theory]
    [InlineData(-91)]
    [InlineData(91)]
    public void Create_WithInvalidLatitude_ThrowsArgumentOutOfRangeException(double latitude)
    {
        Action act = () => Location.Create(latitude, 153.02, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("latitude");
    }

    [Theory]
    [InlineData(-181)]
    [InlineData(181)]
    public void Create_WithInvalidLongitude_ThrowsArgumentOutOfRangeException(double longitude)
    {
        Action act = () => Location.Create(-27.47, longitude, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("longitude");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyName_ThrowsArgumentException(string name)
    {
        Action act = () => Location.Create(-27.47, 153.02, name, "Queensland", "Australia", "Australia/Brisbane");

        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyTimeZoneId_ThrowsArgumentException(string timeZoneId)
    {
        Action act = () => Location.Create(-27.47, 153.02, "Brisbane", "Queensland", "Australia", timeZoneId);

        act.Should().Throw<ArgumentException>().WithParameterName("timeZoneId");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyRegion_ReturnsLocation(string region)
    {
        var location = Location.Create(-27.47, 153.02, "Brisbane", region, "Australia", "Australia/Brisbane");

        location.Region.Should().Be(region);
    }

    [Theory]
    [InlineData(90)]
    [InlineData(-90)]
    public void Create_WithBoundaryLatitude_ReturnsLocation(double latitude)
    {
        var location = Location.Create(latitude, 153.02, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

        location.Latitude.Should().Be(latitude);
    }

    [Theory]
    [InlineData(180)]
    [InlineData(-180)]
    public void Create_WithBoundaryLongitude_ReturnsLocation(double longitude)
    {
        var location = Location.Create(-27.47, longitude, "Brisbane", "Queensland", "Australia", "Australia/Brisbane");

        location.Longitude.Should().Be(longitude);
    }
}
