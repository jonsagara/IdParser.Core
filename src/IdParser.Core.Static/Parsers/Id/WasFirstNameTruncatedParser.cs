namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDF")]
internal static class WasFirstNameTruncatedParser
{
    internal static bool? Parse(string input)
    {
        IdCard.Name.WasFirstTruncated = ParseBool(input);
    }
}
