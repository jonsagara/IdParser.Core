using IdParser.Core.Attributes;

namespace IdParser.Core;

public static class HairColorMetadata
{
    private static readonly Dictionary<HairColor, string> _hairColorAbbreviations = Enum
        .GetValues<HairColor>()
        .ToDictionary(hc => hc, hc => hc.GetAbbreviationOrDefaultFromAbbreviationAttribute());

    /// <summary>
    /// Look up the HairColor abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    public static string GetAbbreviationOrDefault(this HairColor hairColor)
        => _hairColorAbbreviations.TryGetValue(hairColor, out string? abbreviation)
            ? abbreviation
            : hairColor.ToString();
}
