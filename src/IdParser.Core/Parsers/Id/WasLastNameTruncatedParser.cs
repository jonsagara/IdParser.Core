namespace IdParser.Core.Parsers.Id;

internal static class WasLastNameTruncatedParser
{
    internal static bool? Parse(string input)
        => ParserHelper.ParseBool(input);

    internal static Field<bool?> Parse2(string elementId, string? rawValue)
    {
        var wasLastNameTruncated = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : ParserHelper.ParseBool(rawValue);

        return FieldHelpers.ParsedField(elementId: elementId, value: wasLastNameTruncated, rawValue: rawValue);
    }
}
