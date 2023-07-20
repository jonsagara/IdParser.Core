namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DBC")]
internal static class SexParser
{
    internal static Sex? ParseAndSet(string input)
    {
        if (byte.TryParse(input, out var numericSex))
        {
            IdCard.Sex = (Sex)numericSex;
        }
        else if (input.Equals("M", StringComparison.OrdinalIgnoreCase))
        {
            IdCard.Sex = Sex.Male;
        }
        else if (input.Equals("F", StringComparison.OrdinalIgnoreCase))
        {
            IdCard.Sex = Sex.Female;
        }
    }
}
