namespace IdParser.Core.Static.Parsers.Id;

internal static class AliasSuffixParser
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
