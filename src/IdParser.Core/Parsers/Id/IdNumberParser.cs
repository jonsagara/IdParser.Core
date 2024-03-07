namespace IdParser.Core.Parsers.Id;

internal static class IdNumberParser
{
    /// <summary>
    /// Maintain previous behavior and assume that, if present, this element will always have a non-null value.
    /// </summary>
    internal static Field<string> Parse(string elementId, string? rawValue)
        => FieldHelpers.ParsedField(elementId: elementId, value: rawValue ?? string.Empty, rawValue: rawValue);
}
