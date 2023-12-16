namespace IdParser.Core.Parsers.License;

internal static class HazmatEndorsementExpirationDateParser
{
    internal static Field<DateTime?> Parse2(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.DateHasNoValue(rawValue) || version < AAMVAVersion.AAMVA2000)
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate2(elementId: elementId, rawValue: rawValue, country, version);
    }
}
