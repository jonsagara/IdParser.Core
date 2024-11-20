using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using IdParser.Core.Constants;

namespace IdParser.Core.Parsers.Id;

internal static class HeightParser
{
    internal static Field<Height?> Parse(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            // #28: For 2.0.0, I changed this to return an unparsed field error noting that the field had no value or 
            //   less than 3 characters. AAMVA says that DAU is required, and that when there is no data, it should be
            //   encoded as NONE or unavl. MI does neither of those, and just leaves the field blank.
            // Split this into two separate checks: no value is not an error; return null.
            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (rawValue.Length < 3)
        {
            // #28: ... but we can't parse values that are less than 3 characters in length, so that's still an error.
            return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Height from field '{SubfileElementIds.Height}': the field has less than 3 characters: '{rawValue}'");
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            if (!TryParseAAMVA2000Height(rawValue, out int? feet, out int? inches, out string? parseError))
            {
                return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, error: parseError);
            }

            return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(feet: feet.Value, inches: inches.Value), rawValue: rawValue);
        }

        if (int.TryParse(rawValue.AsSpan(start: 0, length: rawValue.Length - 2), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int height))
        {
            // #31: Even though AAMVA > 2000 is not supposed to have height as Feet and Inches mashed together, apparently
            //   NY didn't get the memo because we got a report of an AAMVA 2005 NY license with the AAMVA 2000 height
            //   formatting. Explicitly check for both cm and in, and if it's neither, try to parse it as AAMVA 2000.

            if (rawValue.Contains("cm", StringComparison.OrdinalIgnoreCase))
            {
                return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(centimeters: height), rawValue: rawValue);
            }

            if (rawValue.Contains("in", StringComparison.OrdinalIgnoreCase))
            {
                return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(totalInches: height), rawValue: rawValue);
            }

            if (TryParseAAMVA2000Height(rawValue, out int? feet, out int? inches, out string? parseError))
            {
                return FieldHelpers.ParsedField<Height?>(elementId: elementId, value: new Height(feet: feet.Value, inches: inches.Value), rawValue: rawValue);
            }

            return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, error: parseError);
        }

        return FieldHelpers.UnparsedField<Height?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Height from field '{SubfileElementIds.Height}': '{rawValue}'.");
    }


    //
    // Private methods
    //

    private static bool TryParseAAMVA2000Height(string rawValue, [NotNullWhen(true)] out int? feet, [NotNullWhen(true)] out int? inches, [NotNullWhen(false)] out string? parseError)
    {
        var feetSpan = rawValue.AsSpan(start: 0, length: 1);
        if (!int.TryParse(feetSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int parsedFeet))
        {
            feet = null;
            inches = null;
            parseError = $"Unable to parse Height Feet from field '{SubfileElementIds.Height}': '{feetSpan}' is not a valid integer.";

            return false;
        }

        var inchesSpan = rawValue.AsSpan(start: 1, length: 2);
        if (!int.TryParse(inchesSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out int parsedInches))
        {
            feet = null;
            inches = null;
            parseError = $"Unable to parse Height Inches from field '{SubfileElementIds.Height}': '{inchesSpan}' is not a valid integer.";

            return false;
        }

        feet = parsedFeet;
        inches = parsedInches;
        parseError = null;

        return true;
    }
}
