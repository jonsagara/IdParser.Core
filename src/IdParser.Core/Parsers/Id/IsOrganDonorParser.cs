namespace IdParser.Core.Parsers.Id;

internal static class IsOrganDonorParser
{
    internal static Field<bool> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // If the element and/or value are not present, default to false.
        var isOrganDonor = ParserHelper.ParseBool(rawValue) ?? false;

        return FieldHelpers.ParsedField(elementId: elementId, value: isOrganDonor, rawValue: rawValue);
    }
}

internal static class IsOrganDonorLegacyParser
{
    internal static Field<bool> Parse(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // If the element and/or value are not present, default to false.

        var isOrganDonor = false;

        if (version == AAMVAVersion.AAMVA2000)
        {
            isOrganDonor = ParserHelper.ParseBool(rawValue) ?? false;

            if (rawValue?.Equals("DONOR", StringComparison.OrdinalIgnoreCase) == true)
            {
                isOrganDonor = true;
            }
        }

        return FieldHelpers.ParsedField(elementId: elementId, value: isOrganDonor, rawValue: rawValue);
    }
}
