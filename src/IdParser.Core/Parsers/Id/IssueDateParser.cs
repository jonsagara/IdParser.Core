using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DBD")]
public class IssueDateParser : AbstractParser
{
    public IssueDateParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (DateHasNoValue(input))
        {
            return;
        }

        IdCard.IssueDate = ParseDate(input);
    }
}
