using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.License;

[Parser("DDC")]
public class HazmatEndorsementExpirationDateParser : AbstractParser
{
    public HazmatEndorsementExpirationDateParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (DateHasNoValue(input) || Version < Version.Aamva2000)
        {
            return;
        }

        License.HazmatEndorsementExpirationDate = ParseDate(input);
    }
}
