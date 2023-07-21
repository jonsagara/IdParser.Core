using IdParser.Core.Static.Attributes;

namespace IdParser.Core.Static.Metadata;

internal static class EyeColorMetadata
{
    private static readonly Dictionary<EyeColor, string> _eyeColorAbbreviations = Enum
        .GetValues<EyeColor>()
        .ToDictionary(ec => ec, ec => ec.GetAbbreviationFromAbbreviationAttribute());

    /// <summary>
    /// Look up the EyeColor abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    internal static string GetAbbreviationOrDefault(this EyeColor eyeColor)
        => _eyeColorAbbreviations.GetValueOrDefault(eyeColor, eyeColor.ToString());
}
