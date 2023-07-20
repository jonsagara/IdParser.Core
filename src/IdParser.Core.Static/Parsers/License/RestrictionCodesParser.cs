namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCB")]
internal static class RestrictionCodesParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.RestrictionCodes = input;
    }
}

//[Parser("DAS")]
internal static class RestrictionCodesLegacyParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.RestrictionCodes = input;
    }
}
