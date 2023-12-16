namespace IdParser.Core.Parsers.License;

internal static class EndorsementCodesParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var endorsementCode = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: endorsementCode, rawValue: rawValue);
    }
}

internal static class EndorsementCodesLegacyParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var endorsementCode = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: endorsementCode, rawValue: rawValue);
    }
}
