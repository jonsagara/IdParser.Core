namespace IdParser.Core.Test.Extensions;

internal static class DoubleExtensions
{
    private const double _1Digit = 0.1;

    internal static bool Equals1DigitPrecision(this double value, double comparison)
    {
        var difference = Math.Abs(value - comparison);
        return difference < _1Digit;
    }
}
