using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class WeightRangeParser
{
    internal static Field<WeightRange?> Parse2(string elementId, string rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<WeightRange?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return int.TryParse(rawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int weightRange)
            ? FieldHelpers.ParsedField(elementId: elementId, value: (WeightRange?)weightRange, rawValue: rawValue)
            : FieldHelpers.UnparsedField<WeightRange?>(elementId: elementId, rawValue: rawValue, error: $"Weight range '{rawValue}' not supported by enum {nameof(WeightRange)}.");
    }
}
