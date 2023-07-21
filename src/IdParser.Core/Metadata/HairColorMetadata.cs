using IdParser.Core.Attributes;

namespace IdParser.Core.Metadata;

internal static class HairColorMetadata
{
    private static readonly Dictionary<HairColor, string> _hairColorAbbreviations = Enum
        .GetValues<HairColor>()
        .ToDictionary(ec => ec, ec => ec.GetAbbreviationFromAbbreviationAttribute());

    /// <summary>
    /// Look up the HairColor abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    internal static string GetAbbreviationOrDefault(this HairColor hairColor)
        => _hairColorAbbreviations.GetValueOrDefault(hairColor, hairColor.ToString());
}
