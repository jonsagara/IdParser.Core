using IdParser.Core.Constants;
using IdParser.Core.Parsers.Id;
using IdParser.Core.Parsers.License;

namespace IdParser.Core.Parsers;

internal static class Parser
{
    /// <summary>
    /// Try to match <paramref name="elementId"/> to a known element ID and capture its value.
    /// </summary>
    /// <param name="elementId">The three-character element ID.</param>
    /// <param name="rawValue">The element's raw string value from the scanned ID text.</param>
    /// <param name="country">The country of jurisdiction.</param>
    /// <param name="version">The AAMVA specification version from the ID.</param>
    /// <param name="idCard">The <see cref="IdentificationCard" /> instance we are trying to populate.</param>
    /// <returns>true if <paramref name="elementId"/> matched one of the known element abbreviations; false otherwise.</returns>
    internal static bool ParseAndSetIdCardElement(string elementId, string? rawValue, Country country, AAMVAVersion version, IdentificationCard idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(idCard);

        var elementHandled = true;

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                idCard.AliasFirstName = AliasFirstNameParser.Parse(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.AliasLastName:
                idCard.AliasLastName = AliasLastNameParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AliasSuffix:
                idCard.AliasSuffix = AliasSuffixParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AuditInformation:
                idCard.AuditInformation = AuditInformationParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.City:
                idCard.City = CityParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ComplianceType:
                idCard.ComplianceType = ComplianceTypeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.DateOfBirth:
                idCard.DateOfBirth = DateOfBirthParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                idCard.DocumentDiscriminator = DocumentDiscriminatorParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Ethnicity:
                idCard.Ethnicity = EthnicityParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ExpirationDate:
                idCard.ExpirationDate = ExpirationDateParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.EyeColor:
                idCard.EyeColor = EyeColorParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.FirstName:
                var firstNameParts = FirstNameParser.Parse(rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.GivenName:
                var givenNameParts = GivenNameParser.Parse(rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.HairColor:
                idCard.HairColor = HairColorParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                idCard.HasTemporaryLawfulStatus = HasTemporaryLawfulStatusParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Height:
                idCard.Height = HeightParser.Parse(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IdNumber:
                idCard.IdNumber = IdNumberParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.InventoryControlNumber:
                idCard.InventoryControlNumber = InventoryControlNumberParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonor:
                idCard.IsOrganDonor = IsOrganDonorParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                idCard.IsOrganDonor = IsOrganDonorLegacyParser.Parse(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IssueDate:
                idCard.IssueDate = IssueDateParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                idCard.IsVeteran = IsVeteranParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.JurisdictionCode:
                idCard.JurisdictionCode = JurisdictionCodeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.LastName:
                idCard.LastName = LastNameParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.MiddleName:
                // Some jurisdictions like Wyoming put the middle initial in the FirstName field. If we have
                //   already written that, and if middle name is null, keep the one parsed from first name.
                idCard.MiddleName = MiddleNameParser.Parse(elementId: elementId, rawValue: rawValue ?? idCard.MiddleName.Value);
                break;

            case SubfileElementIds.Name:
                var nameParts = NameParser.Parse(elementId: elementId, rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.First, rawValue: rawValue);
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Middle, rawValue: rawValue);
                idCard.LastName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Last, rawValue: rawValue);
                idCard.Suffix = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Suffix, rawValue: rawValue);
                break;

            case SubfileElementIds.NameSuffix:
                idCard.Suffix = NameSuffixParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PlaceOfBirth:
                idCard.PlaceOfBirth = PlaceOfBirthParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PostalCode:
                idCard.PostalCode = PostalCodeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RevisionDate:
                idCard.RevisionDate = RevisionDateParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Sex:
                idCard.Sex = SexParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1:
                idCard.StreetLine1 = StreetLine1Parser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                idCard.StreetLine1 = StreetLine1LegacyParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine2:
                idCard.StreetLine2 = StreetLine2Parser.Parse(elementId: elementId, rawValue: rawValue, city: idCard.City.Value, jurisdictionCode: idCard.JurisdictionCode.Value, postalCode: idCard.PostalCode.Value);
                break;

            case SubfileElementIds.Under18Until:
                idCard.Under18Until = Under18UntilParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under19Until:
                idCard.Under19Until = Under19UntilParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under21Until:
                idCard.Under21Until = Under21UntilParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                idCard.WasFirstNameTruncated = WasFirstNameTruncatedParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                idCard.WasLastNameTruncated = WasLastNameTruncatedParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                idCard.WasMiddleNameTruncated = WasMiddleNameTruncatedParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInKilograms:
                idCard.Weight = WeightInKilogramsParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInPounds:
                idCard.Weight = WeightInPoundsParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightRange:
                idCard.WeightRange = WeightRangeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return elementHandled;
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
    internal static bool ParseAndSetDriversLicenseElement(string elementId, string? rawValue, Country country, AAMVAVersion version, DriversLicense driversLicense)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(driversLicense);

        var elementHandled = true;

        switch (elementId)
        {
            case SubfileElementIds.EndorsementCodeDescription:
                driversLicense.EndorsementCodeDescription = EndorsementCodeDescriptionParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodes:
                driversLicense.EndorsementCodes = EndorsementCodesParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                driversLicense.EndorsementCodes = EndorsementCodesLegacyParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                driversLicense.HazmatEndorsementExpirationDate = HazmatEndorsementExpirationDateParser.Parse(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                driversLicense.RestrictionCodeDescription = RestrictionCodeDescriptionParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodes:
                driversLicense.RestrictionCodes = RestrictionCodesParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                driversLicense.RestrictionCodes = RestrictionCodesLegacyParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                driversLicense.StandardEndorsementCode = StandardEndorsementCodeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                driversLicense.StandardRestrictionCode = StandardRestrictionCodeParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                driversLicense.StandardVehicleClassification = StandardVehicleClassificationParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                driversLicense.VehicleClassificationDescription = VehicleClassificationDescriptionParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClass:
                driversLicense.VehicleClass = VehicleClassParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                driversLicense.VehicleClass = VehicleClassLegacyParser.Parse(elementId: elementId, rawValue: rawValue);
                break;

            default:
                elementHandled = false;
                break;
        }

        return elementHandled;
    }
}
