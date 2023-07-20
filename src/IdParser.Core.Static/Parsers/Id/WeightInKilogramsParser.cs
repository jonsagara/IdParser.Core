using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAX")]
internal static class WeightInKilogramsParser
{
    internal static Weight Parse(string input)
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
