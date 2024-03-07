using IdParser.Core.Constants;

namespace IdParser.Core.Parsers.Id;

internal static class HairColorParser
{
    internal static Field<HairColor?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (string.IsNullOrEmpty(rawValue) || rawValue.EqualsIgnoreCase("UNK"))
        {
            return ParsedValue(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (rawValue.EqualsIgnoreCase(HairColor.Bald.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Bald, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Black.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Black, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Blond.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Blond, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Brown.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Brown, rawValue: rawValue);
        }
        // California doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BRN"))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Brown, rawValue: rawValue);
        }
        // Arizona doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BR"))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Brown, rawValue: rawValue);
        }
        // West Virginia doesn't follow the abbreviation scheme for brown
        else if (rawValue.EqualsIgnoreCase("BN"))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Brown, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Gray.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Gray, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.RedAuburn.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.RedAuburn, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Sandy.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Sandy, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.White.GetAbbreviationOrDefault()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.White, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Bald.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Bald, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Black.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Black, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Blond.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Blond, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Brown.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Brown, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Gray.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Gray, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.RedAuburn.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.RedAuburn, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.Sandy.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.Sandy, rawValue: rawValue);
        }
        else if (rawValue.EqualsIgnoreCase(HairColor.White.ToString()))
        {
            return ParsedValue(elementId: elementId, value: HairColor.White, rawValue: rawValue);
        }

        return FieldHelpers.UnparsedField<HairColor?>(elementId: elementId, rawValue: rawValue, $"Unable to parse Hair Color from field '{SubfileElementIds.HairColor}': '{rawValue}' is not supported by enum {nameof(HairColor)}.");
    }


    private static Field<HairColor?> ParsedValue(string elementId, HairColor? value, string? rawValue)
        => FieldHelpers.ParsedField(elementId: elementId, value: value, rawValue: rawValue);
}
