namespace IdParser.Core.Parsers.Id;

internal static class MiddleNameParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var middleName = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: middleName, rawValue: rawValue);
    }
}
