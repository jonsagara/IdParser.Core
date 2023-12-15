namespace IdParser.Core.Parsers.Id;

internal static class EthnicityParser
{
    internal static Ethnicity? Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.EqualsIgnoreCase("U"))
        {
            return null;
        }

        if (input.EqualsIgnoreCase(Ethnicity.AlaskanAmericanIndian.GetAbbreviationOrDefault()))
        {
            return Ethnicity.AlaskanAmericanIndian;
        }
        else if (input.EqualsIgnoreCase(Ethnicity.AsianPacificIslander.GetAbbreviationOrDefault()))
        {
            return Ethnicity.AsianPacificIslander;
        }
        else if (input.EqualsIgnoreCase(Ethnicity.Black.GetAbbreviationOrDefault()))
        {
            return Ethnicity.Black;
        }
        else if (input.EqualsIgnoreCase(Ethnicity.HispanicOrigin.GetAbbreviationOrDefault()))
        {
            return Ethnicity.HispanicOrigin;
        }
        else if (input.EqualsIgnoreCase(Ethnicity.NonHispanic.GetAbbreviationOrDefault()))
        {
            return Ethnicity.NonHispanic;
        }
        else if (input.EqualsIgnoreCase(Ethnicity.White.GetAbbreviationOrDefault()))
        {
            return Ethnicity.White;
        }

        return null;
    }


    private static Field<Ethnicity?> ParsedValue(string elementId, Ethnicity? value, string? rawValue)
        => FieldHelpers.ParsedField(elementId: elementId, value: value, rawValue: rawValue);

    internal static Field<Ethnicity?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (string.IsNullOrWhiteSpace(rawValue) || rawValue.EqualsIgnoreCase("U"))
        {
            return ParsedValue(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (rawValue.EqualsIgnoreCase(Ethnicity.AlaskanAmericanIndian.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.AlaskanAmericanIndian, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(Ethnicity.AsianPacificIslander.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.AsianPacificIslander, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(Ethnicity.Black.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.Black, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(Ethnicity.HispanicOrigin.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.HispanicOrigin, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(Ethnicity.NonHispanic.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.NonHispanic, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(Ethnicity.White.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: Ethnicity.White, rawValue: rawValue);
        }

        return ParsedValue(elementId: elementId, value: null, rawValue: rawValue);
    }
}
