﻿using System.Text.RegularExpressions;
using IdParser.Core.Attributes;

namespace IdParser.Core.Parsers.Id;

[Parser("DAH")]
public class StreetLine2Parser : AbstractParser
{
    public StreetLine2Parser(IdentificationCard idCard, Version version, Country country)
        : base(idCard, version, country)
    {
    }

    public override void ParseAndSet(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        // Jurisdictions like Wyoming set the StreetLine2 to the City, State, and Postal Code when it
        if (IdCard.Address.City is not null &&
            IdCard.Address.JurisdictionCode is not null &&
            IdCard.Address.PostalCode is not null &&
            Regex.IsMatch(input, $@"\s*{IdCard.Address.City}(\s|,)*{IdCard.Address.JurisdictionCode}(\s|,)*{IdCard.Address.PostalCode}"))
        {
            return;
        }

        IdCard.Address.StreetLine2 = input.TrimEnd(',');
    }
}