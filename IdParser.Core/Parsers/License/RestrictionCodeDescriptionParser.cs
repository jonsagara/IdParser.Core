using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.License;

[Parser("DCR")]
public class RestrictionCodeDescriptionParser : AbstractParser
{
    public RestrictionCodeDescriptionParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.RestrictionCodeDescription = input;
    }
}
