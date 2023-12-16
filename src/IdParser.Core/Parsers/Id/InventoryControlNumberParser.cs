namespace IdParser.Core.Parsers.Id;

internal static class InventoryControlNumberParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue, rawValue: rawValue);
    }
}
