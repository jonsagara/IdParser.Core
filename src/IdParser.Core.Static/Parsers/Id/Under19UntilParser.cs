namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDI")]
internal static class Under19UntilParser
{
    internal static DateTime? Parse(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        IdCard.Under19Until = ParseDate(input);
    }
}
