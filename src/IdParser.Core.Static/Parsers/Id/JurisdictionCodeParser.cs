namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAJ")]
internal static class JurisdictionCodeParser
{
    internal static string ParseAndSet(string input)
    {
        IdCard.Address.JurisdictionCode = input;
    }
}
