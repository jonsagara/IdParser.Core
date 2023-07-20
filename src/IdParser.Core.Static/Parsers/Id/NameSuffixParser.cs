namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCU")]
internal static class NameSuffixParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        IdCard.Name.Suffix = input;
    }
}
