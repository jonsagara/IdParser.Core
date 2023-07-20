namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DAG")]
internal static class StreetLine1Parser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Address.StreetLine1 = input.TrimEnd(',');
    }
}

//[Parser("DAL")]
internal static class StreetLine1LegacyParser
{
    internal static string Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Address.StreetLine1 = input.TrimEnd(',');
    }
}
