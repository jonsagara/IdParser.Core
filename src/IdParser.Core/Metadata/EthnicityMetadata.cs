using IdParser.Core.Attributes;

namespace IdParser.Core.Metadata;

internal static class EthnicityMetadataHelper
{
    private static readonly Dictionary<Ethnicity, string> _ethnicityAbbreviations = Enum
        .GetValues<Ethnicity>()
        .ToDictionary(e => e, e => e.GetAbbreviationFromAbbreviationAttribute());

    /// <summary>
    /// Look up the Ethnicity abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    internal static string GetAbbreviationOrDefault(this Ethnicity ethnicity)
        => _ethnicityAbbreviations.GetValueOrDefault(ethnicity, ethnicity.ToString());
}
