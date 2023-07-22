namespace IdParser.Core.Parsers.Id;

//[Parser("DAZ")]
internal static class HairColorParser
{
    internal static HairColor? Parse(string input)
    {
        if (string.IsNullOrEmpty(input) || input.EqualsIgnoreCase("UNK"))
        {
            return null;
        }

        if (input.EqualsIgnoreCase(HairColor.Bald.GetAbbreviationOrDefault()))
        {
            return HairColor.Bald;
        }
        else if (input.EqualsIgnoreCase(HairColor.Black.GetAbbreviationOrDefault()))
        {
            return HairColor.Black;
        }
        else if (input.EqualsIgnoreCase(HairColor.Blond.GetAbbreviationOrDefault()))
        {
            return HairColor.Blond;
        }
        else if (input.EqualsIgnoreCase(HairColor.Brown.GetAbbreviationOrDefault()))
        {
            return HairColor.Brown;
        }
        // California doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BRN"))
        {
            return HairColor.Brown;
        }
        // Arizona doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BR"))
        {
            return HairColor.Brown;
        }
        // West Virginia doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BN"))
        {
            return HairColor.Brown;
        }
        else if (input.EqualsIgnoreCase(HairColor.Gray.GetAbbreviationOrDefault()))
        {
            return HairColor.Gray;
        }
        else if (input.EqualsIgnoreCase(HairColor.RedAuburn.GetAbbreviationOrDefault()))
        {
            return HairColor.RedAuburn;
        }
        else if (input.EqualsIgnoreCase(HairColor.Sandy.GetAbbreviationOrDefault()))
        {
            return HairColor.Sandy;
        }
        else if (input.EqualsIgnoreCase(HairColor.White.GetAbbreviationOrDefault()))
        {
            return HairColor.White;
        }
        else if (input.EqualsIgnoreCase(HairColor.Bald.ToString()))
        {
            return HairColor.Bald;
        }
        else if (input.EqualsIgnoreCase(HairColor.Black.ToString()))
        {
            return HairColor.Black;
        }
        else if (input.EqualsIgnoreCase(HairColor.Blond.ToString()))
        {
            return HairColor.Blond;
        }
        else if (input.EqualsIgnoreCase(HairColor.Brown.ToString()))
        {
            return HairColor.Brown;
        }
        else if (input.EqualsIgnoreCase(HairColor.Gray.ToString()))
        {
            return HairColor.Gray;
        }
        else if (input.EqualsIgnoreCase(HairColor.RedAuburn.ToString()))
        {
            return HairColor.RedAuburn;
        }
        else if (input.EqualsIgnoreCase(HairColor.Sandy.ToString()))
        {
            return HairColor.Sandy;
        }
        else if (input.EqualsIgnoreCase(HairColor.White.ToString()))
        {
            return HairColor.White;
        }

        throw new ArgumentOutOfRangeException(nameof(input), $"Hair color '{input}' not supported by enum {nameof(HairColor)}.");
    }
}
