namespace IdParser.Core.Static.Parsers.Id;

internal static class SexParser
{
    internal static Sex Parse(string input)
    {
        if (Enum.TryParse(input.AsSpan(), ignoreCase: true, out Sex sex) && Enum.IsDefined(sex))
        {
            return sex;
        }

        if (input.Equals("M", StringComparison.OrdinalIgnoreCase))
        {
            return Sex.Male;
        }

        if (input.Equals("F", StringComparison.OrdinalIgnoreCase))
        {
            return Sex.Female;
        }

        return Sex.NotSpecified;
    }
}
