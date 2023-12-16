namespace IdParser.Core.Parsers.License;

internal static class RestrictionCodeDescriptionParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var restrictionCodeDescription = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: restrictionCodeDescription, rawValue: rawValue);
    }
}
