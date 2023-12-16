namespace IdParser.Core.Parsers.Id;

internal static class IsOrganDonorParser
{
    internal static bool Parse(string input)
        => ParserHelper.ParseBool(input) ?? false;

    internal static Field<bool?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        return FieldHelpers.ParsedField(elementId: elementId, value: ParserHelper.ParseBool2(rawValue), rawValue: rawValue);
    }
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

    internal static Field<bool?> Parse2(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        bool? isOrganDonor = null;

        if (version == AAMVAVersion.AAMVA2000)
        {
            isOrganDonor = ParserHelper.ParseBool2(rawValue);

            if (rawValue?.Equals("DONOR", StringComparison.OrdinalIgnoreCase) == true)
            {
                isOrganDonor = true;
            }
        }

        return FieldHelpers.ParsedField(elementId: elementId, value: isOrganDonor, rawValue: rawValue);
    }
}
