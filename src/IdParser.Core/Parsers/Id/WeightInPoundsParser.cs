﻿using System.Globalization;
using System.Text.RegularExpressions;
using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DAW")]
public class WeightInPoundsParser : AbstractParser
{
    public WeightInPoundsParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (TryParseMetric(input))
        {
            return;
        }

        var weight = Convert.ToInt16(input, CultureInfo.InvariantCulture);

        if (IdCard.Weight is null)
        {
            IdCard.Weight = Weight.FromImperial(pounds: weight);
            return;
        }

        IdCard.Weight.SetImperial(weight);
    }

    /// <summary>
    /// Alberta put the weight in kilograms in the weight in pounds parser ¯\_(ツ)_/¯
    /// </summary>
    private bool TryParseMetric(string input)
    {
        var metricRegex = new Regex("(?<Weight>\\d+)+\\s*KG");
        var match = metricRegex.Match(input);

        if (match.Success)
        {
            var weight = Convert.ToInt16(match.Groups["Weight"].Value, CultureInfo.InvariantCulture);

            if (IdCard.Weight is null)
            {
                IdCard.Weight = Weight.FromMetric(kilograms: weight);
                return true;
            }

            IdCard.Weight.SetMetric(weight);
            return true;
        }

        return false;
    }
}