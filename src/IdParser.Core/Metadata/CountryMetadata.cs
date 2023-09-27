using IdParser.Core.Attributes;

namespace IdParser.Core;

public static class CountryMetadata
{
    private static readonly Dictionary<Country, string> _countryAbbreviations = Enum
        .GetValues<Country>()
        .ToDictionary(c => c, c => c.GetAbbreviationFromAbbreviationAttribute());

    /// <summary>
    /// Look up the Country abbreviation from the enum's Abbreviation attribute. Throw if none found.
    /// </summary>
    public static string GetAbbreviation(this Country country)
    {
        if (_countryAbbreviations.TryGetValue(country, out string? abbreviation))
        {
            return abbreviation;
        }

        throw new InvalidOperationException($"Unable to find an abbreviation for {nameof(Country)} enum value {country}.");
    }
}
