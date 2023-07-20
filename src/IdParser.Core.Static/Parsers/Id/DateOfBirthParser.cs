namespace IdParser.Core.Static.Parsers.Id;

internal static class DateOfBirthParser
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
