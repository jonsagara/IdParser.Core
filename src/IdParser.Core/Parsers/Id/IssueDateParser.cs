namespace IdParser.Core.Parsers.Id;

internal static class IssueDateParser
{
    internal static DateTime Parse(string input, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(input))
        {
            return DateTime.MinValue;
        }

        return ParserHelper.ParseDate(input, country, version);
    }
}
