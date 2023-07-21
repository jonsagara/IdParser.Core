namespace IdParser.Core.Static.Parsers.License;

internal static class HazmatEndorsementExpirationDateParser
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
