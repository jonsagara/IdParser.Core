namespace IdParser.Core.Static.Parsers.Id;

internal record FirstNameParts(string? First, string? Middle);

internal static class FirstNameParser
{
    internal static FirstNameParts? Parse(string input)
    {
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        // According to A.2.1 (AAMVA Person Name Rule) in the D20 Data Dictionary,
        // first names can only contain alphabetic characters. All other characters, such as spaces,
        // are deleted and not encoded. Middle names are allowed to have spaces.
        // Jurisdictions like Wyoming put the middle initial in the first name field.

        var spaceIndex = input.LastIndexOf(' ');
        if (spaceIndex < 0)
        {
            return new FirstNameParts(First: input, Middle: null);
        }

        // First name is everything up to the last space.
        var first = input.Substring(startIndex: 0, length: spaceIndex).Trim().ReplaceEmptyWithNull();

        // Middle name is everything after the space.
        var middle = input.Substring(startIndex: spaceIndex + 1).Trim().ReplaceEmptyWithNull();

        return new FirstNameParts(First: first, Middle: middle);
    }
}
