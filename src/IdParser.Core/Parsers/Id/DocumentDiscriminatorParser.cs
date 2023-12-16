namespace IdParser.Core.Parsers.Id;

internal static class DocumentDiscriminatorParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var documentDiscriminator = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: documentDiscriminator, rawValue: rawValue);
    }
}
