using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static class StreetLine2Parser
{
    internal static Field<string?> Parse2(string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        string? streetLine2;

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            streetLine2 = null;
        }
        else if (city is not null &&
            jurisdictionCode is not null &&
            postalCode is not null &&
            Regex.IsMatch(rawValue, $@"\s*{city}(\s|,)*{jurisdictionCode}(\s|,)*{postalCode}"))
        {
            // Jurisdictions like Wyoming set the StreetLine2 to the City, State, and Postal Code when it
            streetLine2 = null;
        }
        else
        {
            streetLine2 = rawValue?.TrimEnd(',');
        }

        return FieldHelpers.ParsedField(elementId: elementId, value: streetLine2, rawValue: rawValue);
    }
}
