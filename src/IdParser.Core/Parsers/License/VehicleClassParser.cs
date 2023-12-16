namespace IdParser.Core.Parsers.License;

internal static class VehicleClassParser
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

        var vehicleClass = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: vehicleClass, rawValue: rawValue);
    }
}

internal static class VehicleClassLegacyParser
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

        var vehicleClass = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: vehicleClass, rawValue: rawValue);
    }
}
