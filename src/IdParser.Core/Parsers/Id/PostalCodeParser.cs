using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static partial class PostalCodeParser
{
    [GeneratedRegex(@"[^\w\d]")]
    private static partial Regex NonAlphaNumericRegex();


    internal static Field<string?> Parse(string elementId, string? rawValue)
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

            postalCode = NonAlphaNumericRegex()
                .Replace(rawValue, "")
                .Replace("0000", "", StringComparison.Ordinal);
        }

        return FieldHelpers.ParsedField(elementId: elementId, value: postalCode, rawValue: rawValue);
    }
}
