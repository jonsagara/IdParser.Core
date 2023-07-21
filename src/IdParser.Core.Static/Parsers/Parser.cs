using System;
using IdParser.Core.Static.Constants;
using IdParser.Core.Static.Parsers.Id;
using IdParser.Core.Static.Parsers.License;

namespace IdParser.Core.Static.Parsers;

internal static class Parser
{
    internal static bool ParseAndSetIdElements(string elementId, string data, Country country, AAMVAVersion version, IdentificationCard idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(idCard);

        var handled = true;

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                idCard.Name.AliasFirst = AliasFirstNameParser.Parse(input: data, version);
                break;

            case SubfileElementIds.AliasLastName:
                idCard.Name.AliasLast = AliasLastNameParser.Parse(input: data);
                break;

            case SubfileElementIds.AliasSuffix:
                idCard.Name.AliasSuffix = AliasSuffixParser.Parse(input: data);
                break;

            case SubfileElementIds.AuditInformation:
                idCard.AuditInformation = AuditInformationParser.Parse(input: data);
                break;

            case SubfileElementIds.City:
                idCard.Address.City = CityParser.Parse(input: data);
                break;

            case SubfileElementIds.ComplianceType:
                idCard.ComplianceType = ComplianceTypeParser.Parse(input: data);
                break;

            case SubfileElementIds.DateOfBirth:
                idCard.DateOfBirth = DateOfBirthParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                idCard.DocumentDiscriminator = DocumentDiscriminatorParser.Parse(input: data);
                break;

            case SubfileElementIds.Ethnicity:
                idCard.Ethnicity = EthnicityParser.Parse(input: data);
                break;

            case SubfileElementIds.ExpirationDate:
                idCard.ExpirationDate = ExpirationDateParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.EyeColor:
                idCard.EyeColor = EyeColorParser.Parse(input: data);
                break;

            case SubfileElementIds.FirstName:
                var firstNameParts = FirstNameParser.Parse(input: data);
                idCard.Name.First = firstNameParts?.First;
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.Name.Middle = firstNameParts?.Middle ?? idCard.Name.Middle;
                break;

            case SubfileElementIds.GivenName:
                var givenNameParts = GivenNameParser.Parse(input: data);
                idCard.Name.First = givenNameParts.First;
                // If we didn't parse a middle name out of the input, keep the existing middle name.
                idCard.Name.Middle = givenNameParts.Middle ?? idCard.Name.Middle;
                break;

            case SubfileElementIds.HairColor:
                idCard.HairColor = HairColorParser.Parse(input: data);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                idCard.HasTemporaryLawfulStatus = HasTemporaryLawfulStatusParser.Parse(input: data);
                break;

            case SubfileElementIds.Height:
                idCard.Height = HeightParser.Parse(input: data, version);
                break;

            case SubfileElementIds.IdNumber:
                idCard.IdNumber = IdNumberParser.Parse(input: data);
                break;

            case SubfileElementIds.InventoryControlNumber:
                idCard.InventoryControlNumber = InventoryControlNumberParser.Parse(input: data);
                break;

            case SubfileElementIds.IsOrganDonor:
                idCard.IsOrganDonor = IsOrganDonorParser.Parse(input: data);
                break;

            case SubfileElementIds.IsOrganDonorLegacy:
                idCard.IsOrganDonor = IsOrganDonorLegacyParser.Parse(data, version);
                break;

            case SubfileElementIds.IssueDate:
                idCard.IssueDate = IssueDateParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.IsVeteran:
                idCard.IsVeteran = IsVeteranParser.Parse(input: data);
                break;

            case SubfileElementIds.JurisdictionCode:
                idCard.Address.JurisdictionCode = JurisdictionCodeParser.Parse(input: data);
                break;

            case SubfileElementIds.LastName:
                idCard.Name.Last = LastNameParser.Parse(input: data);
                break;

            case SubfileElementIds.MiddleName:
                idCard.Name.Middle = MiddleNameParser.Parse(input: data);
                break;

            case SubfileElementIds.Name:
                var nameParts = NameParser.Parse(input: data);
                idCard.Name.First = nameParts?.First;
                idCard.Name.Middle = nameParts?.Middle;
                idCard.Name.Last = nameParts?.Last;
                idCard.Name.Suffix = nameParts?.Suffix;
                break;

            case SubfileElementIds.NameSuffix:
                idCard.Name.Suffix = NameSuffixParser.Parse(input: data);
                break;

            case SubfileElementIds.PlaceOfBirth:
                idCard.PlaceOfBirth = PlaceOfBirthParser.Parse(input: data);
                break;

            case SubfileElementIds.PostalCode:
                idCard.Address.PostalCode = PostalCodeParser.Parse(input: data);
                break;

            case SubfileElementIds.RevisionDate:
                idCard.RevisionDate = RevisionDateParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.Sex:
                idCard.Sex = SexParser.Parse(input: data);
                break;

            case SubfileElementIds.StreetLine1:
                idCard.Address.StreetLine1 = StreetLine1Parser.Parse(input: data);
                break;

            case SubfileElementIds.StreetLine1Legacy:
                idCard.Address.StreetLine1 = StreetLine1LegacyParser.Parse(input: data);
                break;

            case SubfileElementIds.StreetLine2:
                idCard.Address.StreetLine2 = StreetLine2Parser.Parse(input: data, idCard.Address);
                break;

            case SubfileElementIds.Under18Until:
                idCard.Under18Until = Under18UntilParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.Under19Until:
                idCard.Under19Until = Under19UntilParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.Under21Until:
                idCard.Under21Until = Under21UntilParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.WasFirstNameTruncated:
                idCard.Name.WasFirstTruncated = WasFirstNameTruncatedParser.Parse(input: data);
                break;

            case SubfileElementIds.WasLastNameTruncated:
                idCard.Name.WasLastTruncated = WasLastNameTruncatedParser.Parse(input: data);
                break;

            case SubfileElementIds.WasMiddleNameTruncated:
                idCard.Name.WasMiddleTruncated = WasMiddleNameTruncatedParser.Parse(input: data);
                break;

            case SubfileElementIds.WeightInKilograms:
                idCard.Weight = WeightInKilogramsParser.Parse(input: data);
                break;

            case SubfileElementIds.WeightInPounds:
                idCard.Weight = WeightInPoundsParser.Parse(input: data);
                break;

            case SubfileElementIds.WeightRange:
                // Alberta is special: they put the weight in KG in the Pounds field, -AND- they
                //   specify a weight range after the weight field. We need to handle that here.
                var weightRange = WeightRangeParser.Parse(input: data);
                if (idCard.Weight is not null)
                {
                    idCard.Weight.WeightRange = weightRange.WeightRange;
                }
                else
                {
                    idCard.Weight = weightRange;
                }
                break;

            default:
                handled = false;
                break;
        }

