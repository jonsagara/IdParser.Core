﻿namespace IdParser.Core.Parsers.Id;

internal static class WasFirstNameTruncatedParser
{
    internal static Field<bool?> Parse(string elementId, string? rawValue)
    {
        var wasFirstNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasFirstNameTruncated, rawValue: rawValue);
    }
}
