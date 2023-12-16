using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace IdParser.Core.Parsers;

internal static class ParserHelper
{
    /// <summary>
    /// Returns true if the string is null or white space, or contains one of the special strings denoting
    /// no value; false otherwise.
    /// </summary>
    internal static bool StringHasNoValue([NotNullWhen(false)] string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "NONE"
            || input == "unavl"
            || input == "unavail";
    }

    /// <summary>
    /// Returns true if the string is null or white space, or contains one of the special strings denoting
    /// no value; false otherwise.
    /// </summary>
    internal static bool DateHasNoValue(string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "00000000";
    }

    /// <summary>
    /// Parse the date accouring to the country and/or AAMVAVersion.
    /// </summary>
    internal static Field<DateTime?> ParseDate(string elementId, string? rawValue, Country? country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        const string usaFormat = "MMddyyyy";
        const string canadaFormat = "yyyyMMdd";
        bool tryCanadaFormatFirst = country is not null && country == Country.Canada || version == AAMVAVersion.AAMVA2000;

        // Some jurisdictions, like New Hampshire (version 2013), don't follow the standard and have trailing
        // characters (like 'M') after the date in the same record. In an attempt to parse the date successfully,
        // only try parsing the positions we know should contain a date.
        if (rawValue is not null && rawValue.Length > usaFormat.Length)
        {
            rawValue = rawValue.Substring(0, usaFormat.Length);
        }

        // Some jurisdictions, like Wyoming (version 2009), don't follow the standard and use the wrong date format.
        // In an attempt to parse the ID successfully, attempt to parse using both formats if the first attempt fails.
        // Hopefully between the two one will work.
        if (DateTime.TryParseExact(rawValue, tryCanadaFormatFirst ? canadaFormat : usaFormat, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out var firstAttemptResult))
        {
            return new Field<DateTime?>(ElementId: elementId, Value: firstAttemptResult, RawValue: rawValue, Error: null, Present: true);
        }

        if (DateTime.TryParseExact(rawValue, !tryCanadaFormatFirst ? canadaFormat : usaFormat, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out var secondAttemptResult))
        {
            //return new Field<DateTime?>(ElementId: elementId, Value: secondAttemptResult, RawValue: input, Error: null, Present: true);
            return FieldHelpers.ParsedField<DateTime?>(elementId: elementId, value: secondAttemptResult, rawValue: rawValue);
        }

        return FieldHelpers.UnparsedField<DateTime?>(elementId: elementId, rawValue: rawValue, $"[{elementId}] Failed to parse the date '{rawValue}' for country '{country}' using version '{version}'.");
    }

    /// <summary>
    /// Parse the boolean value based on special strings that denote true or false.
    /// </summary>
    internal static bool? ParseBool(string? rawValue)
    {
        return (rawValue?.ToUpperInvariant()) switch
        {
            "T" => true,
            "Y" => true,
            "N" => false,
            "F" => false,
            "1" => true,
            "0" => false,
            "U" => null,// Unknown whether truncated
            _ => null,
        };
    }
}
