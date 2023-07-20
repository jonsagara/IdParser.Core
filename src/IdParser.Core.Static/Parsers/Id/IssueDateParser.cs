namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DBD")]
internal static class IssueDateParser
{
    internal static DateTime? Parse(string input)
    {
        if (DateHasNoValue(input))
        {
            return;
        }

        IdCard.IssueDate = ParseDate(input);
    }
}
