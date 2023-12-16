namespace IdParser.Core.Parsers.Id;

internal static class HasTemporaryLawfulStatusParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;

    internal static Field<bool> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // If the elementId is present, but there is no value, default to false.
        var hasTemporaryLawfulStatus = ParserHelper.ParseBool2(rawValue) ?? false;

        return FieldHelpers.ParsedField(elementId: elementId, value: hasTemporaryLawfulStatus, rawValue: rawValue);
    }
}
