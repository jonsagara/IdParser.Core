﻿using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DCK")]
public class InventoryControlNumberParser : AbstractParser
{
    public InventoryControlNumberParser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        IdCard.InventoryControlNumber = input;
    }
}