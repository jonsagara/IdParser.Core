namespace IdParser.Core.Parsers.Id;

internal static class WasFirstNameTruncatedParser
{
    internal static bool? Parse(string input)
        => ParserHelper.ParseBool(input);
}
