using System.Globalization;
using System.Text.RegularExpressions;
using IdParser.Core.Constants;
using IdParser.Core.Logging;
using IdParser.Core.Parsers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace IdParser.Core;

/// <summary>
/// Any element with an unrecognized 3-character element ID and its associated value.
/// </summary>
/// <param name="ElementId">The 3-character element ID.</param>
/// <param name="RawValue">The raw value from the scanned ID text.</param>
public record UnhandledElement(string ElementId, string? RawValue);

/// <summary>
/// Details about failure to parse or extract a meaningful value from an element's raw value obtained from 
/// the scanned ID text.
/// </summary>
/// <param name="ElementId">The 3-character element ID.</param>
/// <param name="RawValue">The element's raw value from the scanned ID text.</param>
/// <param name="Error">A message describing the error that occurred.</param>
public record ElementParseError(string ElementId, string? RawValue, string Error);

/// <summary>
/// Contains the result of parsing a scanned ID: the ID card, a collection of unhandled fields, and any
/// field-level parsing errors that occurred.
/// </summary>
public class BarcodeParseResult
{
    /// <summary>
    /// Contains values of any elements extracted from the scanned ID text.
    /// </summary>
    public IdentificationCard Card { get; }

    public IReadOnlyCollection<UnhandledElement> UnhandledElements { get; }

    /// <summary>
    /// Contains element-level errors that occurred while trying to extract a meaningful value from the element's raw value.
    /// </summary>
    public IReadOnlyCollection<ElementParseError> ElementParseErrors { get; }


    public BarcodeParseResult(IdentificationCard card, IReadOnlyCollection<UnhandledElement> unhandledElements, IReadOnlyCollection<ElementParseError> elementParseErrors)
    {
        ArgumentNullException.ThrowIfNull(card);
        ArgumentNullException.ThrowIfNull(unhandledElements);
        ArgumentNullException.ThrowIfNull(elementParseErrors);

        Card = card;
        UnhandledElements = unhandledElements;
        ElementParseErrors = elementParseErrors;
    }
}

public static partial class Barcode
{
    /// <summary>
    /// The text should begin with an '@' character (ASCII Decimal 64, Hex 0x40).
    /// </summary>
    internal const char ExpectedComplianceIndicator = (char)64;

    /// <summary>
    /// The second character should be a linefeed character '\n' (ASCII Decimal 10, Hex 0x0A).
    /// </summary>
    internal const char ExpectedDataElementSeparator = (char)10;

    /// <summary>
    /// The third character should be a Record Separator character '\u0030' (ASCII Decimal 30, Hex 0x1E).
    /// </summary>
    internal const char ExpectedRecordSeparator = (char)30;

    /// <summary>
    /// The fourth character should be a carriage return character '\r' (ASCII Decimal 13, Hex 0x0D).
    /// </summary>
    internal const char ExpectedSegmentTerminator = (char)13;

    /// <summary>
    /// The expected file type should be ANSI, followed by a space: &quot;ANSI &quot;
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

        loggerFactory ??= NullLoggerFactory.Instance;
        ILogger logger = loggerFactory.CreateLogger(typeof(Barcode));

        if (validationLevel == Validation.Strict)
        {
            ValidateHeaderFormat(rawPdf417Input);
        }
        else
        {
            rawPdf417Input = Fixes.TryToCorrectHeader(rawPdf417Input, loggerFactory);
        }

        var aamvaVersionResult = ParseAAMVAVersion(rawPdf417Input);
        var idCard = GetIdCardInstance(rawPdf417Input, aamvaVersionResult);

#warning TODO: Need to bail here if we couldn't parse the IssuerIdentificationNumber, and we can't continue trying to parse the rest of the ID.

        // NOTE: any elementIds without a value will have null as the value, NOT "".
        var subfileRecords = GetSubfileRecords(rawPdf417Input, idCard.AAMVAVersionNumber.Value, idCard);

        // We have to parse and retrieve Country from the subfile first because other fields depends on its value.
        var countryResult = ParseCountry(idCard.IssuerIdentificationNumber.Value, idCard.AAMVAVersionNumber.Value, subfileRecords);
        idCard.Country = FieldHelpers.ParsedField(elementId: SubfileElementIds.Country, value: countryResult.Country, rawValue: countryResult.RawValue);