        return handled;
    }

    internal static bool ParseAndSetDriversLicenseElements(string elementId, string data, Country country, AAMVAVersion version, DriversLicense driversLicense)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(driversLicense);

        var handled = true;

        switch (elementId)
        {
            case SubfileElementIds.EndorsementCodeDescription:
                driversLicense.Jurisdiction.EndorsementCodeDescription = EndorsementCodeDescriptionParser.Parse(input: data);
                break;

            case SubfileElementIds.EndorsementCodes:
                driversLicense.Jurisdiction.EndorsementCodes = EndorsementCodesParser.Parse(input: data);
                break;

            case SubfileElementIds.EndorsementCodesLegacy:
                driversLicense.Jurisdiction.EndorsementCodes = EndorsementCodesLegacyParser.Parse(input: data);
                break;

            case SubfileElementIds.HazmatEndorsementExpirationDate:
                driversLicense.HazmatEndorsementExpirationDate = HazmatEndorsementExpirationDateParser.Parse(input: data, country, version);
                break;

            case SubfileElementIds.RestrictionCodeDescription:
                driversLicense.Jurisdiction.RestrictionCodeDescription = RestrictionCodeDescriptionParser.Parse(input: data);
                break;

            case SubfileElementIds.RestrictionCodes:
                driversLicense.Jurisdiction.RestrictionCodes = RestrictionCodesParser.Parse(input: data);
                break;

            case SubfileElementIds.RestrictionCodesLegacy:
                driversLicense.Jurisdiction.RestrictionCodes = RestrictionCodesLegacyParser.Parse(input: data);
                break;

            case SubfileElementIds.StandardEndorsementCode:
                driversLicense.StandardEndorsementCode = StandardEndorsementCodeParser.Parse(input: data);
                break;

            case SubfileElementIds.StandardRestrictionCode:
                driversLicense.StandardRestrictionCode = StandardRestrictionCodeParser.Parse(input: data);
                break;

            case SubfileElementIds.StandardVehicleClassification:
                driversLicense.StandardVehicleClassification = StandardVehicleClassificationParser.Parse(input: data);
                break;

            case SubfileElementIds.VehicleClassificationDescription:
                driversLicense.Jurisdiction.VehicleClassificationDescription = VehicleClassificationDescriptionParser.Parse(input: data);
                break;

            case SubfileElementIds.VehicleClass:
                driversLicense.Jurisdiction.VehicleClass = VehicleClassParser.Parse(input: data);
                break;

            case SubfileElementIds.VehicleClassLegacy:
                driversLicense.Jurisdiction.VehicleClass = VehicleClassLegacyParser.Parse(input: data);
                break;

            default:
                handled = false;
                break;
        }

        return handled;
    }
}
