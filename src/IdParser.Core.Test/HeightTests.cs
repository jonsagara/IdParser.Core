using IdParser.Core.Test.Extensions;
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


    [Fact]
    public void FeetInchesToTotalInchesTest()
    {
        var height = new Height(feet: 5, inches: 9);

        Assert.Equal(69, height.TotalInches);
    }

    [Fact]
    public void TotalInchesToFeetInchesTest()
    {
        var height = new Height(totalInches: 69);

        Assert.Equal(5, height.FeetAndInches.Feet);
        Assert.Equal(9, height.FeetAndInches.Inches);
    }

    [Fact]
    public void FeetInchesToCentimetersTest()
    {
        var height = new Height(totalInches: 69);

        var expectedCm = 175.3;

        Assert.True(height.Centimeters.Equals1DigitPrecision(expectedCm));
    }

    [Fact]
    public void TotalInchesToFeetInches_ThreeFeet()
    {
        var height = new Height(totalInches: 36);

        Assert.Equal(3, height.FeetAndInches.Feet);
        Assert.Equal(0, height.FeetAndInches.Inches);
    }
}
