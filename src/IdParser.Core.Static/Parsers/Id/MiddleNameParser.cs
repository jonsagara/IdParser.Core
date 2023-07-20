namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAD")]
internal static class MiddleNameParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        IdCard.Name.Middle = input;
    }
}
