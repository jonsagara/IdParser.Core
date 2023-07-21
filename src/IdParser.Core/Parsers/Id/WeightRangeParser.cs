using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

internal static class WeightRangeParser
{
    internal static WeightRange Parse(string input)
    {
#warning TODO: Enum.TryParse?
        var weightRange = (WeightRange)Convert.ToByte(input, CultureInfo.InvariantCulture);

        return weightRange;
    }
}
