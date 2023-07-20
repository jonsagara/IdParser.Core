using System.Globalization;
using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DAU")]
public class HeightParser : AbstractParser
{
    public HeightParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 3)
        {
            return;
        }

        if (Version == Version.Aamva2000)
        {
            var feet = Convert.ToInt32(input.Substring(0, 1), CultureInfo.InvariantCulture);
            var inches = Convert.ToInt32(input.Substring(1, 2), CultureInfo.InvariantCulture);

            IdCard.Height = Height.FromImperial(feet, inches);
            return;
        }

        var height = Convert.ToInt32(input.Substring(0, input.Length - 2), CultureInfo.InvariantCulture);

        if (input.Contains("cm", StringComparison.OrdinalIgnoreCase))
        {
            IdCard.Height = Height.FromMetric(height);
            return;
        }

        IdCard.Height = Height.FromImperial(height);
    }
}
