namespace IdParser.Core.Static.Parsers.Id;

internal static class LastNameParser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return input.TrimEnd(',');
    }
}
