namespace IdParser.Core.Parsers.License;

internal static class StandardRestrictionCodeParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var standardRestrictionCode = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: standardRestrictionCode, rawValue: rawValue);
    }
}
