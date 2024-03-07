namespace IdParser.Core.Parsers.Id;

internal static class AliasSuffixParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var aliasSuffix = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: aliasSuffix, rawValue: rawValue);
    }
}
