using FluentAssertions;
using WeatherApp.API.Domain;

namespace WeatherApp.API.Tests.Domain.ValueObjects;

public class WeatherConditionTests
{
    [Fact]
    public void Create_WithValidArguments_ReturnsWeatherCondition()
    {
        var condition = WeatherCondition.Create(1003, "Partly cloudy", "//cdn.weatherapi.com/weather/64x64/day/116.png");

        condition.Code.Should().Be(1003);
        condition.Text.Should().Be("Partly cloudy");
        condition.IconUrl.Should().Be("//cdn.weatherapi.com/weather/64x64/day/116.png");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyText_ThrowsArgumentException(string text)
    {
        Action act = () => WeatherCondition.Create(1003, text, "//cdn.weatherapi.com/weather/64x64/day/116.png");

        act.Should().Throw<ArgumentException>().WithParameterName("text");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyIconUrl_ThrowsArgumentException(string iconUrl)
    {
        Action act = () => WeatherCondition.Create(1003, "Partly cloudy", iconUrl);

        act.Should().Throw<ArgumentException>().WithParameterName("iconUrl");
    }
}
