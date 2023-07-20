namespace IdParser.Core.Static.Parsers.License;

//[Parser("DDC")]
internal static class HazmatEndorsementExpirationDateParser
{
    internal static DateTime? Parse(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        License.HazmatEndorsementExpirationDate = ParseDate(input);
    }
}