        var populateResult = PopulateIdCard(idCard, idCard.AAMVAVersionNumber.Value, countryResult.Country, subfileRecords, logger);
        if (populateResult.UnhandledElements.Count > 0)
        {
            logger.UnhandledElementIds(string.Join(", ", populateResult.UnhandledElements.Select(ue => ue.ElementId)));
        }

        return new BarcodeParseResult(idCard, populateResult.UnhandledElements, populateResult.ElementErrors);
    }


    //
    // Private methods
    //

    /// <summary>
    /// Get the <see cref="ExpectedComplianceIndicator"/> '@' from the scanned text.
    /// </summary>
    private static char ParseComplianceIndicator(string input)
        => input.AsSpan(start: 0, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedDataElementSeparator"/> '\n' from the scanned text.
    /// </summary>
    private static char ParseDataElementSeparator(string input)
        => input.AsSpan(start: 1, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedRecordSeparator"/> '\u0030' from the scanned text.
    /// </summary>
    private static char ParseRecordSeparator(string input)
        => input.AsSpan(start: 2, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedSegmentTerminator"/> '\r' from the scanned text.
    /// </summary>
    private static char ParseSegmentTerminator(string input)
        => input.AsSpan(start: 3, length: 1)[0];

    /// <summary>
    /// Get the <see cref="ExpectedFileType"/> (e.g., &quot;ANSI &quot;) from the scanned text.
    /// </summary>
    private static string ParseFileType(string input)
        => input.Substring(startIndex: 4, length: 5);

    /// <summary>
    /// Ensure the header has the required and expected characters: &quot;@\n\u0030\rANSI &quot;.
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
    private static IdentificationCard GetIdCardInstance(string rawPdf417Input, AAMVAVersionResult aamvaVersionResult)
    {
        // Create either a Drivers License or an Identification Card, based on the contents of the scanned text.
        var idCard = ParseSubfileType(rawPdf417Input, aamvaVersionResult.Version) == "DL"
            ? new DriversLicense()
            : new IdentificationCard();


        //
        // Parse the Issuer Identification Number
        //

        var issuerIdNumberRawValue = rawPdf417Input.Substring(9, 6);

        if (Enum.TryParse<IssuerIdentificationNumber>(issuerIdNumberRawValue.AsSpan(), out var issuerIdentificationNumber) && Enum.IsDefined(issuerIdentificationNumber))
        {
            idCard.IssuerIdentificationNumber = FieldHelpers.ParsedField(elementId: null, value: issuerIdentificationNumber, rawValue: issuerIdNumberRawValue);
        }
        else
        {
            idCard.IssuerIdentificationNumber = FieldHelpers.UnparsedField<IssuerIdentificationNumber>(elementId: null, rawValue: issuerIdNumberRawValue, error: $"Unable to parse Issuer Identification Number from value '{issuerIdNumberRawValue}'.");
        }


        //
        // Set the previously parsed AAMVA Version Number
        //

        idCard.AAMVAVersionNumber = FieldHelpers.ParsedField(elementId: null, value: aamvaVersionResult.Version, rawValue: aamvaVersionResult.RawValue);


        //
        // Parse the Jurisdiction Version Number
        //

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
                : FieldHelpers.UnparsedField<int>(elementId: null, rawValue: jurisdictionVersionNumberRawValue, error: $"Unable to parse Jurisdiction Version Number from value '{jurisdictionVersionNumberRawValue}'.");
        }

        return idCard;
    }


    [GeneratedRegex(@"(DL|ID)([\d\w]{3,8})(DL|ID|Z\w)([DZ][A-Z]{2})")]
    private static partial Regex SubfileRegex();

    /// <summary>
    /// Get the index of the subfile starting position.
    /// </summary>
    private static int ParseSubfileOffset(string rawPdf417Input, AAMVAVersion version, IdentificationCard idCard)
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
                // Parse is fine because we know the span contains all decimal digits.
                ixSubfileStartPosition = int.Parse(offsetAsSpan, provider: CultureInfo.InvariantCulture);
            }
        }

        if (ixSubfileStartPosition == 0)
        {
            // Some jurisdictions, like Ontario, have a zero offset, which is incorrect.
            // Set the offset to the start of the subfile type indicator.
            var match = SubfileRegex().Match(rawPdf417Input);

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
    private static Dictionary<string, string?> GetSubfileRecords(string rawPdf417Input, AAMVAVersion version, IdentificationCard idCard)
    {
        var ixSubfileStart = ParseSubfileOffset(rawPdf417Input, version, idCard);

        var records = rawPdf417Input
            .Substring(startIndex: ixSubfileStart)
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
        // The remaining characters are the value. If there are none, return null.
        return records
            .Where(r => r.Length >= 3)
            .ToDictionary(r => r.Substring(startIndex: 0, length: 3), r => r.Substring(startIndex: 3).Trim().ToNullIfWhiteSpace());
    }


    private readonly record struct ParseCountryResult(Country Country, string? RawValue);

    /// <summary>
    /// Parses the country based on the DCG subfile record.
    /// Gets the country from the IIN if no matching subfile record was found.
    /// </summary>
    private static ParseCountryResult ParseCountry(IssuerIdentificationNumber iin, AAMVAVersion version, Dictionary<string, string?> subfileRecords)
    {
        // Country is not a subfile record in the AAMVA 2000 standard.
        if (version == AAMVAVersion.AAMVA2000)
        {
            return new ParseCountryResult(Country.USA, null);
        }

        if (subfileRecords.TryGetValue(SubfileElementIds.Country, out string? data))
        {
            if (data == "USA")
            {
                return new ParseCountryResult(Country.USA, data);
            }

            if (data == "CAN" || data == "CDN")
            {
                return new ParseCountryResult(Country.Canada, data);
            }
        }

        return new ParseCountryResult(IssuerMetadataHelper.GetCountry(iin), null);
    }


    private readonly record struct PopulateResult(List<UnhandledElement> UnhandledElements, List<ElementParseError> ElementErrors);

    private static PopulateResult PopulateIdCard(IdentificationCard idCard, AAMVAVersion version, Country country, Dictionary<string, string?> subfileRecords, ILogger logger)
    {
        List<UnhandledElement> unhandledElements = new();
        List<ElementParseError> elementErrors = new();

        foreach (var elementId in subfileRecords.Keys)
        {
            var rawValue = subfileRecords[elementId];

            if (elementId.StartsWith('Z') && !idCard.AdditionalJurisdictionElements.ContainsKey(elementId))
            {
                idCard.AdditionalJurisdictionElements.Add(elementId, FieldHelpers.ParsedField(elementId: elementId, value: rawValue, rawValue: rawValue));
                continue;
            }

            try
            {
                var parseAndSetResult = Parser.ParseAndSetIdCardElement(elementId: elementId, rawValue: rawValue, country, version, idCard);
                if (parseAndSetResult.ElementHandled)
                {
                    AddErrorIfParseAndSetFailed(parseAndSetResult, elementErrors);

                    // Element handled. No need for further processing.
                    continue;
                }

                if (idCard is DriversLicense driversLicense)
                {
                    parseAndSetResult = Parser.ParseAndSetDriversLicenseElement(elementId: elementId, rawValue: rawValue, country, version, driversLicense);
                    if (parseAndSetResult.ElementHandled)
                    {
                        AddErrorIfParseAndSetFailed(parseAndSetResult, elementErrors);

                        // Element handled. No need for further processing.
                        continue;
                    }
                }

                // We parse Country separately because various other fields rely on it for parsing.
                if (elementId != SubfileElementIds.Country)
                {
                    unhandledElements.Add(new UnhandledElement(ElementId: elementId, RawValue: rawValue));
                }
            }
            catch (Exception ex)
            {
                logger.PopulateIdCardUnhandledException(ex, methodName: nameof(PopulateIdCard), elementId: elementId);
                throw;
            }
        }

        return new PopulateResult(unhandledElements, elementErrors);
    }

    private static void AddErrorIfParseAndSetFailed(Parser.ParseAndSetElementResult parseAndSetResult, List<ElementParseError> elementErrors)
    {
        if (!parseAndSetResult.HasError)
        {
            return;
        }

        elementErrors.Add(parseAndSetResult.ElementParseError);
    }
}
