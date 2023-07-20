namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCT")]
internal static class GivenNameParser
{
    // AAMVA 2003-2005
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var givenNames = input.Split(',', '$', ' ');
        IdCard.Name.First = givenNames[0].Trim();
        IdCard.Name.Middle = givenNames.Length > 1 ? givenNames[1].Trim().ReplaceEmptyWithNull() : null;
    }
}
