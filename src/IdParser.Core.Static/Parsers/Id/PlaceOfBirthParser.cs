namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCI")]
internal static class PlaceOfBirthParser
{
    internal static string Parse(string input)
    {
        IdCard.PlaceOfBirth = input;
    }
}
