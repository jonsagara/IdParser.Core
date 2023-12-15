namespace IdParser.Core.Parsers.Id;

internal static class MiddleNameParser
{
    internal static string? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        return input;
    }

    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var middleName = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: middleName, rawValue: rawValue);
    }
}
