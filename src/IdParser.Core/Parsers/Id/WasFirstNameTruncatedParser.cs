namespace IdParser.Core.Parsers.Id;

internal static class WasFirstNameTruncatedParser
{
    internal static bool? Parse(string input)
        => ParserHelper.ParseBool(input);

    internal static Field<bool?> Parse2(string elementId, string? rawValue)
    {
        var wasFirstNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasFirstNameTruncated, rawValue: rawValue);
    }
}
