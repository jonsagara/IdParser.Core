namespace IdParser.Core.Parsers.Id;

internal static class AliasFirstNameParser
{
    internal static string? Parse(string input, AAMVAVersion version)
    {
        // DBG was designated for Medical Indicator/Codes only in AAMVA 2000 but we don't support this deprecated property
        if (version == AAMVAVersion.AAMVA2000)
        {
            return null;
        }

        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        return input;
    }
}
