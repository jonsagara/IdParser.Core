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

    /// <summary>
    /// Alberta put the weight in kilograms in the weight in pounds parser ¯\_(ツ)_/¯
    /// </summary>
    private static bool TryParseMetric(string input, [NotNullWhen(true)] out Weight? weight)
    {
        weight = null;

        var metricRegex = new Regex("(?<Weight>\\d+)+\\s*KG");
        var match = metricRegex.Match(input);

        if (match.Success)
        {
            var weightKg = short.Parse(match.Groups["Weight"].Value.AsSpan(), provider: CultureInfo.InvariantCulture);

            weight = new Weight(kilograms: weightKg);
            return true;
        }

        return false;
    }
}
