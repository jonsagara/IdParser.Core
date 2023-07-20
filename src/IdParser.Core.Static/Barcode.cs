using System.Globalization;

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
        //var subfileRecords = GetSubfileRecords(idCard, aamvaVersion, rawPdf417Input);
        //var country = ParseCountry(idCard.IssuerIdentificationNumber, aamvaVersion, subfileRecords);
        //idCard.Address.Country = country;

        //PopulateIdCard(idCard, aamvaVersion, country, subfileRecords, validationLevel);

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
}
