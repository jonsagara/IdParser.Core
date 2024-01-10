using System.Globalization;
using IdParser.Core.Constants;

namespace IdParser.Core.Parsers.Id;

internal static class WeightRangeParser
{
    internal static Field<WeightRange?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<WeightRange?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return int.TryParse(rawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int weightRange)
            ? FieldHelpers.ParsedField(elementId: elementId, value: (WeightRange?)weightRange, rawValue: rawValue)
            : FieldHelpers.UnparsedField<WeightRange?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Weight Range from field '{SubfileElementIds.WeightRange}': '{rawValue}' is not supported by enum {nameof(WeightRange)}.");
    }
}
