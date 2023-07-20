namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAQ")]
internal static class IdNumberParser
{
    internal static string Parse(string input)
    {
        IdCard.IdNumber = input;
    }
}
