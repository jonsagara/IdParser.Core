namespace IdParser.Core.Static.Parsers.Id;

internal static class RevisionDateParser
{
    internal static DateTime? Parse(string input, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(input))
        {
            return null;
        }

        return ParserHelper.ParseDate(input, country, version);
    }
}
