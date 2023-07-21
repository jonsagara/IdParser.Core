using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class WeightRangeParser
{
    internal static WeightRange Parse(string input)
    {
#warning TODO: Enum.TryParse?
        var weightRange = (WeightRange)int.Parse(input.AsSpan(), provider: CultureInfo.InvariantCulture);

        return weightRange;
    }
}
