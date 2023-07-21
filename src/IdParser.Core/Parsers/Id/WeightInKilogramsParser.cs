using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class WeightInKilogramsParser
{
    internal static Weight Parse(string input)
    {
        var weight = short.Parse(input.AsSpan(), provider: CultureInfo.InvariantCulture);

        return new Weight(kilograms: weight);
    }
}
