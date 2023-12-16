namespace IdParser.Core.Parsers.Id;

internal static class NameSuffixParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var nameSuffix = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: nameSuffix, rawValue: rawValue);
    }
}
