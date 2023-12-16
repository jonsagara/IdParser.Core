namespace IdParser.Core.Parsers.Id;

internal static class WasFirstNameTruncatedParser
{
    internal static Field<bool?> Parse2(string elementId, string? rawValue)
    {
        var wasFirstNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool2(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasFirstNameTruncated, rawValue: rawValue);
    }
}
