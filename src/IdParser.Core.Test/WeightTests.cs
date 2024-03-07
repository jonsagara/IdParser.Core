using IdParser.Core.Test.Extensions;
using Xunit;

namespace IdParser.Core.Test;

public class WeightTests
{
    [Fact]
    public void EqualityFromPoundsTest()
    {
        var left = new Weight(pounds: 120);
        var right = new Weight(pounds: 120);

        Assert.Equal(left, right);
        Assert.NotSame(left, right);
    }

    [Fact]
    public void ImperialDisplayTest()
    {
        var weight = new Weight(pounds: 115);
        var actual = weight.ToString();

        Assert.Equal("115 lbs", actual);
    }

    [Fact]
    public void RangeDisplayTest()
    {
        var weightRange = WeightRange.Lbs161To190;
        var actual = weightRange.GetDescriptionOrDefault();

        Assert.Equal("161-190 lbs (71-86 kg)", actual);
    }

    [Fact]
    public void MetricDisplayTest()
    {
        var weight = new Weight(kilograms: 33);
        var actual = weight.ToString();

        Assert.Equal("33 kg", actual);
    }

    [Fact]
    public void PoundsToKilogramsTest()
    {
        var weight = new Weight(pounds: 175);
        var expectedKg = 79.4;

        Assert.True(weight.Kilograms.Equals1DigitPrecision(expectedKg));
    }

    [Fact]
    public void PoundsTest()
    {
        var weight = new Weight(pounds: 175);
        var expected = 175;

        Assert.Equal(expected, weight.Pounds);
    }

    [Fact]
    public void PoundsConvertedTest()
    {
        var weight = new Weight(kilograms: 79.4);
        var expected = 175;

        Assert.Equal(expected, weight.Pounds);
    }
}
