using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class HeightParser
{
    internal static Field<Height?> Parse(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (string.IsNullOrEmpty(rawValue) || rawValue.Length < 3)
        {
            return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, "Height field has no value, or has less than 3 characters, and can't be parsed.");
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            var feetSpan = rawValue.AsSpan(start: 0, length: 1);
            if (!int.TryParse(feetSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int feet))
            {
                return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height feet from '{feetSpan}'.");
            }

            var inchesSpan = rawValue.AsSpan(start: 1, length: 2);
            if (!int.TryParse(inchesSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int inches))
            {
                return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height inches from '{inchesSpan}'.");
            }

            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(feet: feet, inches: inches), rawValue: rawValue);
        }

        if (int.TryParse(rawValue.AsSpan(start: 0, length: rawValue.Length - 2), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int height))
        {
            return rawValue.Contains("cm", StringComparison.OrdinalIgnoreCase)
                ? FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(centimeters: height), rawValue: rawValue)
                : FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(totalInches: height), rawValue: rawValue);
        }

        return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Height from '{rawValue}'.");
    }
}
