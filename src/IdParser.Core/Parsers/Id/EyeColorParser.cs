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
        // New Jersey doesn't follow the abbreviation scheme for hazel
        else if (input.EqualsIgnoreCase("HZL"))
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


    private static Field<EyeColor?> ParsedValue(string elementId, EyeColor? value, string? rawValue)
        => FieldHelpers.ParsedField(elementId: elementId, value: value, rawValue: rawValue);


    internal static Field<EyeColor?> Parse2(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        // #3: This handles the unsupported "NONE" value that can come from ON.
        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return ParsedValue(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (rawValue.EqualsIgnoreCase("UNK"))
        {
            return ParsedValue(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (rawValue.EqualsIgnoreCase(EyeColor.Black.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Black, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Blue.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Blue, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Brown.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Brown, rawValue: rawValue);
        }
        // California doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BRN"))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Brown, rawValue: rawValue);
        }
        // Arizona doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BR"))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Brown, rawValue: rawValue);
        }
        // West Virginia doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BN"))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Brown, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Dichromatic.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Dichromatic, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Gray.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Gray, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Green.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Green, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Hazel.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Hazel, rawValue: rawValue);
        }
        // New Jersey doesn't follow the abbreviation scheme for hazel
        else if (rawValue.EqualsIgnoreCase("HZL"))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Hazel, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Maroon.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Maroon, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Pink.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Pink, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Black.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Black, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Blue.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Blue, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Brown.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Brown, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Dichromatic.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Dichromatic, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Gray.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Gray, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Green.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Green, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Hazel.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Hazel, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Maroon.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Maroon, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(EyeColor.Pink.ToString()))
        {
            return ParsedValue(elementId: elementId, value: EyeColor.Pink, rawValue: rawValue);
        }

        return FieldHelpers.UnparsedField<EyeColor?>(elementId: elementId, rawValue: rawValue, $"Eye color '{rawValue}' not supported by enum {nameof(EyeColor)}.");
    }
}
