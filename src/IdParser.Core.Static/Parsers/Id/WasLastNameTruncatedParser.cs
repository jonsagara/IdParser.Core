namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDE")]
internal static class WasLastNameTruncatedParser
{
    internal static bool? ParseAndSet(string input)
    {
        IdCard.Name.WasLastTruncated = ParseBool(input);
    }
}
