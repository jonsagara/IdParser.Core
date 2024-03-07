namespace IdParser.Core.Parsers.Id;

internal static class LastNameParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue?.TrimEnd(','), rawValue: rawValue);
    }
}
