namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDG")]
internal static class WasMiddleNameTruncatedParser
{
    internal static bool? ParseAndSet(string input)
    {
        IdCard.Name.WasMiddleTruncated = ParseBool(input);
    }
}
