namespace IdParser.Core.Parsers.Id;

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

    internal static Field<DateTime?> Parse2(string elementId, string? input, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.DateHasNoValue(input))
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: input);
        }

        return ParserHelper.ParseDate2(elementId: elementId, input: input, country, version);
    }
}
