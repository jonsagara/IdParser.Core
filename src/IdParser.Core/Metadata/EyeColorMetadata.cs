﻿using IdParser.Core.Attributes;

namespace IdParser.Core;

public static class EyeColorMetadata
{
    private static readonly Dictionary<EyeColor, string> _eyeColorAbbreviations = Enum
        .GetValues<EyeColor>()
        .ToDictionary(ec => ec, ec => ec.GetAbbreviationOrDefaultFromAbbreviationAttribute());

    /// <summary>
    /// Look up the EyeColor abbreviation from the enum's Abbreviation attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    public static string GetAbbreviationOrDefault(this EyeColor eyeColor)
        => _eyeColorAbbreviations.TryGetValue(eyeColor, out string? abbreviation)
        ? abbreviation
        : eyeColor.ToString();
}
