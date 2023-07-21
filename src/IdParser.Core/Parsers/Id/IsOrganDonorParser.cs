namespace IdParser.Core.Static.Parsers.Id;

internal static class IsOrganDonorParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;
}

internal static class IsOrganDonorLegacyParser
{
    internal static bool Parse(string input, AAMVAVersion version)
    {
        var isOrganDonor = false;

        if (version == AAMVAVersion.AAMVA2000)
        {
            isOrganDonor = ParserHelper.ParseBool(input) ?? false;

            if (input.Equals("DONOR", StringComparison.OrdinalIgnoreCase))
            {
                isOrganDonor = true;
            }
        }

        return isOrganDonor;
    }
}
