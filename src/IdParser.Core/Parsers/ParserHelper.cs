﻿using System.Globalization;

namespace IdParser.Core.Parsers;

internal static class ParserHelper
{
    internal static bool StringHasNoValue(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "NONE"
            || input == "unavl"
            || input == "unavail";
    }

    internal static bool DateHasNoValue(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "00000000";
    }

    internal static DateTime ParseDate(string input, Country? country, AAMVAVersion version)
    {
        const string usaFormat = "MMddyyyy";
        const string canadaFormat = "yyyyMMdd";
        bool tryCanadaFormatFirst = country is not null && country == Country.Canada || version == AAMVAVersion.AAMVA2000;

        // Some jurisdictions, like New Hampshire (version 2013), don't follow the standard and have trailing
        // characters (like 'M') after the date in the same record. In an attempt to parse the date successfully,
        // only try parsing the positions we know should contain a date.
        if (input is not null && input.Length > usaFormat.Length)
        {
            input = input.Substring(0, usaFormat.Length);
        }

        // Some jurisdictions, like Wyoming (version 2009), don't follow the standard and use the wrong date format.
        // In an attempt to parse the ID successfully, attempt to parse using both formats if the first attempt fails.
        // Hopefully between the two one will work.
        if (DateTime.TryParseExact(input, tryCanadaFormatFirst ? canadaFormat : usaFormat, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out var firstAttemptResult))
        {
            return firstAttemptResult;
        }

        if (DateTime.TryParseExact(input, !tryCanadaFormatFirst ? canadaFormat : usaFormat, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out var secondAttemptResult))
        {
            return secondAttemptResult;
        }

        throw new ArgumentException($"Failed to parse the date '{input}' for country '{country}' using version '{version}'.", nameof(input));
    }

    internal static bool? ParseBool(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        switch (input.ToUpperInvariant())
        {
            case "T":
                return true;
            case "Y":
                return true;
            case "N":
                return false;
            case "F":
                return false;
            case "1":
                return true;
            case "0":
                return false;
            case "U":
                // Unknown whether truncated
                return null;
            default:
                return null;
        }
    }
}
