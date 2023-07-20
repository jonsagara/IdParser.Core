﻿using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.License;

[Parser("DCQ")]
public class EndorsementCodeDescriptionParser : AbstractParser
{
    public EndorsementCodeDescriptionParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodeDescription = input;
    }
}
