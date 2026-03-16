using FluentAssertions;
using WeatherApp.API.Providers;

namespace WeatherApp.API.Tests.Providers;

public class WeatherQueryTests
{
    [Fact]
    public void WithCityName_SetsQuery()
    {
        var query = WeatherQuery.For("Brisbane");

        query.Query.Should().Be("Brisbane");
        query.Lang.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void WithEmptyInput_Throws(string input)
    {
        Action act = () => WeatherQuery.For(input);

        act.Should().Throw<ArgumentException>().WithParameterName("input");
    }

    [Fact]
	
    public void WithLanguage_SetsLang()
    {
        var query = WeatherQuery.For("Brisbane", Language.French);

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
