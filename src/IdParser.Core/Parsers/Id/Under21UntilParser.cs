namespace IdParser.Core.Parsers.Id;

internal static class Under21UntilParser
{
    internal static DateTime? Parse(string input, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(input) || version < AAMVAVersion.AAMVA2000)
        {
            return null;
        }

        return ParserHelper.ParseDate(input, country, version);
    }

    internal static Field<DateTime?> Parse2(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(rawValue) || version < AAMVAVersion.AAMVA2000)
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate2(elementId: elementId, rawValue: rawValue, country, version);
    }
}
