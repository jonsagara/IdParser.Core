namespace IdParser.Core.Parsers.License;

internal static class EndorsementCodeDescriptionParser
{
    internal static string? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        return input;
    }
}
