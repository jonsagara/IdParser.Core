using IdParser.Core.Attributes;

namespace IdParser.Core;

internal record IssuerMetadata(Country Country, string Abbreviation, string Description);

public static class IssuerMetadataHelper
{
    private static readonly Dictionary<IssuerIdentificationNumber, string> _issuerIdentificationNumberAbbreviations = Enum
        .GetValues<IssuerIdentificationNumber>()
        .ToDictionary(iin => iin, ec => ec.GetAbbreviationFromAbbreviationAttribute());

    private static readonly Dictionary<IssuerIdentificationNumber, Country> _issuerIdentificationNumberCountries = Enum
        .GetValues<IssuerIdentificationNumber>()
        .ToDictionary(iin => iin, ec => ec.GetCountryFromCountryAttribute());

    private static readonly Dictionary<IssuerIdentificationNumber, string> _issuerIdentificationNumberDescriptions = Enum
        .GetValues<IssuerIdentificationNumber>()
        .ToDictionary(iin => iin, ec => ec.GetDescriptionFromDescriptionAttribute());

    /// <summary>
    /// Look up the IssuerIdentificationNumber abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    public static string GetAbbreviationOrDefault(this IssuerIdentificationNumber issuerIdentificationNumber)
        => _issuerIdentificationNumberAbbreviations.TryGetValue(issuerIdentificationNumber, out string? abbreviation)
        ? abbreviation
        : issuerIdentificationNumber.ToString();

    /// <summary>
    /// Look up the IssuerIdentificationNumber's Country from the enum's Country attribute. If none found,
    /// throw an exception. Every enum value should have a Country attribute.
    /// </summary>
    public static Country GetCountry(this IssuerIdentificationNumber issuerIdentificationNumber)
    {
        if (_issuerIdentificationNumberCountries.TryGetValue(issuerIdentificationNumber, out Country country))
        {
            return country;
        }

        throw new InvalidOperationException($"{nameof(IssuerIdentificationNumber)} enum value {issuerIdentificationNumber} is missing a {typeof(CountryAttribute).FullName}.");
    }

    /// <summary>
    /// Look up the IssuerIdentificationNumber description from the enum's Description attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    public static string GetDescriptionOrDefault(this IssuerIdentificationNumber issuerIdentificationNumber)
        => _issuerIdentificationNumberDescriptions.TryGetValue(issuerIdentificationNumber, out string? description)
        ? description 
        : issuerIdentificationNumber.ToString();
}
