namespace IdParser.Core.Parsers.Id;

internal static class WasMiddleNameTruncatedParser
{
    internal static bool? Parse(string input)
        => ParserHelper.ParseBool(input);
}
