using Xunit;

namespace IdParser.Core.Test;

public class ValidationTests
{
    [Fact]
    public void InvalidLengthTest()
    {
        Assert.Throws<ArgumentException>(() => Barcode.Parse2("ABC123"));
    }

    [Fact]
    public void InvalidComplianceIndicatorTest()
    {
        Assert.Throws<ArgumentException>(() => Barcode.Parse2(new string('A', 32)));
    }
}
