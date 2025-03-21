﻿namespace IdParser.Core.Parsers.Id;

internal static class DateOfBirthParser
{
    internal static Field<DateTime?> Parse(string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.DateHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        return ParserHelper.ParseDate(elementId: elementId, rawValue: rawValue, country, version);
    }
}
