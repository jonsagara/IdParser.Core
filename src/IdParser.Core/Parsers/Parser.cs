using System.Diagnostics.CodeAnalysis;
using IdParser.Core.Constants;
using IdParser.Core.Parsers.Id;
using IdParser.Core.Parsers.License;

namespace IdParser.Core.Parsers;

internal static class Parser
{
    internal readonly record struct ParseAndSetElementResult
    {
        internal required bool ElementHandled { get; init; }

        internal required ElementParseError? ElementParseError { get; init; }

        [MemberNotNullWhen(true, nameof(ElementParseError))]
        internal bool HasError
            => ElementParseError is not null;
    }


    /// <summary>
    /// Try to match <paramref name="elementId"/> to a known element ID and capture its value.
    /// </summary>
    /// <param name="elementId">The three-character element ID.</param>
    /// <param name="rawValue">The element's raw string value from the scanned ID text.</param>
    /// <param name="country">The country of jurisdiction.</param>
    /// <param name="version">The AAMVA specification version from the ID.</param>
    /// <param name="idCard">The <see cref="IdentificationCard" /> instance we are trying to populate.</param>
    /// <returns>true if <paramref name="elementId"/> matched one of the known element abbreviations; false otherwise.</returns>
    internal static ParseAndSetElementResult ParseAndSetIdCardElement(string elementId, string? rawValue, Country country, AAMVAVersion version, IdentificationCard idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(idCard);

        var elementHandled = true;
        ElementParseError? elementParseError = null;

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                (idCard.AliasFirstName, elementParseError) = ParseElement(AliasFirstNameParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.AliasLastName:
                (idCard.AliasLastName, elementParseError) = ParseElement(AliasLastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AliasSuffix:
                (idCard.AliasSuffix, elementParseError) = ParseElement(AliasSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AuditInformation:
                (idCard.AuditInformation, elementParseError) = ParseElement(AuditInformationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.City:
                (idCard.City, elementParseError) = ParseElement(CityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ComplianceType:
                (idCard.ComplianceType, elementParseError) = ParseElement(ComplianceTypeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.DateOfBirth:
                (idCard.DateOfBirth, elementParseError) = ParseElement(DateOfBirthParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                (idCard.DocumentDiscriminator, elementParseError) = ParseElement(DocumentDiscriminatorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Ethnicity:
                (idCard.Ethnicity, elementParseError) = ParseElement(EthnicityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ExpirationDate:
                (idCard.ExpirationDate, elementParseError) = ParseElement(ExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.EyeColor:
                (idCard.EyeColor, elementParseError) = ParseElement(EyeColorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.FirstName:
                // NOTE: this parser doesn't report errors
                var firstNameParts = FirstNameParser.Parse(rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.GivenName:
                // NOTE: this parser doesn't report errors
                var givenNameParts = GivenNameParser.Parse(rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.HairColor:
                (idCard.HairColor, elementParseError) = ParseElement(HairColorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                (idCard.HasTemporaryLawfulStatus, elementParseError) = ParseElement(HasTemporaryLawfulStatusParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Height:
                (idCard.Height, elementParseError) = ParseElement(HeightParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IdNumber:
                (idCard.IdNumber, elementParseError) = ParseElement(IdNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.InventoryControlNumber:
                (idCard.InventoryControlNumber, elementParseError) = ParseElement(InventoryControlNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonor:
                (idCard.IsOrganDonor, elementParseError) = ParseElement(IsOrganDonorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                (idCard.IsOrganDonor, elementParseError) = ParseElement(IsOrganDonorLegacyParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IssueDate:
                (idCard.IssueDate, elementParseError) = ParseElement(IssueDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                (idCard.IsVeteran, elementParseError) = ParseElement(IsVeteranParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.JurisdictionCode:
                (idCard.JurisdictionCode, elementParseError) = ParseElement(JurisdictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.LastName:
                (idCard.LastName, elementParseError) = ParseElement(LastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.MiddleName:
                // Some jurisdictions like Wyoming put the middle initial in the FirstName field. If we have
                //   already written that, and if middle name is null, keep the one parsed from first name.
                (idCard.MiddleName, elementParseError) = ParseElement(MiddleNameParser.Parse, elementId: elementId, rawValue: rawValue ?? idCard.MiddleName.Value);
                break;

            case SubfileElementIds.Name:
                // NOTE: this parser doesn't report errors
                var nameParts = NameParser.Parse(elementId: elementId, rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.First, rawValue: rawValue);
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Middle, rawValue: rawValue);
                idCard.LastName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Last, rawValue: rawValue);
                idCard.Suffix = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Suffix, rawValue: rawValue);
                break;

            case SubfileElementIds.NameSuffix:
                (idCard.Suffix, elementParseError) = ParseElement(NameSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PlaceOfBirth:
                (idCard.PlaceOfBirth, elementParseError) = ParseElement(PlaceOfBirthParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PostalCode:
                (idCard.PostalCode, elementParseError) = ParseElement(PostalCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RevisionDate:
                (idCard.RevisionDate, elementParseError) = ParseElement(RevisionDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Sex:
                (idCard.Sex, elementParseError) = ParseElement(SexParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1:
                (idCard.StreetLine1, elementParseError) = ParseElement(StreetLine1Parser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                (idCard.StreetLine1, elementParseError) = ParseElement(StreetLine1LegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine2:
                (idCard.StreetLine2, elementParseError) = ParseElement(StreetLine2Parser.Parse, elementId: elementId, rawValue: rawValue, city: idCard.City.Value, jurisdictionCode: idCard.JurisdictionCode.Value, postalCode: idCard.PostalCode.Value);
                break;

            case SubfileElementIds.Under18Until:
                (idCard.Under18Until, elementParseError) = ParseElement(Under18UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under19Until:
                (idCard.Under19Until, elementParseError) = ParseElement(Under19UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under21Until:
                (idCard.Under21Until, elementParseError) = ParseElement(Under21UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                (idCard.WasFirstNameTruncated, elementParseError) = ParseElement(WasFirstNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                (idCard.WasLastNameTruncated, elementParseError) = ParseElement(WasLastNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                (idCard.WasMiddleNameTruncated, elementParseError) = ParseElement(WasMiddleNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInKilograms:
                (idCard.Weight, elementParseError) = ParseElement(WeightInKilogramsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInPounds:
                (idCard.Weight, elementParseError) = ParseElement(WeightInPoundsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightRange:
                (idCard.WeightRange, elementParseError) = ParseElement(WeightRangeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return new ParseAndSetElementResult
        {
            ElementHandled = elementHandled,
            ElementParseError = elementParseError,
        };
    }

    /// <summary>
    /// Try to match <paramref name="elementId"/> to a known element ID and capture its value.
    /// </summary>
    /// <param name="elementId">The three-character element ID.</param>
    /// <param name="rawValue">The element's raw string value from the scanned ID text.</param>
    /// <param name="country">The country of jurisdiction.</param>
    /// <param name="version">The AAMVA specification version from the ID.</param>
    /// <param name="driversLicense">The <see cref="DriversLicense"/> instance we are trying to populate.</param>
    /// <returns>true if <paramref name="elementId"/> matched one of the known element abbreviations; false otherwise.</returns>
    internal static ParseAndSetElementResult ParseAndSetDriversLicenseElement(string elementId, string? rawValue, Country country, AAMVAVersion version, DriversLicense driversLicense)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(driversLicense);

        var elementHandled = true;
        ElementParseError? elementParseError = null;

        switch (elementId)
        {
            case SubfileElementIds.EndorsementCodeDescription:
                (driversLicense.EndorsementCodeDescription, elementParseError) = ParseElement(EndorsementCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodes:
                (driversLicense.EndorsementCodes, elementParseError) = ParseElement(EndorsementCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                (driversLicense.EndorsementCodes, elementParseError) = ParseElement(EndorsementCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                (driversLicense.HazmatEndorsementExpirationDate, elementParseError) = ParseElement(HazmatEndorsementExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                (driversLicense.RestrictionCodeDescription, elementParseError) = ParseElement(RestrictionCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodes:
                (driversLicense.RestrictionCodes, elementParseError) = ParseElement(RestrictionCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                (driversLicense.RestrictionCodes, elementParseError) = ParseElement(RestrictionCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                (driversLicense.StandardEndorsementCode, elementParseError) = ParseElement(StandardEndorsementCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                (driversLicense.StandardRestrictionCode, elementParseError) = ParseElement(StandardRestrictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                (driversLicense.StandardVehicleClassification, elementParseError) = ParseElement(StandardVehicleClassificationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                (driversLicense.VehicleClassificationDescription, elementParseError) = ParseElement(VehicleClassificationDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClass:
                (driversLicense.VehicleClass, elementParseError) = ParseElement(VehicleClassParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                (driversLicense.VehicleClass, elementParseError) = ParseElement(VehicleClassLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return new ParseAndSetElementResult
        {
            ElementHandled = elementHandled,
            ElementParseError = elementParseError,
        };
    }


    //
    // Private methods
    //

    private readonly record struct InternalParseResult<T>(Field<T> Field, ElementParseError? ElementError);

    private delegate Field<T> ParseRawValue<T>(string elementId, string? rawValue);

    private delegate Field<T> ParseRawValueWithVersion<T>(string elementId, string? rawValue, AAMVAVersion version);

    private delegate Field<T> ParseRawValueWithCountryAndVersion<T>(string elementId, string? rawValue, Country country, AAMVAVersion version);

    private delegate Field<T> ParseStreetAddressFunc<T>(string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode);


    private static InternalParseResult<T> ParseElement<T>(ParseRawValue<T> parseFunc, string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

    private static InternalParseResult<T> ParseElement<T>(ParseRawValueWithVersion<T> parseFunc, string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, version);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

    private static InternalParseResult<T> ParseElement<T>(ParseRawValueWithCountryAndVersion<T> parseFunc, string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, country, version);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

    private static InternalParseResult<T> ParseElement<T>(ParseStreetAddressFunc<T> parseFunc, string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, city: city, jurisdictionCode: jurisdictionCode, postalCode: postalCode);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }
}
