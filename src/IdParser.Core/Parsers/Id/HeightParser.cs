using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class HeightParser
{
    internal static Height? Parse(string input, AAMVAVersion version)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 3)
        {
            return null;
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            var feet = int.Parse(input.AsSpan(start: 0, length: 1), provider: CultureInfo.InvariantCulture);
            var inches = int.Parse(input.AsSpan(start: 1, length: 2), provider: CultureInfo.InvariantCulture);

            return new Height(feet: feet, inches: inches);
        }

        var height = int.Parse(input.AsSpan(start: 0, length: input.Length - 2), provider: CultureInfo.InvariantCulture);

        if (input.Contains("cm", StringComparison.OrdinalIgnoreCase))
        {
            return new Height(centimeters: height);
        }

        return new Height(totalInches: height);
    }

    internal static Field<Height?> Parse2(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (string.IsNullOrEmpty(rawValue) || rawValue.Length < 3)
        {
            return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, "Height field has no value, or has less than 3 characters, and can't be parsed.");
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            var feet = int.Parse(rawValue.AsSpan(start: 0, length: 1), provider: CultureInfo.InvariantCulture);
            var inches = int.Parse(rawValue.AsSpan(start: 1, length: 2), provider: CultureInfo.InvariantCulture);

            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(feet: feet, inches: inches), rawValue: rawValue);
        }

        var height = int.Parse(rawValue.AsSpan(start: 0, length: rawValue.Length - 2), provider: CultureInfo.InvariantCulture);

        if (rawValue.Contains("cm", StringComparison.OrdinalIgnoreCase))
        {
            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(centimeters: height), rawValue: rawValue);
        }

        return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(totalInches: height), rawValue: rawValue);
    }
}
