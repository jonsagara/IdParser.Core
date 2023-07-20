using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCE")]
internal static class WeightRangeParser
{
    internal static WeightRange Parse(string input)
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
