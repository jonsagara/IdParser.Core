using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IdParser.Core.Parsers.Id;

internal static class WeightInPoundsParser
{
    internal static Field<Weight?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (TryParseMetric(rawValue, out Weight? weight))
        {
            return FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: weight, rawValue: rawValue);
        }

        return short.TryParse(rawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out short weightLbs)
            ? FieldHelpers.ParsedField<Weight?>(elementId: elementId, value: new Weight(pounds: weightLbs), rawValue: rawValue)
            : FieldHelpers.UnparsedField<Weight?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Weight in pounds from '{rawValue}'");
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
            // Parse is okay here because the regex guarantees that the Weight capturing group will only contain digits.
            var weightKg = short.Parse(match.Groups["Weight"].Value.AsSpan(), provider: CultureInfo.InvariantCulture);

            weight = new Weight(kilograms: weightKg);
            return true;
        }

        return false;
    }
}
