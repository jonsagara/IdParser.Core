using System.Globalization;
using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DCE")]
public class WeightRangeParser : AbstractParser
{
    public WeightRangeParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        var weightRange = (WeightRange)Convert.ToByte(input, CultureInfo.InvariantCulture);

        if (IdCard.Weight is null)
        {
            IdCard.Weight = Weight.FromRange(weightRange: weightRange);
            return;
        }

        IdCard.Weight.WeightRange = weightRange;
    }
}
