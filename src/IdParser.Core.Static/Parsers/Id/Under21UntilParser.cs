namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDJ")]
internal static class Under21UntilParser
{
    internal static DateTime? ParseAndSet(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        IdCard.Under21Until = ParseDate(input);
    }
}
