namespace IdParser.Core.Static.Parsers.License;

internal static class RestrictionCodeDescriptionParser
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
