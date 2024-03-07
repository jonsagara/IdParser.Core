namespace IdParser.Core.Parsers.Id;

internal static class JurisdictionCodeParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue, rawValue: rawValue);
    }
}
