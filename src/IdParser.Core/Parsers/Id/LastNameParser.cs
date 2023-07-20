using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DCS")]
public class LastNameParser : AbstractParser
{
    public LastNameParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Name.Last = input.TrimEnd(',');
    }
}
