namespace IdParser.Core.Parsers.Id;

internal static class IdNumberParser
{
    internal static string Parse(string input)
        => input;

    internal static Field<string> Parse2(string elementId, string input)
        => FieldHelpers.ParsedField(elementId: elementId, value: input, rawValue: input);
}
