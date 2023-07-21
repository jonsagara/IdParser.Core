using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

internal static class WeightRangeParser
{
    internal static Weight Parse(string input)
    {
        var weightRange = (WeightRange)Convert.ToByte(input, CultureInfo.InvariantCulture);

        return new Weight(weightRange);
    }
}
