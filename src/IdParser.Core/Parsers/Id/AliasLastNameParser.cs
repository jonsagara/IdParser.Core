namespace IdParser.Core.Parsers.Id;

internal static class AliasLastNameParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var aliasLastName = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: aliasLastName, rawValue: rawValue);
    }
}
