using System.Text.RegularExpressions;

namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAH")]
internal static class StreetLine2Parser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        // Jurisdictions like Wyoming set the StreetLine2 to the City, State, and Postal Code when it
        if (IdCard.Address.City is not null &&
            IdCard.Address.JurisdictionCode is not null &&
            IdCard.Address.PostalCode is not null &&
            Regex.IsMatch(input, $@"\s*{IdCard.Address.City}(\s|,)*{IdCard.Address.JurisdictionCode}(\s|,)*{IdCard.Address.PostalCode}"))
        {
            return;
        }

        IdCard.Address.StreetLine2 = input.TrimEnd(',');
    }
}
