using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static class PostalCodeParser
{
    private static readonly Regex _rxNonAlphaNumeric = new Regex(@"[^\w\d]", RegexOptions.Compiled);

    internal static string? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        // Some jurisdictions, like Hawaii, have spaces after the ZIP code followed by a number like 0.
        // Chop off the excess junk; otherwise we'd wind up with a 6-digit ZIP code.
        var indexOfSpaces = input.IndexOf("    ", StringComparison.OrdinalIgnoreCase);
        if (indexOfSpaces >= 0)
        {
            input = input.Substring(0, indexOfSpaces);
        }

        return _rxNonAlphaNumeric
            .Replace(input, "")
            .Replace("0000", "", StringComparison.Ordinal);
    }

    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        string? postalCode;

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            postalCode = null;
        }
        else
        {
            // Some jurisdictions, like Hawaii, have spaces after the ZIP code followed by a number like 0.
            // Chop off the excess junk; otherwise we'd wind up with a 6-digit ZIP code.
            var indexOfSpaces = rawValue.IndexOf("    ", StringComparison.OrdinalIgnoreCase);
            if (indexOfSpaces >= 0)
            {
                rawValue = rawValue.Substring(0, indexOfSpaces);
            }

            postalCode = _rxNonAlphaNumeric
                .Replace(rawValue, "")
                .Replace("0000", "", StringComparison.Ordinal);
        }

        return FieldHelpers.ParsedField(elementId: elementId, value: postalCode, rawValue: rawValue);
    }
}
