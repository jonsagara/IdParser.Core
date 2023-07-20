using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DDL")]
public class IsVeteranParser : AbstractParser
{
    public IsVeteranParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        IdCard.IsVeteran = ParseBool(input) ?? false;
    }
}
