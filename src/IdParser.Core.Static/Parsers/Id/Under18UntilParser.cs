namespace IdParser.Core.Static.Parsers.Id;

internal static class Under18UntilParser
{
    internal static DateTime? Parse(string input, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(input) || version < AAMVAVersion.AAMVA2000)
        {
            return null;
        }

        return ParserHelper.ParseDate(input, country, version);
    }
}
