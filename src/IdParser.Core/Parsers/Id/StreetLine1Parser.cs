﻿using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DAG")]
public class StreetLine1Parser : AbstractParser
{
    public StreetLine1Parser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Address.StreetLine1 = input.TrimEnd(',');
    }
}

[Parser("DAL")]
public class StreetLine1LegacyParser : AbstractParser
{
    public StreetLine1LegacyParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        IdCard.Address.StreetLine1 = input.TrimEnd(',');
    }
}