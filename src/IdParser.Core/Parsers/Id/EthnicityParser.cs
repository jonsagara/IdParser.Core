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
}
