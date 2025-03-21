﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using IdParser.Core.Constants;

namespace IdParser.Core.Parsers.Id;

internal static partial class WeightInPoundsParser
{
    internal static Field<Weight?> Parse(string elementId, string? rawValue)
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
            : FieldHelpers.UnparsedField<Weight?>(elementId: elementId, rawValue: rawValue, error: $"Unable to parse Weight in pounds from field '{SubfileElementIds.WeightInPounds}': '{rawValue}' is not a valid integer.");
    }


    [GeneratedRegex(@"(?<Weight>\d+)+\s*KG")]
    private static partial Regex MetricWeightRegex();

    /// <summary>
    /// Alberta put the weight in kilograms in the weight in pounds parser ¯\_(ツ)_/¯
    /// </summary>
    private static bool TryParseMetric(string input, [NotNullWhen(true)] out Weight? weight)
    {
        weight = null;

        var match = MetricWeightRegex().Match(input);

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
