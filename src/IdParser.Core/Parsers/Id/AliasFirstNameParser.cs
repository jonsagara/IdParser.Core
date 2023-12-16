namespace IdParser.Core.Parsers.Id;

internal static class AliasFirstNameParser
{
    internal static Field<string?> Parse(string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // DBG was designated for Medical Indicator/Codes only in AAMVA 2000 but we don't support this deprecated property
        if (version == AAMVAVersion.AAMVA2000)
        {
            return FieldHelpers.UnparsedField<string?>(elementId: elementId, rawValue: rawValue, $"DBG was designated for Medical Indicator/Codes only in AAMVA 2000 but we don't support this deprecated property. AAMVA Version = {version}.");
        }

        var aliasFirstName = ParserHelper.StringHasNoValue(rawValue)
            ? null
            : rawValue;

        return FieldHelpers.ParsedField(elementId: elementId, value: aliasFirstName, rawValue: rawValue);
    }
}
