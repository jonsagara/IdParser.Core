namespace IdParser.Core.Parsers.Id;

internal static class WasLastNameTruncatedParser
{
    internal static Field<bool?> Parse(string elementId, string? rawValue)
    {
        var wasLastNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool2(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasLastNameTruncated, rawValue: rawValue);
    }
}
