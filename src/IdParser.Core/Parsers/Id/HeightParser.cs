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
            var feet = int.Parse(input.AsSpan(start: 0, length: 1), provider: CultureInfo.InvariantCulture);
            var inches = int.Parse(input.AsSpan(start: 1, length: 2), provider: CultureInfo.InvariantCulture);

            return new Height(feet: feet, inches: inches);
        }

        var height = int.Parse(input.AsSpan(start: 0, length: input.Length - 2), provider: CultureInfo.InvariantCulture);

        if (input.Contains("cm", StringComparison.OrdinalIgnoreCase))
        {
            return new Height(centimeters: height);
        }

        return new Height(inches: height);
    }
}
