namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCN")]
internal static class StandardEndorsementCodeParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.StandardEndorsementCode = input;
    }
}
