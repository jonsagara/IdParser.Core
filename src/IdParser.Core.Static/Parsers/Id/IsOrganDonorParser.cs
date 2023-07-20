namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDK")]
internal static class IsOrganDonorParser
{
    internal static bool ParseAndSet(string input)
    {
        IdCard.IsOrganDonor = ParseBool(input) ?? false;
    }
}

//[Parser("DBH")]
internal static class IsOrganDonorLegacyParser
{
    internal static bool ParseAndSet(string input, AAMVAVersion version)
    {
        if (version == AAMVAVersion.AAMVA2000)
        {
            IdCard.IsOrganDonor = ParseBool(input) ?? false;

            if (input.Equals("DONOR", StringComparison.OrdinalIgnoreCase))
            {
                IdCard.IsOrganDonor = true;
            }
        }
    }
}
