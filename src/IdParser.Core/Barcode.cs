using System.Globalization;
using System.Text.RegularExpressions;
using IdParser.Core.Constants;
using IdParser.Core.Parsers;
using Microsoft.Extensions.Logging;

namespace IdParser.Core;

public record BarcodeParseResult(
    IdentificationCard Card,
    IReadOnlyCollection<string> UnhandledElementIds
    );

public record BarcodeParseResult2(
    IdentificationCard2 Card
    );

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
    /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use to create an <see cref="ILogger"/> for logging.</param>
    public static BarcodeParseResult Parse(string rawPdf417Input, Validation validationLevel = Validation.Strict, ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(rawPdf417Input);

        if (rawPdf417Input.Length < 31)
        {
            throw new ArgumentException($"The input is missing required header elements and is not a valid AAMVA format. Expected at least 31 characters. Received {rawPdf417Input.Length}.", nameof(rawPdf417Input));
        }

        ILogger? logger = loggerFactory?.CreateLogger(typeof(Barcode));

        if (validationLevel == Validation.Strict)
        {
            ValidateHeaderFormat(rawPdf417Input);
        }
        else
        {
            rawPdf417Input = Fixes.TryToCorrectHeader(rawPdf417Input, loggerFactory);
        }

        var aamvaVersionResult = ParseAAMVAVersion(rawPdf417Input);
        var idCard = GetIdCardInstance(rawPdf417Input, aamvaVersionResult.Version);
        var subfileRecords = GetSubfileRecords(rawPdf417Input, aamvaVersionResult.Version, idCard);

        // We have to parse and retrieve Country from the subfile first because other fields depends on its value.
        var country = ParseCountry(idCard.IssuerIdentificationNumber, aamvaVersionResult.Version, subfileRecords);
        idCard.Address.Country = country;

        var unhandledElementIds = PopulateIdCard(idCard, aamvaVersionResult.Version, country, subfileRecords, logger);
        if (unhandledElementIds.Count > 0)
        {
            logger?.LogError($"One or more ElementIds were not handled by the ID or Driver's License parsers: {{UnhandledElementIds}}", string.Join(", ", unhandledElementIds));
        }

        return new BarcodeParseResult(idCard, unhandledElementIds);
    }

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
    /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use to create an <see cref="ILogger"/> for logging.</param>
    public static BarcodeParseResult2 Parse2(string rawPdf417Input, Validation validationLevel = Validation.Strict, ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(rawPdf417Input);

        if (rawPdf417Input.Length < 31)
        {
            throw new ArgumentException($"The input is missing required header elements and is not a valid AAMVA format. Expected at least 31 characters. Received {rawPdf417Input.Length}.", nameof(rawPdf417Input));
        }

        ILogger? logger = loggerFactory?.CreateLogger(typeof(Barcode));

        if (validationLevel == Validation.Strict)
        {
            ValidateHeaderFormat(rawPdf417Input);
        }
        else
        {
            rawPdf417Input = Fixes.TryToCorrectHeader(rawPdf417Input, loggerFactory);
        }

        //var aamvaVersion = ParseAAMVAVersion(rawPdf417Input);
        //var idCard = GetIdCardInstance(rawPdf417Input, aamvaVersion);
        //var subfileRecords = GetSubfileRecords(rawPdf417Input, aamvaVersion, idCard);

        //// We have to parse and retrieve Country from the subfile first because other fields depends on its value.
        //var country = ParseCountry(idCard.IssuerIdentificationNumber, aamvaVersion, subfileRecords);
        //idCard.Address.Country = country;

        //var unhandledElementIds = PopulateIdCard(idCard, aamvaVersion, country, subfileRecords, logger);
        //if (unhandledElementIds.Count > 0)
        //{
        //    logger?.LogError($"One or more ElementIds were not handled by the ID or Driver's License parsers: {{UnhandledElementIds}}", string.Join(", ", unhandledElementIds));
        //}

        //return new BarcodeParseResult(idCard, unhandledElementIds);

        var aamvaVersionResult = ParseAAMVAVersion(rawPdf417Input);
        var idCard = GetIdCardInstance2(rawPdf417Input, aamvaVersionResult);

#warning TODO: Need to bail here because we couldn't parse the IssuerIdentificationNumber, and we can't continue trying to parse the rest of the ID.

        var subfileRecords = GetSubfileRecords2(rawPdf417Input, idCard.AAMVAVersionNumber.Value, idCard);

        // We have to parse and retrieve Country from the subfile first because other fields depends on its value.
        var country = ParseCountry(idCard.IssuerIdentificationNumber.Value, idCard.AAMVAVersionNumber.Value, subfileRecords);
        //idCard.Address.Country = country;

        PopulateIdCard2(idCard, idCard.AAMVAVersionNumber.Value, country, subfileRecords, logger);
        if (idCard.UnhandledElementIds.Count > 0)
        {
            logger?.LogError($"One or more ElementIds were not handled by the ID or Driver's License parsers: {{UnhandledElementIds}}", string.Join(", ", idCard.UnhandledElementIds));
        }

        return new BarcodeParseResult2(idCard);
    }


    //
    // Private methods
    //

    /// <summary>
    /// Get the <see cref="ExpectedComplianceIndicator"/> from the scanned text.
    /// </summary>
    private static char ParseComplianceIndicator(string input)
        => input.AsSpan(start: 0, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedDataElementSeparator"/> from the scanned text.
    /// </summary>
    private static char ParseDataElementSeparator(string input)
        => input.AsSpan(start: 1, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedRecordSeparator"/> from the scanned text.
    /// </summary>
    private static char ParseRecordSeparator(string input)
        => input.AsSpan(start: 2, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedSegmentTerminator"/> from the scanned text.
    /// </summary>
    private static char ParseSegmentTerminator(string input)
        => input.AsSpan(start: 3, length: 1)[0];

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

    private readonly record struct AAMVAVersionResult(AAMVAVersion Version, string RawValue);

    /// <summary>
    /// Gets the AAMVA version of the input. If not a defined <see cref="AAMVAVersion"/> enum value, return &quot;Future&quot;.
    /// </summary>
    /// <param name="input">The raw PDF417 barcode data</param>
    private static AAMVAVersionResult ParseAAMVAVersion(string input)
    {
        ArgumentNullException.ThrowIfNull(nameof(input));

        if (input.Length < 17)
        {
            throw new ArgumentException("Input must not be less than 17 characters in order to parse the AAMVA version.", nameof(input));
        }

        var aamvaVersionRawValue = input.Substring(startIndex: 15, length: 2);
        if (Enum.TryParse<AAMVAVersion>(aamvaVersionRawValue.AsSpan(), out var version) && Enum.IsDefined(version))
        {
            // We parsed the version number from the text, -AND- the number is actually a defined 
            //   enum value. Return it.
            return new AAMVAVersionResult(Version: version, RawValue: aamvaVersionRawValue);
        }

        // Unable to parse the version number from the text, -OR- the parsed number is not a defined
        //   enum value. Return "Future".
        return new AAMVAVersionResult(Version: AAMVAVersion.Future, RawValue: aamvaVersionRawValue);
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

        idCard.IssuerIdentificationNumber = (IssuerIdentificationNumber)int.Parse(rawPdf417Input.AsSpan(9, 6), provider: CultureInfo.InvariantCulture);
        idCard.AAMVAVersionNumber = version;
        idCard.JurisdictionVersionNumber = version == AAMVAVersion.AAMVA2000
            ? 0
            : int.Parse(rawPdf417Input.AsSpan(17, 2), provider: CultureInfo.InvariantCulture);

        return idCard;
    }

    /// <summary>
    /// If it's a driver's license, return a <see cref="DriversLicense2"/> instance. Otherwise, return an
    /// <see cref="IdentificationCard2"/> instance.
    /// </summary>
    private static IdentificationCard2 GetIdCardInstance2(string rawPdf417Input, AAMVAVersionResult aamvaVersionResult)
    {
        var idCard = ParseSubfileType(rawPdf417Input, aamvaVersionResult.Version) == "DL"
            ? new DriversLicense2()
            : new IdentificationCard2();


        var issuerIdentificationNumberRawValue = rawPdf417Input.AsSpan(9, 6).ToString();

        idCard.IssuerIdentificationNumber = int.TryParse(issuerIdentificationNumberRawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out var issuerIdentificationNumber)
            ? FieldHelpers.ParsedField(elementId: null, (IssuerIdentificationNumber)issuerIdentificationNumber, issuerIdentificationNumberRawValue)
            : FieldHelpers.UnparsedField<IssuerIdentificationNumber>(elementId: null, rawValue: issuerIdentificationNumberRawValue, $"[IssuerIdentificationNumber] Unable to parse Issuer Identification Number from value '{issuerIdentificationNumberRawValue}'.");

        idCard.AAMVAVersionNumber = FieldHelpers.ParsedField(elementId: null, value: aamvaVersionResult.Version, rawValue: aamvaVersionResult.RawValue);


        var jurisdictionVersionNumberRawValue = rawPdf417Input.Substring(startIndex: 17, length: 2);

        if (aamvaVersionResult.Version == AAMVAVersion.AAMVA2000)
        {
            // Evidently, the 2000 spec didn't have this field, or has an implied version number of 0.
            idCard.JurisdictionVersionNumber = FieldHelpers.ParsedField(elementId: null, value: 0, rawValue: null);
        }
        else
        {
            idCard.JurisdictionVersionNumber = int.TryParse(jurisdictionVersionNumberRawValue.AsSpan(), NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out var jurisdictionVersionNumber)
                ? FieldHelpers.ParsedField(elementId: null, value: jurisdictionVersionNumber, rawValue: jurisdictionVersionNumberRawValue)
                : FieldHelpers.UnparsedField<int>(elementId: null, rawValue: jurisdictionVersionNumberRawValue, error: $"[JurisdictionVersionNumber] Unable to parse Jurisdiction Version Number from value '{jurisdictionVersionNumberRawValue}'.");
        }

        return idCard;
    }


    private static readonly Regex _rxSubfile = new Regex("(DL|ID)([\\d\\w]{3,8})(DL|ID|Z\\w)([DZ][A-Z]{2})", RegexOptions.Compiled);

    /// <summary>
    /// Get the index of the subfile starting position.
    /// </summary>
    private static int ParseSubfileOffset(string rawPdf417Input, AAMVAVersion version, IdentificationCard idCard)
    {
        var offset = 0;

        if (version == AAMVAVersion.AAMVA2000)
        {
            offset = int.Parse(rawPdf417Input.AsSpan(start: 21, length: 4), provider: CultureInfo.InvariantCulture);

            // South Carolina's offset is off by one byte which causes the parsing of the IdNumber to fail
            if (idCard.IssuerIdentificationNumber == IssuerIdentificationNumber.SouthCarolina && offset == 30)
            {
                offset--;
            }
        }
        else if (version >= AAMVAVersion.AAMVA2003)
        {
            // Change to use Span to reduce memory allocations.
            var offsetAsSpan = rawPdf417Input.AsSpan(start: 23, length: 4);
            var allOffsetCharsAreDigits = true;

            foreach (var offsetChar in offsetAsSpan)
            {
                if (char.IsDigit(offsetChar))
                {
                    continue;
                }

                allOffsetCharsAreDigits = false;
                break;
            }

            // Alberta specifies characters, like "abac", in the place of the expected offset
            // which causes the parsing of the subfile records to fail.
            if (allOffsetCharsAreDigits)
            {
                offset = int.Parse(offsetAsSpan, provider: CultureInfo.InvariantCulture);
            }
        }

        if (offset == 0)
        {
            // Some jurisdictions, like Ontario, have a zero offset, which is incorrect.
            // Set the offset to the start of the subfile type indicator.
            var match = _rxSubfile.Match(rawPdf417Input);

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
    /// Get the index of the subfile starting position.
    /// </summary>
    private static int ParseSubfileOffset2(string rawPdf417Input, AAMVAVersion version, IdentificationCard2 idCard)
    {
        var ixSubfileStartPosition = 0;

        if (version == AAMVAVersion.AAMVA2000)
        {
            var aamva2000OffsetSpan = rawPdf417Input.AsSpan(start: 21, length: 4);

            if (!int.TryParse(aamva2000OffsetSpan, NumberStyles.Integer, provider: CultureInfo.InvariantCulture, out ixSubfileStartPosition))
            {
                // Throw a more useful error message than int.Parse.
                throw new ArgumentException($"[Subfile Start Index] Unable to parse the subfile start index from the value '{aamva2000OffsetSpan}'.");
            }

            // South Carolina's offset is off by one byte which causes the parsing of the IdNumber to fail
            if (idCard.IssuerIdentificationNumber.Value == IssuerIdentificationNumber.SouthCarolina && ixSubfileStartPosition == 30)
            {
                ixSubfileStartPosition--;
            }
        }
        else if (version >= AAMVAVersion.AAMVA2003)
        {
            // Change to use Span to reduce memory allocations.
            var offsetAsSpan = rawPdf417Input.AsSpan(start: 23, length: 4);
            var allOffsetCharsAreDigits = true;

            foreach (var offsetChar in offsetAsSpan)
            {
                if (char.IsDigit(offsetChar))
                {
                    continue;
                }

                allOffsetCharsAreDigits = false;
                break;
            }

            // Alberta specifies characters, like "abac", in the place of the expected offset
            // which causes the parsing of the subfile records to fail.
            if (allOffsetCharsAreDigits)
            {
                // Don't try parse, as we know the span contains all decimal digits.
                ixSubfileStartPosition = int.Parse(offsetAsSpan, provider: CultureInfo.InvariantCulture);
            }
        }

        if (ixSubfileStartPosition == 0)
        {
            // Some jurisdictions, like Ontario, have a zero offset, which is incorrect.
            // Set the offset to the start of the subfile type indicator.
            var match = _rxSubfile.Match(rawPdf417Input);

            if (match.Success)
            {
                const int subfileTypeLength = 2;
                const int firstElementIdLength = 3;

                ixSubfileStartPosition = match.Index + match.Length - subfileTypeLength - firstElementIdLength;
            }
        }

        return ixSubfileStartPosition;
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
            .Where(r => r.Length >= 3)
            .ToDictionary(r => r.Substring(startIndex: 0, length: 3), r => r.Substring(startIndex: 3).Trim());
    }

    /// <summary>
    /// Get a list of all the data records (name and value as a single string) that we need to parse.
    /// </summary>
    private static Dictionary<string, string> GetSubfileRecords2(string rawPdf417Input, AAMVAVersion version, IdentificationCard2 idCard)
    {
        var ixSubfile = ParseSubfileOffset2(rawPdf417Input, version, idCard);

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
            .Where(r => r.Length >= 3)
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
            return Country.USA;
        }

        if (subfileRecords.TryGetValue(SubfileElementIds.Country, out string? data))
        {
            if (data == "USA")
            {
                return Country.USA;
            }

            if (data == "CAN" || data == "CDN")
            {
                return Country.Canada;
            }
        }

        return IssuerMetadataHelper.GetCountry(iin);
    }

    private static IReadOnlyCollection<string> PopulateIdCard(IdentificationCard idCard, AAMVAVersion version, Country country, Dictionary<string, string> subfileRecords, ILogger? logger)
    {
        List<string> unhandledElementIds = new();

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
                var handled = Parser.ParseAndSetIdElements(elementId: elementId, data: data, country, version, idCard);

                if (!handled && idCard is DriversLicense driversLicense)
                {
                    handled = Parser.ParseAndSetDriversLicenseElements(elementId: elementId, data: data, country, version, driversLicense);
                }

                if (!handled)
                {
                    // We parse Country separately because various other fields rely on it for parsing.
                    if (elementId != SubfileElementIds.Country)
                    {
                        unhandledElementIds.Add(elementId);
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Unhandled exception in {nameof(PopulateIdCard)} while trying to parse element Id {{ElementId}}", elementId);
                throw;
            }
        }

        return unhandledElementIds;
    }

    private static void PopulateIdCard2(IdentificationCard2 idCard, AAMVAVersion version, Country country, Dictionary<string, string> subfileRecords, ILogger? logger)
    {
        foreach (var elementId in subfileRecords.Keys)
        {
            var data = subfileRecords[elementId];

            if (elementId.StartsWith("Z", StringComparison.Ordinal) && !idCard.AdditionalJurisdictionElements.ContainsKey(elementId))
            {
                idCard.AdditionalJurisdictionElements.Add(elementId, FieldHelpers.ParsedField<string?>(elementId: elementId, value: data, rawValue: data));
                continue;
            }

            try
            {
                var handled = Parser2.ParseAndSetIdElements(elementId: elementId, data: data, country, version, idCard);

                if (!handled && idCard is DriversLicense2 driversLicense)
                {
                    handled = Parser2.ParseAndSetDriversLicenseElements(elementId: elementId, data: data, country, version, driversLicense);
                }

                if (!handled)
                {
                    // We parse Country separately because various other fields rely on it for parsing.
                    if (elementId != SubfileElementIds.Country)
                    {
                        idCard.UnhandledElementIds.Add(elementId);
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Unhandled exception in {nameof(PopulateIdCard)} while trying to parse element Id {{ElementId}}", elementId);
                throw;
            }
        }
    }
}
