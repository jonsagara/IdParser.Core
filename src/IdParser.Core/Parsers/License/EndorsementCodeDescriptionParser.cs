namespace IdParser.Core.Parsers.License;

internal static class EndorsementCodeDescriptionParser
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

        var endorsementCodeDescription = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: endorsementCodeDescription, rawValue: rawValue);
    }
}
