namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDL")]
internal static class IsVeteranParser
{
    internal static bool ParseAndSet(string input)
    {
        IdCard.IsVeteran = ParseBool(input) ?? false;
    }
}
