﻿using System.Diagnostics.CodeAnalysis;
using IdParser.Core.Constants;
using IdParser.Core.Parsers.Id;
using IdParser.Core.Parsers.License;

namespace IdParser.Core.Parsers;

internal static class Parser
{
    internal readonly record struct ParseAndSetElementResult
    {
        internal required bool ElementHandled { get; init; }

        internal required ParseError? Error { get; init; }

        [MemberNotNullWhen(true, nameof(Error))]
        internal bool HasError
            => Error is not null;
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
        ParseError? error = null;

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                (idCard.AliasFirstName, error) = ParseElement(AliasFirstNameParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.AliasLastName:
                (idCard.AliasLastName, error) = ParseElement(AliasLastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AliasSuffix:
                (idCard.AliasSuffix, error) = ParseElement(AliasSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AuditInformation:
                (idCard.AuditInformation, error) = ParseElement(AuditInformationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.City:
                (idCard.City, error) = ParseElement(CityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ComplianceType:
                (idCard.ComplianceType, error) = ParseElement(ComplianceTypeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.DateOfBirth:
                (idCard.DateOfBirth, error) = ParseElement(DateOfBirthParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                (idCard.DocumentDiscriminator, error) = ParseElement(DocumentDiscriminatorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Ethnicity:
                (idCard.Ethnicity, error) = ParseElement(EthnicityParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ExpirationDate:
                (idCard.ExpirationDate, error) = ParseElement(ExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.EyeColor:
                (idCard.EyeColor, error) = ParseElement(EyeColorParser.Parse, elementId: elementId, rawValue: rawValue);
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
                (idCard.HairColor, error) = ParseElement(HairColorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                (idCard.HasTemporaryLawfulStatus, error) = ParseElement(HasTemporaryLawfulStatusParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Height:
                (idCard.Height, error) = ParseElement(HeightParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IdNumber:
                (idCard.IdNumber, error) = ParseElement(IdNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.InventoryControlNumber:
                (idCard.InventoryControlNumber, error) = ParseElement(InventoryControlNumberParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonor:
                (idCard.IsOrganDonor, error) = ParseElement(IsOrganDonorParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                (idCard.IsOrganDonor, error) = ParseElement(IsOrganDonorLegacyParser.Parse, elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IssueDate:
                (idCard.IssueDate, error) = ParseElement(IssueDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                (idCard.IsVeteran, error) = ParseElement(IsVeteranParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.JurisdictionCode:
                (idCard.JurisdictionCode, error) = ParseElement(JurisdictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.LastName:
                (idCard.LastName, error) = ParseElement(LastNameParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.MiddleName:
                // Some jurisdictions like Wyoming put the middle initial in the FirstName field. If we have
                //   already written that, and if middle name is null, keep the one parsed from first name.
                (idCard.MiddleName, error) = ParseElement(MiddleNameParser.Parse, elementId: elementId, rawValue: rawValue ?? idCard.MiddleName.Value);
                break;

            case SubfileElementIds.Name:
                // Name parser for legacy 2000 spec. The field code doesn't appear in the 2020 spec.
                // NOTE: this parser doesn't report errors
                var nameParts = NameParser.Parse(elementId: elementId, rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.First, rawValue: rawValue);
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Middle, rawValue: rawValue);
                idCard.LastName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Last, rawValue: rawValue);
                idCard.Suffix = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Suffix, rawValue: rawValue);
                break;

            case SubfileElementIds.NameSuffix:
                (idCard.Suffix, error) = ParseElement(NameSuffixParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PlaceOfBirth:
                (idCard.PlaceOfBirth, error) = ParseElement(PlaceOfBirthParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PostalCode:
                (idCard.PostalCode, error) = ParseElement(PostalCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RevisionDate:
                (idCard.RevisionDate, error) = ParseElement(RevisionDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Sex:
                (idCard.Sex, error) = ParseElement(SexParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1:
                (idCard.StreetLine1, error) = ParseElement(StreetLine1Parser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                (idCard.StreetLine1, error) = ParseElement(StreetLine1LegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine2:
                (idCard.StreetLine2, error) = ParseElement(StreetLine2Parser.Parse, elementId: elementId, rawValue: rawValue, city: idCard.City.Value, jurisdictionCode: idCard.JurisdictionCode.Value, postalCode: idCard.PostalCode.Value);
                break;

            case SubfileElementIds.Under18Until:
                (idCard.Under18Until, error) = ParseElement(Under18UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under19Until:
                (idCard.Under19Until, error) = ParseElement(Under19UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under21Until:
                (idCard.Under21Until, error) = ParseElement(Under21UntilParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                (idCard.WasFirstNameTruncated, error) = ParseElement(WasFirstNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                (idCard.WasLastNameTruncated, error) = ParseElement(WasLastNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                (idCard.WasMiddleNameTruncated, error) = ParseElement(WasMiddleNameTruncatedParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInKilograms:
                (idCard.Weight, error) = ParseElement(WeightInKilogramsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInPounds:
                (idCard.Weight, error) = ParseElement(WeightInPoundsParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightRange:
                (idCard.WeightRange, error) = ParseElement(WeightRangeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return new ParseAndSetElementResult
        {
            ElementHandled = elementHandled,
            Error = error,
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
        ParseError? error = null;

        switch (elementId)
        {
            case SubfileElementIds.EndorsementCodeDescription:
                (driversLicense.EndorsementCodeDescription, error) = ParseElement(EndorsementCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodes:
                (driversLicense.EndorsementCodes, error) = ParseElement(EndorsementCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                (driversLicense.EndorsementCodes, error) = ParseElement(EndorsementCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                (driversLicense.HazmatEndorsementExpirationDate, error) = ParseElement(HazmatEndorsementExpirationDateParser.Parse, elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                (driversLicense.RestrictionCodeDescription, error) = ParseElement(RestrictionCodeDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodes:
                (driversLicense.RestrictionCodes, error) = ParseElement(RestrictionCodesParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                (driversLicense.RestrictionCodes, error) = ParseElement(RestrictionCodesLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                (driversLicense.StandardEndorsementCode, error) = ParseElement(StandardEndorsementCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                (driversLicense.StandardRestrictionCode, error) = ParseElement(StandardRestrictionCodeParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                (driversLicense.StandardVehicleClassification, error) = ParseElement(StandardVehicleClassificationParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                (driversLicense.VehicleClassificationDescription, error) = ParseElement(VehicleClassificationDescriptionParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClass:
                (driversLicense.VehicleClass, error) = ParseElement(VehicleClassParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                (driversLicense.VehicleClass, error) = ParseElement(VehicleClassLegacyParser.Parse, elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return new ParseAndSetElementResult
        {
            ElementHandled = elementHandled,
            Error = error,
        };
    }


    //
    // Private methods
    //

    private readonly record struct ParseElementResult<T>(Field<T> Field, ParseError? Error);

    private delegate Field<T> ParseFunction<T>(string elementId, string? rawValue);

    private delegate Field<T> ParseWithVersionFunction<T>(string elementId, string? rawValue, AAMVAVersion version);

    private delegate Field<T> ParseWithCountryAndVersionFunction<T>(string elementId, string? rawValue, Country country, AAMVAVersion version);

    private delegate Field<T> ParseStreetAddressFunction<T>(string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode);


    private static ParseElementResult<T> ParseElement<T>(ParseFunction<T> parseFunc, string elementId, string? rawValue)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue);

        var parseError = field.HasError
            ? new ParseError(Message: field.Error!, ElementId: elementId, RawValue: rawValue)
            : null;

        return new ParseElementResult<T>(field, parseError);
    }

    private static ParseElementResult<T> ParseElement<T>(ParseWithVersionFunction<T> parseFunc, string elementId, string? rawValue, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, version);

        var parseError = field.HasError
            ? new ParseError(Message: field.Error!, ElementId: elementId, RawValue: rawValue)
            : null;

        return new ParseElementResult<T>(field, parseError);
    }

    private static ParseElementResult<T> ParseElement<T>(ParseWithCountryAndVersionFunction<T> parseFunc, string elementId, string? rawValue, Country country, AAMVAVersion version)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, country, version);

        var parseError = field.HasError
            ? new ParseError(Message: field.Error!, ElementId: elementId, RawValue: rawValue)
            : null;

        return new ParseElementResult<T>(field, parseError);
    }

    private static ParseElementResult<T> ParseElement<T>(ParseStreetAddressFunction<T> parseFunc, string elementId, string? rawValue, string? city, string? jurisdictionCode, string? postalCode)
    {
        ArgumentNullException.ThrowIfNull(parseFunc);

        var field = parseFunc(elementId: elementId, rawValue: rawValue, city: city, jurisdictionCode: jurisdictionCode, postalCode: postalCode);

        var parseError = field.HasError
            ? new ParseError(Message: field.Error!, ElementId: elementId, RawValue: rawValue)
            : null;

        return new ParseElementResult<T>(field, parseError);
    }
}
