namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCF")]
internal static class DocumentDiscriminatorParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        IdCard.DocumentDiscriminator = input;
    }
}
