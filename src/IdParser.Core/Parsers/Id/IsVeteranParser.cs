namespace IdParser.Core.Parsers.Id;

internal static class IsVeteranParser
{
    internal static Field<bool> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // If the element or value is not present, default to false.
        var isVeteran = ParserHelper.ParseBool(rawValue) ?? false;

        return FieldHelpers.ParsedField(elementId: elementId, value: isVeteran, rawValue: rawValue);
    }
}
