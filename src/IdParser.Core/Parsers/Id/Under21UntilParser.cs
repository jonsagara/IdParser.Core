namespace IdParser.Core.Parsers.Id;

internal static class Under21UntilParser
{
    internal static Field<DateTime?> Parse(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(rawValue) || version < AAMVAVersion.AAMVA2000)
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate(elementId: elementId, rawValue: rawValue, country, version);
    }
}
