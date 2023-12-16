namespace IdParser.Core.Parsers.Id;

internal static class EthnicityParser
{
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


    private static Field<Ethnicity?> ParsedValue(string elementId, Ethnicity? value, string? rawValue)
        => FieldHelpers.ParsedField(elementId: elementId, value: value, rawValue: rawValue);
}
