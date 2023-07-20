using System.Globalization;
using IdParser.Attributes;

namespace IdParser.Parsers.Id;

[Parser("DAX")]
public class WeightInKilogramsParser : AbstractParser
{
    public WeightInKilogramsParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        var weight = Convert.ToInt16(input, CultureInfo.InvariantCulture);

        if (IdCard.Weight is null)
        {
            IdCard.Weight = Weight.FromMetric(kilograms: weight);
            return;
        }

        IdCard.Weight.SetMetric(weight);
    }
}
