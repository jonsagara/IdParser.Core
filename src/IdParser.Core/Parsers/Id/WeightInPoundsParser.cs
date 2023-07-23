using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static class WeightInPoundsParser
{
    internal static Weight Parse(string input)
    {
        if (TryParseMetric(input, out Weight? weight))
        {
            return weight;
        }

        var weightLbs = short.Parse(input.AsSpan(), provider: CultureInfo.InvariantCulture);

        return new Weight(pounds: weightLbs);
    }


    private static readonly Regex _rxMetricWeight = new Regex("(?<Weight>\\d+)+\\s*KG", RegexOptions.Compiled);

    /// <summary>
    /// Alberta put the weight in kilograms in the weight in pounds parser ¯\_(ツ)_/¯
    /// </summary>
    private static bool TryParseMetric(string input, [NotNullWhen(true)] out Weight? weight)
    {
        weight = null;

        var match = _rxMetricWeight.Match(input);

        if (match.Success)
        {
            var weightKg = short.Parse(match.Groups["Weight"].Value.AsSpan(), provider: CultureInfo.InvariantCulture);

            weight = new Weight(kilograms: weightKg);
            return true;
        }

        return false;
    }
}
