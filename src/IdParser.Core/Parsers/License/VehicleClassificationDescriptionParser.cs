﻿namespace IdParser.Core.Parsers.License;

internal static class VehicleClassificationDescriptionParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        var vehicleClassificationDescription = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: vehicleClassificationDescription, rawValue: rawValue);
    }
}
