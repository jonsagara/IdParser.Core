using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

internal static class WeightInKilogramsParser
{
    internal static Weight Parse(string input)
    {
        var weight = Convert.ToInt16(input, CultureInfo.InvariantCulture);

        return new Weight(kilograms: weight);
    }
}
