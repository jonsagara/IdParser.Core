namespace IdParser.Core.Parsers.Id;

internal static class WasLastNameTruncatedParser
{
    internal static bool? Parse(string input)
        => ParserHelper.ParseBool(input);
}
