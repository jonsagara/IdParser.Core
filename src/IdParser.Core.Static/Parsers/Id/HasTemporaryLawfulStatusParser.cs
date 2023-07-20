namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDD")]
internal static class HasTemporaryLawfulStatusParser
{
    internal static bool Parse(string input)
    {
        IdCard.HasTemporaryLawfulStatus = ParseBool(input) ?? false;
    }
}
