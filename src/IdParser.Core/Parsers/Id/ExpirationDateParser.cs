namespace IdParser.Core.Parsers.Id;

internal static class ExpirationDateParser
{
    internal static DateTime Parse(string input, Country country, AAMVAVersion version)
    {
        if (ParserHelper.DateHasNoValue(input))
        {
            return DateTime.MinValue;
        }

        return ParserHelper.ParseDate(input, country, version);
    }

    internal static Field<DateTime?> Parse2(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.DateHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate2(elementId: elementId, rawValue: rawValue, country, version);
    }
}
