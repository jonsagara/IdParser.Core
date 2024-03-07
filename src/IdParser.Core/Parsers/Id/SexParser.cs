namespace IdParser.Core.Parsers.Id;

internal static class SexParser
{
    internal static Field<Sex?> Parse(string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        if (ParserHelper.StringHasNoValue(rawValue))
        {
            return FieldHelpers.ParsedField<Sex?>(elementId: elementId, value: null, rawValue: rawValue);
        }

        if (Enum.TryParse(rawValue.AsSpan(), ignoreCase: true, out Sex sex) && Enum.IsDefined(sex))
        {
            return FieldHelpers.ParsedField<Sex?>(elementId: elementId, value: sex, rawValue: rawValue);
        }

        if (rawValue.Equals("M", StringComparison.OrdinalIgnoreCase))
        {
            return FieldHelpers.ParsedField<Sex?>(elementId: elementId, value: Sex.Male, rawValue: rawValue);
        }

        if (rawValue.Equals("F", StringComparison.OrdinalIgnoreCase))
        {
            return FieldHelpers.ParsedField<Sex?>(elementId: elementId, value: Sex.Female, rawValue: rawValue);
        }

        return FieldHelpers.ParsedField<Sex?>(elementId: elementId, value: Sex.NotSpecified, rawValue: rawValue);
    }
}
