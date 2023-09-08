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

    // I don't see the value in making Weight an IComparable.
    //[Fact]
    //public void ComparableTest()
    //{
    //    var first = new Weight(pounds: 150);
    //    var second = new Weight(pounds: 125);

    //    Assert.True(first.CompareTo(second) > 0);
    //    Assert.True(second.CompareTo(first) < 0);
    //}

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

        Assert.True(weight.Kilograms!.Value.Equals1DigitPrecision(expectedKg));
    }

    [Fact]
    public void PoundsTest()
    {
        // This is internally converted to kg. Ensure it is round-tripped back to the expected value.
        var weight = new Weight(pounds: 175);
        var expected = "175 lbs";

        Assert.Equal(expected, weight.ToString()); ;
    }
}
