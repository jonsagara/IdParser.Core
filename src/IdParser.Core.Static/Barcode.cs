using System.Globalization;
using System.Text.RegularExpressions;
using IdParser.Core.Static.Constants;
using IdParser.Core.Static.Metadata;
using IdParser.Core.Static.Parsers;

namespace IdParser.Core.Static;

public static class Barcode
{
    /// <summary>
    /// The text should begin with an @ sign (ASCII Decimal 64, Hex 0x40).
    /// </summary>
    internal const char ExpectedComplianceIndicator = (char)64;

    /// <summary>
    /// The second character should be a linefeed (ASCII Decimal 10, Hex 0x0A).
    /// </summary>
    internal const char ExpectedDataElementSeparator = (char)10;

    /// <summary>
    /// The third character should be a Record Separator (ASCII Decimal 30, Hex 0x1E).
    /// </summary>
    internal const char ExpectedRecordSeparator = (char)30;

    /// <summary>
    /// The fourth character should be a carriage return (ASCII Decimal 13, Hex 0x0D).
    /// </summary>
    internal const char ExpectedSegmentTerminator = (char)13;

    /// <summary>
    /// The expected file type should be ANSI, followed by a space.
    /// </summary>
    internal const string ExpectedFileType = "ANSI ";

    /// <summary>
    /// The above, combined, is the expected header in the barcode text. It should be &quot;@\n\u0030\rANSI &quot;.
    /// </summary>
    internal static readonly string ExpectedHeader = $"@{ExpectedSegmentTerminator}{ExpectedDataElementSeparator}{ExpectedRecordSeparator}{ExpectedSegmentTerminator}{ExpectedDataElementSeparator}{ExpectedFileType}";


    /// <summary>
    /// Parses the raw input from the PDF417 barcode into an IdentificationCard or DriversLicense object.
    /// </summary>
    /// <param name="rawPdf417Input">The string to parse the information out of</param>
    /// <param name="validationLevel">
    /// Specifies the level of <see cref="Validation"/> that will be performed.
    /// Strict validation will ensure the input fully conforms to the AAMVA standard.
    /// No validation will be performed if none is specified and exceptions will not be thrown
    /// for elements that do not match or do not adversely affect parsing.
    /// </param>
    public static IdentificationCard Parse(string rawPdf417Input, Validation validationLevel = Validation.Strict)
    {
        ArgumentNullException.ThrowIfNull(rawPdf417Input);

        if (rawPdf417Input.Length < 31)
        {
            throw new ArgumentException($"The input is missing required header elements and is not a valid AAMVA format. Expected at least 31 characters. Received {rawPdf417Input.Length}.", nameof(rawPdf417Input));
        }

        if (validationLevel == Validation.Strict)
        {
            ValidateHeaderFormat(rawPdf417Input);
        }
        else
        {
            rawPdf417Input = Fixes.TryToCorrectHeader(rawPdf417Input);
        }

        var aamvaVersion = ParseAAMVAVersion(rawPdf417Input);
        var idCard = GetIdCardInstance(rawPdf417Input, aamvaVersion);
        var subfileRecords = GetSubfileRecords(rawPdf417Input, aamvaVersion, idCard);

        // We have to parse and retrieve Country from the subfile first because other fields depends on its value.
        var country = ParseCountry(idCard.IssuerIdentificationNumber, aamvaVersion, subfileRecords);
        idCard.Address.Country = country;

        PopulateIdCard(idCard, aamvaVersion, country, subfileRecords, validationLevel);

        return idCard;
    }


    //
    // Private methods
    //

