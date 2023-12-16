namespace IdParser.Core.Parsers.License;

internal static class StandardEndorsementCodeParser
{
   internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var standardEndorsementCode = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: standardEndorsementCode, rawValue: rawValue);
    }
}
