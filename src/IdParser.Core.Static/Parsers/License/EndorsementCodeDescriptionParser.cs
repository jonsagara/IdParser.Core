namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCQ")]
internal static class EndorsementCodeDescriptionParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodeDescription = input;
    }
}
