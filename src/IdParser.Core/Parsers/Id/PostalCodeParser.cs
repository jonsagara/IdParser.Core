﻿using System.Text.RegularExpressions;

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
}
