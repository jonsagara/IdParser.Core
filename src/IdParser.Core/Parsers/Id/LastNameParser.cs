namespace IdParser.Core.Parsers.Id;

internal static class LastNameParser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return input.TrimEnd(',');
    }

    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue?.TrimEnd(','), rawValue: rawValue);
    }
}
