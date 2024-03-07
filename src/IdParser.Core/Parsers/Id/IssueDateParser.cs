namespace IdParser.Core.Parsers.Id;

internal static class IssueDateParser
{
    internal static Field<DateTime?> Parse(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // #18: IDs from TX using AAMVA2016 can have "NONE" for the DBD field, which is mandatory and should contain
        //   an 8-character date string. Alas...
        if (ParserHelper.DateHasNoValue(rawValue) || ParserHelper.StringIsNone(rawValue))
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate(elementId: elementId, rawValue: rawValue, country, version);
    }
}
