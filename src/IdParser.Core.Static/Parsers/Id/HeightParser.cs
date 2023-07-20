using System.Globalization;

namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAU")]
internal static class HeightParser : AbstractParser
{
    internal static Height? ParseAndSet(string input, AAMVAVersion version)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 3)
        {
            return;
        }

        if (version == AAMVAVersion.AAMVA2000)
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
