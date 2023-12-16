namespace IdParser.Core.Parsers.Id;

internal static class WasMiddleNameTruncatedParser
{
    internal static Field<bool?> Parse(string elementId, string? rawValue)
    {
        var wasMiddleNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasMiddleNameTruncated, rawValue: rawValue);
    }
}