    /// <summary>
    /// Get the <see cref="ExpectedComplianceIndicator"/> from the scanned text.
    /// </summary>
    private static char ParseComplianceIndicator(string input)
        => input.Substring(startIndex: 0, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedDataElementSeparator"/> from the scanned text.
    /// </summary>
    private static char ParseDataElementSeparator(string input)
        => input.Substring(startIndex: 1, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedRecordSeparator"/> from the scanned text.
    /// </summary>
    private static char ParseRecordSeparator(string input)
        => input.Substring(startIndex: 2, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedSegmentTerminator"/> from the scanned text.
    /// </summary>
    private static char ParseSegmentTerminator(string input)
        => input.Substring(startIndex: 3, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedFileType"/> (e.g., ANSI ) from the scanned text.
    /// </summary>
    private static string ParseFileType(string input)
        => input.Substring(startIndex: 4, length: 5);

    /// <summary>
    /// Ensure the header has the required and expected fields.
    /// </summary>
    private static void ValidateHeaderFormat(string input)
    {
        var complianceIndicator = ParseComplianceIndicator(input);
        if (complianceIndicator != ExpectedComplianceIndicator)
        {
            throw new ArgumentException($"The compliance indicator is invalid. Expected '{ExpectedComplianceIndicator.ToHexString()}'. Received '{complianceIndicator.ToHexString()}'.", nameof(input));
        }

        var dataElementSeparator = ParseDataElementSeparator(input);
        if (dataElementSeparator != ExpectedDataElementSeparator)
        {
            throw new ArgumentException($"The data element separator is invalid. Expected '{ExpectedDataElementSeparator.ToHexString()}'. Received '{dataElementSeparator.ToHexString()}'.", nameof(input));
        }

        var recordSeparator = ParseRecordSeparator(input);
        if (recordSeparator != ExpectedRecordSeparator)
        {
            throw new ArgumentException($"The record separator is invalid. Expected '{ExpectedRecordSeparator.ToHexString()}'. Received '{recordSeparator.ToHexString()}'.", nameof(input));
        }

        var segmentTerminator = ParseSegmentTerminator(input);
        if (segmentTerminator != ExpectedSegmentTerminator)
        {
            throw new ArgumentException($"The segment terminator is invalid. Expected '{ExpectedSegmentTerminator.ToHexString()}'. Received '{segmentTerminator.ToHexString()}'.", nameof(input));
        }

        var fileType = ParseFileType(input);
        if (fileType != ExpectedFileType)
        {
            throw new ArgumentException($"The file type is invalid. Expected '{ExpectedFileType}'. Received '{fileType.ToHexString()}'.", nameof(input));
        }
    }

    /// <summary>
    /// Get the AAMVA version number from the scanned text, and convert it to a byte.
    /// </summary>
    private static byte ParseAAMVAVersionNumber(string input)
        => Convert.ToByte(input.Substring(startIndex: 15, length: 2), CultureInfo.InvariantCulture);

    /// <summary>
    /// Gets the AAMVA version of the input.
    /// </summary>
    /// <param name="input">The raw PDF417 barcode data</param>
    private static AAMVAVersion ParseAAMVAVersion(string input)
    {
        ArgumentNullException.ThrowIfNull(nameof(input));

        if (input.Length < 17)
        {
            throw new ArgumentException("Input must not be less than 17 characters in order to parse the AAMVA version.", nameof(input));
        }

        var parsedAAMVAVersionText = input.Substring(startIndex: 15, length: 2);

        if (Enum.TryParse<AAMVAVersion>(parsedAAMVAVersionText, out var version) && Enum.IsDefined(version))
        {
            // We parsed the version number from the text, -AND- the number is actually a defined 
            //   enum value. Return it.
            return version;
        }

        // Unable to parse the version number from the text, -OR- the parsed number is not a defined
        //   enum value. Return "Future".
        return AAMVAVersion.Future;
    }

    /// <summary>
    /// Determines whether the barcode is an <see cref="IdentificationCard"/> or a <see cref="DriversLicense"/>.
    /// </summary>
    /// <remarks>
    /// NOTE: The 2000 spec had the subfile type in a different location.
    /// </remarks>
    private static string ParseSubfileType(string input, AAMVAVersion version)
        => version == AAMVAVersion.AAMVA2000
            ? input.Substring(startIndex: 19, length: 2)
            : input.Substring(startIndex: 21, length: 2);

    /// <summary>
    /// If it's a driver's license, return a <see cref="DriversLicense"/> instance. Otherwise, return an
    /// <see cref="IdentificationCard"/> instance.
    /// </summary>
    private static IdentificationCard GetIdCardInstance(string rawPdf417Input, AAMVAVersion version)
    {
        var idCard = ParseSubfileType(rawPdf417Input, version) == "DL"
            ? new DriversLicense()
            : new IdentificationCard();

        idCard.IssuerIdentificationNumber = (IssuerIdentificationNumber)Convert.ToInt32(rawPdf417Input.Substring(9, 6), CultureInfo.InvariantCulture);
        idCard.AAMVAVersionNumber = version;
        idCard.JurisdictionVersionNumber = version == AAMVAVersion.AAMVA2000
            ? (byte)0
            : Convert.ToByte(rawPdf417Input.Substring(17, 2), CultureInfo.InvariantCulture);

        return idCard;
    }

    /// <summary>
    /// Get the index of the subfile starting position.
    /// </summary>
    private static int ParseSubfileOffset(string rawPdf417Input, AAMVAVersion version, IdentificationCard idCard)
    {
        var offset = 0;

        if (version == AAMVAVersion.AAMVA2000)
        {
            offset = Convert.ToInt32(rawPdf417Input.Substring(startIndex: 21, length: 4), CultureInfo.InvariantCulture);

            // South Carolina's offset is off by one byte which causes the parsing of the IdNumber to fail
            if (idCard.IssuerIdentificationNumber == IssuerIdentificationNumber.SouthCarolina && offset == 30)
            {
                offset--;
            }
        }
        else if (version >= AAMVAVersion.AAMVA2003)
        {
            var offsetAsString = rawPdf417Input.Substring(startIndex: 23, length: 4);

            // Alberta specifies characters, like "abac", in the place of the expected offset
            // which causes the parsing of the subfile records to fail
            if (offsetAsString.All(char.IsDigit))
            {
                offset = Convert.ToInt32(offsetAsString, CultureInfo.InvariantCulture);
            }
        }

        if (offset == 0)
        {
            // Some jurisdictions, like Ontario, have a zero offset, which is incorrect.
            // Set the offset to the start of the subfile type indicator.
            var subfileRegex = new Regex("(DL|ID)([\\d\\w]{3,8})(DL|ID|Z\\w)([DZ][A-Z]{2})");
            var match = subfileRegex.Match(rawPdf417Input);

            if (match.Success)
            {
                const int subfileTypeLength = 2;
                const int firstElementIdLength = 3;

                offset = match.Index + match.Length - subfileTypeLength - firstElementIdLength;
            }
        }

        return offset;
    }

    /// <summary>
    /// Get a list of all the data records (name and value as a single string) that we need to parse.
    /// </summary>
    private static Dictionary<string, string> GetSubfileRecords(string rawPdf417Input, AAMVAVersion version, IdentificationCard idCard)
    {
        var ixSubfile = ParseSubfileOffset(rawPdf417Input, version, idCard);

        var records = rawPdf417Input
            .Substring(startIndex: ixSubfile)
            .Split(new[] { ParseDataElementSeparator(rawPdf417Input), ParseSegmentTerminator(rawPdf417Input) }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (records[0] == "DL" || records[0] == "ID")
        {
            // We don't care about a record that just says "DL" or "ID", so remove it.
            //   We already know whether it's a DL or ID.
            records.RemoveAt(0);
        }
        else if (records[0].StartsWith("DL", StringComparison.Ordinal) || records[0].StartsWith("ID", StringComparison.Ordinal))
        {
            // The initial record starts with DL or ID. Discard the first two characters, and keep everything after it.
            records[0] = records[0].Substring(startIndex: 2);
        }

        // Discard any records with length < 3 because we can't parse them into element Ids.
        // First three characters are the element id.
        // The remaining characters are the value.
        return records
            .Where(r => r.Length < 3)
            .ToDictionary(r => r.Substring(startIndex: 0, length: 3), r => r.Substring(startIndex: 3).Trim());
    }

    /// <summary>
    /// Parses the country based on the DCG subfile record.
    /// Gets the country from the IIN if no matching subfile record was found.
    /// </summary>
    private static Country ParseCountry(IssuerIdentificationNumber iin, AAMVAVersion version, Dictionary<string, string> subfileRecords)
    {
        // Country is not a subfile record in the AAMVA 2000 standard.
        if (version == AAMVAVersion.AAMVA2000)
        {
            return Country.Usa;
        }

        if (subfileRecords.TryGetValue(SubfileElementIds.Country, out string? data))
        {
            if (data == "USA")
            {
                return Country.Usa;
            }

            if (data == "CAN" || data == "CDN")
            {
                return Country.Canada;
            }
        }

        return IssuerMetadataHelper.GetCountry(iin);
    }

    private static void PopulateIdCard(IdentificationCard idCard, AAMVAVersion version, Country country, Dictionary<string, string> subfileRecords, Validation validationLevel)
    {
        foreach (var elementId in subfileRecords.Keys)
        {
            var data = subfileRecords[elementId];

            if (elementId.StartsWith("Z", StringComparison.Ordinal) && !idCard.AdditionalJurisdictionElements.ContainsKey(elementId))
            {
                idCard.AdditionalJurisdictionElements.Add(elementId, data);
                continue;
            }

            try
            {
                Parser.ParseAndSetIdElements(elementId: elementId, data: data, country, idCard);

                if (idCard is DriversLicense driversLicense)
                {
                    Parser.ParseAndSetDriversLicenseElements(elementId: elementId, data: data, country, driversLicense);
                }
            }
            catch (Exception ex)
            {
#warning TODO: how to log exceptions without taking a dependency on Microsoft.Extensions.Logging?
                Console.WriteLine($"Unhandled exception while trying to parse element Id {elementId}: {ex}");

                if (validationLevel == Validation.Strict)
                {
                    throw;
                }
            }
        }
    }
}
