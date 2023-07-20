namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDB")]
internal static class RevisionDate
{
    internal static DateTime? Parse(string input)
    {
        if (DateHasNoValue(input))
        {
            return;
        }

        IdCard.RevisionDate = ParseDate(input);
    }
}
