namespace IdParser.Core.Parsers.Id;

internal static class AuditInformationParser
{
    internal static string Parse(string input)
        => input;

    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: rawValue, rawValue: rawValue);
    }
}
