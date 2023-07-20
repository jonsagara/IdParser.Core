namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCR")]
internal static class RestrictionCodeDescriptionParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.RestrictionCodeDescription = input;
    }
}
