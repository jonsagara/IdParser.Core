namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DBA")]
internal static class ExpirationDateParser
{
    internal static string? Parse(string input)
    {
        if (DateHasNoValue(input))
        {
            return;
        }

        IdCard.ExpirationDate = ParseDate(input);
    }
}
