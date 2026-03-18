using FluentAssertions;
using WeatherApp.API.Providers;

namespace WeatherApp.API.Tests.Providers;

public class WeatherQueryTests
{
    [Fact]
    public void ForId_SetsQueryWithIdPrefix()
    {
        var query = WeatherQuery.For(12345);

        query.Query.Should().Be("id:12345");
        query.Lang.Should().BeNull();
    }

    [Fact]
    public void ForCoordinates_SetsQueryAsLatLon()
    {
        var query = WeatherQuery.For(-27.5, 153.02);

        query.Query.Should().Be("-27.5,153.02");
        query.Lang.Should().BeNull();
    }

    [Fact]
    public void ForId_WithLanguage_SetsLang()
    {
        var query = WeatherQuery.For(12345, Language.French);

        query.Lang.Should().Be(Language.French);
    }

    [Fact]
    public void ForCoordinates_WithLanguage_SetsLang()
    {
        var query = WeatherQuery.For(-27.5, 153.02, Language.French);

        query.Lang.Should().Be(Language.French);
    }
}

public class LanguageExtensionsTests
{
    [Theory]
    [InlineData(Language.French, "fr")]
    [InlineData(Language.German, "de")]
    [InlineData(Language.ChineseTraditional, "zh_tw")]
    [InlineData(Language.WuShanghainese, "zh_wuu")]
    [InlineData(Language.YueCantonese, "zh_yue")]
    [InlineData(Language.Mandarin, "zh_cmn")]
    [InlineData(Language.Arabic, "ar")]
    [InlineData(Language.Zulu, "zu")]
    public void ToLangCode_ReturnsExpectedCode(Language language, string expectedCode)
    {
        language.ToLangCode().Should().Be(expectedCode);
    }
}
