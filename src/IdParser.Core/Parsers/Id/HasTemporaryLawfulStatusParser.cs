namespace IdParser.Core.Static.Parsers.Id;

internal static class HasTemporaryLawfulStatusParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;
}
