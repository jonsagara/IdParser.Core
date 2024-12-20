﻿namespace IdParser.Core.Parsers.Id;

internal static class HasTemporaryLawfulStatusParser
{
    internal static Field<bool> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // If the elementId is present, but there is no value, default to false.
        var hasTemporaryLawfulStatus = ParserHelper.ParseBool(rawValue) ?? false;

        return FieldHelpers.ParsedField(elementId: elementId, value: hasTemporaryLawfulStatus, rawValue: rawValue);
    }
}
