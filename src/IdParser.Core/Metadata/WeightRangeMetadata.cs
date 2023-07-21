using IdParser.Core.Attributes;

namespace IdParser.Core.Metadata;

internal static class WeightRangeMetadata
{
    private static readonly Dictionary<WeightRange, string> _weightRangeDescriptions = Enum
        .GetValues<WeightRange>()
        .ToDictionary(wr => wr, wr => wr.GetDescriptionFromDescriptionAttribute());

    /// <summary>
    /// Look up the WeightRange description from the enum's Description attribute. If none found,
    /// use the enum value as a string.
    /// </summary>
    internal static string GetDescriptionOrDefault(this WeightRange weightRange)
        => _weightRangeDescriptions.GetValueOrDefault(weightRange, weightRange.ToString());
}
