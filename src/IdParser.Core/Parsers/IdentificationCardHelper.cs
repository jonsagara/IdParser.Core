namespace IdParser.Core.Parsers;

internal static class IdentificationCardHelper
{
    internal static string? PostalCodeDisplay(string? postalCode, Country country)
    {
        if (postalCode is null)
        {
            return null;
        }

        if (country == Country.USA && postalCode.Length > 5)
        {
            return $"{postalCode.Substring(0, 5)}-{postalCode.Substring(5)}";
        }

        if (country == Country.Canada && postalCode.Length == 6)
        {
            return $"{postalCode.Substring(0, 3)} {postalCode.Substring(3)}";
        }

        return postalCode;
    }
}
