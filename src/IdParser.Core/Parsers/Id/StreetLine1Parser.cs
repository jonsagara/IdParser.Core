namespace IdParser.Core.Parsers.Id;

internal static class StreetLine1Parser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue?.TrimEnd(','), rawValue: rawValue);
    }
}

internal static class StreetLine1LegacyParser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue?.TrimEnd(','), rawValue: rawValue);
    }
}
