namespace IdParser.Core.Parsers.License;

internal static class VehicleClassificationDescriptionParser
{
    internal static string? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        return input;
    }

    internal static Field<string?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var vehicleClassificationDescription = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: vehicleClassificationDescription, rawValue: rawValue);
    }
}
