using IdParser.Core.Constants;
using IdParser.Core.Parsers.Id;
using IdParser.Core.Parsers.License;

namespace IdParser.Core.Parsers;

internal static class Parser2
{
    internal static bool ParseAndSetIdElements(string elementId, string? rawValue, Country country, AAMVAVersion version, IdentificationCard2 idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(rawValue);
        ArgumentNullException.ThrowIfNull(idCard);

        var handled = true;

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                idCard.AliasFirstName = AliasFirstNameParser.Parse2(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.AliasLastName:
                idCard.AliasLastName = AliasLastNameParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AliasSuffix:
                idCard.AliasSuffix = AliasSuffixParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.AuditInformation:
                idCard.AuditInformation = AuditInformationParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.City:
                idCard.City = CityParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ComplianceType:
                idCard.ComplianceType = ComplianceTypeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.DateOfBirth:
                idCard.DateOfBirth = DateOfBirthParser.Parse2(elementId: elementId, input: rawValue, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                idCard.DocumentDiscriminator = DocumentDiscriminatorParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Ethnicity:
                idCard.Ethnicity = EthnicityParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.ExpirationDate:
                idCard.ExpirationDate = ExpirationDateParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.EyeColor:
                idCard.EyeColor = EyeColorParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.FirstName:
                var firstNameParts = FirstNameParser.Parse2(rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: firstNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.GivenName:
                var givenNameParts = GivenNameParser.Parse(input: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.First, rawValue: rawValue);
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: givenNameParts?.Middle ?? idCard.MiddleName.Value, rawValue: rawValue);
                break;

            case SubfileElementIds.HairColor:
                idCard.HairColor = HairColorParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                idCard.HasTemporaryLawfulStatus = HasTemporaryLawfulStatusParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.Height:
                idCard.Height = HeightParser.Parse2(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IdNumber:
                idCard.IdNumber = IdNumberParser.Parse2(elementId: elementId, input: rawValue);
                break;

            case SubfileElementIds.InventoryControlNumber:
                idCard.InventoryControlNumber = InventoryControlNumberParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonor:
                idCard.IsOrganDonor = IsOrganDonorParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                idCard.IsOrganDonor = IsOrganDonorLegacyParser.Parse2(elementId: elementId, rawValue: rawValue, version);
                break;

            case SubfileElementIds.IssueDate:
                idCard.IssueDate = IssueDateParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                idCard.IsVeteran = IsVeteranParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.JurisdictionCode:
                idCard.JurisdictionCode = JurisdictionCodeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.LastName:
                idCard.LastName = LastNameParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.MiddleName:
                // Some jurisdictions like Wyoming put the middle initial in the FirstName field. If we have
                //   already written that, and if middle name is null, keep the one parsed from first name.
                idCard.MiddleName = MiddleNameParser.Parse2(elementId: elementId, rawValue: rawValue ?? idCard.MiddleName.Value);
                break;

            case SubfileElementIds.Name:
                var nameParts = NameParser2.Parse2(elementId: elementId, rawValue: rawValue);
                idCard.FirstName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.First, rawValue: rawValue);
                idCard.MiddleName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Middle, rawValue: rawValue);
                idCard.LastName = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Last, rawValue: rawValue);
                idCard.Suffix = FieldHelpers.ParsedField(elementId: elementId, value: nameParts?.Suffix, rawValue: rawValue);
                break;

            case SubfileElementIds.NameSuffix:
                idCard.Suffix = NameSuffixParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PlaceOfBirth:
                idCard.PlaceOfBirth = PlaceOfBirthParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.PostalCode:
                idCard.PostalCode = PostalCodeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RevisionDate:
                idCard.RevisionDate = RevisionDateParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Sex:
                idCard.Sex = SexParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1:
                idCard.StreetLine1 = StreetLine1Parser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                idCard.StreetLine1 = StreetLine1LegacyParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StreetLine2:
                idCard.StreetLine2 = StreetLine2Parser.Parse2(elementId: elementId, rawValue: rawValue, city: idCard.City.Value, jurisdictionCode: idCard.JurisdictionCode.Value, postalCode: idCard.PostalCode.Value);
                break;

            case SubfileElementIds.Under18Until:
                idCard.Under18Until = Under18UntilParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under19Until:
                idCard.Under19Until = Under19UntilParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.Under21Until:
                idCard.Under21Until = Under21UntilParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                idCard.WasFirstNameTruncated = WasFirstNameTruncatedParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                idCard.WasLastNameTruncated = WasLastNameTruncatedParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                idCard.WasMiddleNameTruncated = WasMiddleNameTruncatedParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInKilograms:
                idCard.Weight = WeightInKilogramsParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightInPounds:
                idCard.Weight = WeightInPoundsParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.WeightRange:
                idCard.WeightRange = WeightRangeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            default:
                handled = false;
                break;
        }

        return handled;
    }

    internal static bool ParseAndSetDriversLicenseElements(string elementId, string? rawValue, Country country, AAMVAVersion version, DriversLicense2 driversLicense)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(driversLicense);

        var handled = true;

        switch (elementId)
        {
            case SubfileElementIds.EndorsementCodeDescription:
                driversLicense.EndorsementCodeDescription = EndorsementCodeDescriptionParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodes:
                driversLicense.EndorsementCodes = EndorsementCodesParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                driversLicense.EndorsementCodes = EndorsementCodesLegacyParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                driversLicense.HazmatEndorsementExpirationDate = HazmatEndorsementExpirationDateParser.Parse2(elementId: elementId, rawValue: rawValue, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                driversLicense.RestrictionCodeDescription = RestrictionCodeDescriptionParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodes:
                driversLicense.RestrictionCodes = RestrictionCodesParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                driversLicense.RestrictionCodes = RestrictionCodesLegacyParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                driversLicense.StandardEndorsementCode = StandardEndorsementCodeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                driversLicense.StandardRestrictionCode = StandardRestrictionCodeParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                driversLicense.StandardVehicleClassification = StandardVehicleClassificationParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                driversLicense.VehicleClassificationDescription = VehicleClassificationDescriptionParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClass:
                driversLicense.VehicleClass = VehicleClassParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                driversLicense.VehicleClass = VehicleClassLegacyParser.Parse2(elementId: elementId, rawValue: rawValue);
                break;

            default:
                handled = false;
                break;
        }

        return handled;
    }
}
