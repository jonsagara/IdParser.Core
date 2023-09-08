using Xunit;

namespace IdParser.Core.Test;

public class HeightTests
{
    [Fact]
    public void EqualityTest()
    {
        var left = new Height(totalInches: 65);
        var right = new Height(totalInches: 65);

        Assert.Equal(left, right);
    }

    // I don't see the value in making Height implement IComparable.
    //[Fact]
    //public void ComparableTest()
    //{
    //    var first = new Height(feet: 6, inches: 2);
    //    var second = new Height(feet: 5, inches: 8);

    //    Assert.True(first.CompareTo(second) > 0);
    //    Assert.True(second.CompareTo(first) < 0);
    //}

    // Height no longer has a custom ToString() overload.
    //[Fact]
    //public void ImperialDisplayTest()
    //{
    //    var height = new Height(inches: 67);
    //    var actual = height.ToString();

    //    Assert.Equal("5'7\"", actual);
    //}

    //[Fact]
    //public void MetricDisplayTest()
    //{
    //    var height = new Height(centimeters: 175);
    //    var actual = height.ToString();

    //    Assert.Equal("175 cm", actual);
    //}

    //[Fact]
    //public void RoundingTest()
    //{
    //    var height = new Height(inches: 62);
    //    var actual = height.ToString();

    //    Assert.Equal("5'2\"", actual);
    //}
}
