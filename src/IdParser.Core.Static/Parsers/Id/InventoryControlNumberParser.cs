namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCK")]
internal static class InventoryControlNumberParser
{
    internal static string Parse(string input)
    {
        IdCard.InventoryControlNumber = input;
    }
}
