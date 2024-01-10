using System.Globalization;
using IdParser.Core.Constants;

namespace IdParser.Core.Parsers.Id;

internal static class HeightParser
{
    internal static Field<Height?> Parse(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (string.IsNullOrEmpty(rawValue) || rawValue.Length < 3)
        {
            return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height from field '{SubfileElementIds.Height}': the field has no value, or has less than 3 characters: '{rawValue}'");
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            var feetSpan = rawValue.AsSpan(start: 0, length: 1);
            if (!int.TryParse(feetSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int feet))
            {
                return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height Feet from field '{SubfileElementIds.Height}': '{feetSpan}' is not a valid integer.");
            }

            var inchesSpan = rawValue.AsSpan(start: 1, length: 2);
            if (!int.TryParse(inchesSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int inches))
            {
                return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height Inches from field '{SubfileElementIds.Height}': '{inchesSpan}' is not a valid integer.");
            }

            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(feet: feet, inches: inches), rawValue: rawValue);
        }

        if (int.TryParse(rawValue.AsSpan(start: 0, length: rawValue.Length - 2), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int height))
        {
            return rawValue.Contains("cm", StringComparison.OrdinalIgnoreCase)
                ? FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(centimeters: height), rawValue: rawValue)
                : FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(totalInches: height), rawValue: rawValue);
        }

        return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Height from field '{SubfileElementIds.Height}': '{rawValue}'.");
    }
}
