namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCO")]
internal static class StandardRestrictionCodeParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.StandardRestrictionCode = input;
    }
}
