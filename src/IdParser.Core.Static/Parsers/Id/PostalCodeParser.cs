namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAK")]
internal static class PostalCodeParser
{
    private const string NonAlphaNumericPattern = @"[^\w\d]";

    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        // Some jurisdictions, like Hawaii, have spaces after the ZIP code followed by a number like 0.
        // Chop off the excess junk otherwise we'd wind up with a 6-digit ZIP code.
        var indexOfSpaces = input.IndexOf("    ", StringComparison.OrdinalIgnoreCase);
        if (indexOfSpaces >= 0)
        {
            input = input.Substring(0, indexOfSpaces);
        }

        IdCard.Address.PostalCode = new Regex(NonAlphaNumericPattern)
            .Replace(input, "")
            .Replace("0000", "", StringComparison.Ordinal);
    }
}
