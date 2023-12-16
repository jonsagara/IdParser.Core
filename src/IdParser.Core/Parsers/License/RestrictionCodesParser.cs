namespace IdParser.Core.Parsers.License;

internal static class RestrictionCodesParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var restrictionCodes = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: restrictionCodes, rawValue: rawValue);
    }
}

internal static class RestrictionCodesLegacyParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var restrictionCodes = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: restrictionCodes, rawValue: rawValue);
    }
}
