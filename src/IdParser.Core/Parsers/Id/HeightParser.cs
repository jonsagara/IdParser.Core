using System.Globalization;

namespace IdParser.Core.Parsers.Id;

internal static class HeightParser
{
    internal static Height? Parse(string input, AAMVAVersion version)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 3)
        {
            return null;
        }

        if (version == AAMVAVersion.AAMVA2000)
        {
            var feet = Convert.ToInt32(input.Substring(startIndex: 0, length: 1), CultureInfo.InvariantCulture);
            var inches = Convert.ToInt32(input.Substring(startIndex: 1, length: 2), CultureInfo.InvariantCulture);

            return new Height(feet: feet, inches: inches);
        }

        var height = Convert.ToInt32(input.Substring(startIndex: 0, length: input.Length - 2), CultureInfo.InvariantCulture);

        if (input.Contains("cm", StringComparison.OrdinalIgnoreCase))
        {
            return new Height(centimeters: height);
        }

        return new Height(inches: height);
    }
}
