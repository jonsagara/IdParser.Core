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

        return short.TryParse(rawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out short weight)
            ? FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: new Weight(kilograms: weight), rawValue: rawValue)
            : FieldHelpers.UnparsedField<Weight?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Weight in kilograms from '{rawValue}'");
    }
}
