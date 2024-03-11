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
                (idCard.AliasFirstName, elementParseError) = InternalParse(AliasFirstNameParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.AliasLastName:
                (idCard.AliasLastName, elementParseError) = InternalParse(AliasLastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AliasSuffix:
                (idCard.AliasSuffix, elementParseError) = InternalParse(AliasSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AuditInformation:
                (idCard.AuditInformation, elementParseError) = InternalParse(AuditInformationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.City:
                (idCard.City, elementParseError) = InternalParse(CityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ComplianceType:
                (idCard.ComplianceType, elementParseError) = InternalParse(ComplianceTypeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.DateOfBirth:
                (idCard.DateOfBirth, elementParseError) = InternalParse(DateOfBirthParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                (idCard.DocumentDiscriminator, elementParseError) = InternalParse(DocumentDiscriminatorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Ethnicity:
                (idCard.Ethnicity, elementParseError) = InternalParse(EthnicityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ExpirationDate:
                (idCard.ExpirationDate, elementParseError) = InternalParse(ExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.EyeColor:
                (idCard.EyeColor, elementParseError) = InternalParse(EyeColorParser.Parse, elementId: elementId, rawValue: rawValue);
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
                (idCard.HairColor, elementParseError) = InternalParse(HairColorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                (idCard.HasTemporaryLawfulStatus, elementParseError) = InternalParse(HasTemporaryLawfulStatusParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Height:
                (idCard.Height, elementParseError) = InternalParse(HeightParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IdNumber:
                (idCard.IdNumber, elementParseError) = InternalParse(IdNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.InventoryControlNumber:
                (idCard.InventoryControlNumber, elementParseError) = InternalParse(InventoryControlNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonor:
                (idCard.IsOrganDonor, elementParseError) = InternalParse(IsOrganDonorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                (idCard.IsOrganDonor, elementParseError) = InternalParse(IsOrganDonorLegacyParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IssueDate:
                (idCard.IssueDate, elementParseError) = InternalParse(IssueDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                (idCard.IsVeteran, elementParseError) = InternalParse(IsVeteranParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.JurisdictionCode:
                (idCard.JurisdictionCode, elementParseError) = InternalParse(JurisdictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.LastName:
                (idCard.LastName, elementParseError) = InternalParse(LastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.MiddleName:
                // Some jurisdictions like Wyoming put the middle initial in the FirstName field. If we have
                //   already written that, and if middle name is null, keep the one parsed from first name.
                (idCard.MiddleName, elementParseError) = InternalParse(MiddleNameParser.Parse, elementId: elementId, rawValue: rawValue ?? idCard.MiddleName.Value);
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
                (idCard.Suffix, elementParseError) = InternalParse(NameSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PlaceOfBirth:
                (idCard.PlaceOfBirth, elementParseError) = InternalParse(PlaceOfBirthParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PostalCode:
                (idCard.PostalCode, elementParseError) = InternalParse(PostalCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RevisionDate:
                (idCard.RevisionDate, elementParseError) = InternalParse(RevisionDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Sex:
                (idCard.Sex, elementParseError) = InternalParse(SexParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1:
                (idCard.StreetLine1, elementParseError) = InternalParse(StreetLine1Parser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                (idCard.StreetLine1, elementParseError) = InternalParse(StreetLine1LegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine2:
                (idCard.StreetLine2, elementParseError) = InternalParse(StreetLine2Parser.Parse, elementId: elementId, rawValue: rawValue, city: idCard.City.Value, jurisdictionCode: idCard.JurisdictionCode.Value, postalCode: idCard.PostalCode.Value);
                break;

            case SubfileElementIds.Under18Until:
                (idCard.Under18Until, elementParseError) = InternalParse(Under18UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under19Until:
                (idCard.Under19Until, elementParseError) = InternalParse(Under19UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under21Until:
                (idCard.Under21Until, elementParseError) = InternalParse(Under21UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                (idCard.WasFirstNameTruncated, elementParseError) = InternalParse(WasFirstNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                (idCard.WasLastNameTruncated, elementParseError) = InternalParse(WasLastNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                (idCard.WasMiddleNameTruncated, elementParseError) = InternalParse(WasMiddleNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInKilograms:
                (idCard.Weight, elementParseError) = InternalParse(WeightInKilogramsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInPounds:
                (idCard.Weight, elementParseError) = InternalParse(WeightInPoundsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightRange:
                (idCard.WeightRange, elementParseError) = InternalParse(WeightRangeParser.Parse, elementId: elementId, rawValue: rawValue);
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
                (driversLicense.EndorsementCodeDescription, elementParseError) = InternalParse(EndorsementCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodes:
                (driversLicense.EndorsementCodes, elementParseError) = InternalParse(EndorsementCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                (driversLicense.EndorsementCodes, elementParseError) = InternalParse(EndorsementCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                (driversLicense.HazmatEndorsementExpirationDate, elementParseError) = InternalParse(HazmatEndorsementExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                (driversLicense.RestrictionCodeDescription, elementParseError) = InternalParse(RestrictionCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodes:
                (driversLicense.RestrictionCodes, elementParseError) = InternalParse(RestrictionCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                (driversLicense.RestrictionCodes, elementParseError) = InternalParse(RestrictionCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                (driversLicense.StandardEndorsementCode, elementParseError) = InternalParse(StandardEndorsementCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                (driversLicense.StandardRestrictionCode, elementParseError) = InternalParse(StandardRestrictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                (driversLicense.StandardVehicleClassification, elementParseError) = InternalParse(StandardVehicleClassificationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                (driversLicense.VehicleClassificationDescription, elementParseError) = InternalParse(VehicleClassificationDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClass:
                (driversLicense.VehicleClass, elementParseError) = InternalParse(VehicleClassParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                (driversLicense.VehicleClass, elementParseError) = InternalParse(VehicleClassLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
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

#warning TODO: use a delegate so that we have named parameters to parseFun
    private static InternalParseResult<T> InternalParse<T>(Func<string, string?, Field<T>> parseFunc, string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId, rawValue);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

#warning TODO: use a delegate so that we have named parameters to parseFun
    private static InternalParseResult<T> InternalParse<T>(Func<string, string?, AAMVAVersion, Field<T>> parseFunc, string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId, rawValue, version);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

#warning TODO: use a delegate so that we have named parameters to parseFun
    private static InternalParseResult<T> InternalParse<T>(Func<string, string?, Country, AAMVAVersion, Field<T>> parseFunc, string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId, rawValue, country, version);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }

#warning TODO: use a delegate so that we have named parameters to parseFun
    private static InternalParseResult<T> InternalParse<T>(Func<string, string?, string?, string?, string?, Field<T>> parseFunc, string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId, rawValue, city, jurisdictionCode, postalCode);

        var parseError = field.HasError
            ? new ElementParseError(elementId, rawValue, field.Error!)
            : null;

        return new InternalParseResult<T>(field, parseError);
    }
}
