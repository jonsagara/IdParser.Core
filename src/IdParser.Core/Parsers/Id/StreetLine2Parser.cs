using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static class StreetLine2Parser
{
    internal static string? Parse(string input, Address address)
    {
        ArgumentNullException.ThrowIfNull(address);

        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        // Jurisdictions like Wyoming set the StreetLine2 to the City, State, and Postal Code when it
        if (address.City is not null &&
            address.JurisdictionCode is not null &&
            address.PostalCode is not null &&
            Regex.IsMatch(input, $@"\s*{address.City}(\s|,)*{address.JurisdictionCode}(\s|,)*{address.PostalCode}"))
        {
            return null;
        }

        return input.TrimEnd(',');
    }
}
