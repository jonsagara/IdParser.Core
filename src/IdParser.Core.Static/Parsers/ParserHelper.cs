namespace IdParser.Core.Static.Parsers;
internal static class ParserHelper
{
    internal static bool StringHasNoValue(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "NONE"
            || input == "unavl"
            || input == "unavail";
    }
}
