namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCD")]
internal static class EndorsementCodesParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodes = input;
    }
}

//[Parser("DAT")]
internal static class EndorsementCodesLegacyParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodes = input;
    }
}
