namespace IdParser.Core.Parsers.Id;

internal static class IsVeteranParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;
}
