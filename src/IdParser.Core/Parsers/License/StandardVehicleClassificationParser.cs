namespace IdParser.Core.Parsers.License;

internal static class StandardVehicleClassificationParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var standardVehicleClassification = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: standardVehicleClassification, rawValue: rawValue);
    }
}
