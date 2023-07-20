using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DDJ")]
public class Under21UntilParser : AbstractParser
{
    public Under21UntilParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        IdCard.Under21Until = ParseDate(input);
    }
}
