namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DCS")]
internal static class LastNameParser
{
    internal static string ParseAndSet(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Name.Last = input.TrimEnd(',');
    }
}
