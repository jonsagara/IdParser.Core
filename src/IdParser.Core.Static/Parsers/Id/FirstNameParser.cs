namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAC")]
internal static class FirstNameParser
{
    internal static string? ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        // According to A.2.1 (AAMVA Person Name Rule) in the D20 Data Dictionary,
        // first names can only contain alphabetic characters. All other characters, such as spaces,
        // are deleted and not encoded. Middle names are allowed to have spaces.
        // Jurisdictions like Wyoming put the middle initial in the first name field.

        var spaceIndex = input.LastIndexOf(' ');

        if (spaceIndex < 0)
        {
            IdCard.Name.First = input;
            return;
        }

        IdCard.Name.First = input.Substring(0, spaceIndex).Trim().ReplaceEmptyWithNull();
        IdCard.Name.Middle = input.Substring(spaceIndex + 1).Trim().ReplaceEmptyWithNull();
    }
}
