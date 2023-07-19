﻿using IdParser.Attributes;

namespace IdParser.Parsers.License;

[Parser("DCD")]
public class EndorsementCodesParser : AbstractParser
{
    public EndorsementCodesParser(IdentificationCard idCard, Version version, Country country) : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodes = input;
    }
}

[Parser("DAT")]
public class EndorsementCodesLegacyParser : AbstractParser
{
    public EndorsementCodesLegacyParser(IdentificationCard idCard, Version version, Country country) : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.EndorsementCodes = input;
    }
}
