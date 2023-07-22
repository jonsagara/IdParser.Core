using IdParser.Core.Metadata;

namespace IdParser.Core.Parsers.Id;

internal static class EyeColorParser
{
    internal static EyeColor? Parse(string input)
    {
        // #3: This handles the unsupported "NONE" value that can come from ON.
        if (ParserHelper.StringHasNoValue(input))
        {
            return null;
        }

        if (input.EqualsIgnoreCase("UNK"))
        {
            return null;
        }

        if (input.EqualsIgnoreCase(EyeColor.Black.GetAbbreviationOrDefault()))
        {
            return EyeColor.Black;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Blue.GetAbbreviationOrDefault()))
        {
            return EyeColor.Blue;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Brown.GetAbbreviationOrDefault()))
        {
            return EyeColor.Brown;
        }
        // California doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BRN"))
        {
            return EyeColor.Brown;
        }
        // Arizona doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BR"))
        {
            return EyeColor.Brown;
        }
        // West Virginia doesn't follow the abbreviation scheme for brown
        else if (input.EqualsIgnoreCase("BN"))
        {
            return EyeColor.Brown;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Dichromatic.GetAbbreviationOrDefault()))
        {
            return EyeColor.Dichromatic;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Gray.GetAbbreviationOrDefault()))
        {
            return EyeColor.Gray;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Green.GetAbbreviationOrDefault()))
        {
            return EyeColor.Green;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Hazel.GetAbbreviationOrDefault()))
        {
            return EyeColor.Hazel;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Maroon.GetAbbreviationOrDefault()))
        {
            return EyeColor.Maroon;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Pink.GetAbbreviationOrDefault()))
        {
            return EyeColor.Pink;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Black.ToString()))
        {
            return EyeColor.Black;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Blue.ToString()))
        {
            return EyeColor.Blue;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Brown.ToString()))
        {
            return EyeColor.Brown;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Dichromatic.ToString()))
        {
            return EyeColor.Dichromatic;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Gray.ToString()))
        {
            return EyeColor.Gray;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Green.ToString()))
        {
            return EyeColor.Green;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Hazel.ToString()))
        {
            return EyeColor.Hazel;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Maroon.ToString()))
        {
            return EyeColor.Maroon;
        }
        else if (input.EqualsIgnoreCase(EyeColor.Pink.ToString()))
        {
            return EyeColor.Pink;
        }

        throw new ArgumentOutOfRangeException(nameof(input), $"Eye color '{input}' not supported by enum {nameof(EyeColor)}.");
    }
}
