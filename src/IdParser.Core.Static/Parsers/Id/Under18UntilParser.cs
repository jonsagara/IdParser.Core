namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDH")]
internal static class Under18UntilParser
{
    internal static DateTime? ParseAndSet(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        IdCard.Under18Until = ParseDate(input);
    }
}
