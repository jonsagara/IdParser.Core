using IdParser.Core.Attributes;

namespace IdParser.Core;

public static class EthnicityMetadataHelper
{
    private static readonly Dictionary<Ethnicity, string> _ethnicityAbbreviations = Enum
        .GetValues<Ethnicity>()
        .ToDictionary(e => e, e => e.GetAbbreviationOrDefaultFromAbbreviationAttribute());

    /// <summary>
    /// Look up the Ethnicity abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    public static string GetAbbreviationOrDefault(this Ethnicity ethnicity)
        => _ethnicityAbbreviations.TryGetValue(ethnicity, out string? abbreviation)
        ? abbreviation
        : ethnicity.ToString();
}
