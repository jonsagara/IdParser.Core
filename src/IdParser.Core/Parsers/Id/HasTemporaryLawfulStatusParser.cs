namespace IdParser.Core.Parsers.Id;

internal static class HasTemporaryLawfulStatusParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;

    internal static Field<bool?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: ParserHelper.ParseBool2(rawValue), rawValue: rawValue);
    }
}
