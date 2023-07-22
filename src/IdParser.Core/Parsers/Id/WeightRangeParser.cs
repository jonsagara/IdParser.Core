using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class WeightRangeParser
{
    internal static WeightRange? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        var weightRange = (WeightRange)int.Parse(input.AsSpan(), provider: CultureInfo.InvariantCulture);

        return weightRange;
    }
}
