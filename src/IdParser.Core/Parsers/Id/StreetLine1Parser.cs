namespace IdParser.Core.Parsers.Id;

internal static class StreetLine1Parser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return input.TrimEnd(',');
    }
}

internal static class StreetLine1LegacyParser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return input.TrimEnd(',');
    }
}
