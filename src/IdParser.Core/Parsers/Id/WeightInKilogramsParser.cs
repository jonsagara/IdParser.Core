using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class WeightInKilogramsParser
{
    internal static Weight Parse(string input)
    {
        var weight = short.Parse(input.AsSpan(), provider: CultureInfo.InvariantCulture);

        return new Weight(kilograms: weight);
    }

    internal static Field<Weight?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        var weight = short.Parse(rawValue.AsSpan(), provider: CultureInfo.InvariantCulture);

        return FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: new Weight(kilograms: weight), rawValue: rawValue);
    }
}
