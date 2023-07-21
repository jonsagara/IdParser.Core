using IdParser.Core.Static.Constants;
using IdParser.Core.Static.Parsers.Id;

namespace IdParser.Core.Static.Parsers;

internal static class Parser
{
    internal static void ParseAndSet(string elementId, string data, Country country, IdentificationCard idCard)
    {
        ArgumentNullException.ThrowIfNull(elementId);
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(idCard);

        switch (elementId)
        {
            case SubfileElementIds.AliasFirstName:
                idCard.Name.AliasFirst = AliasFirstNameParser.Parse(input: data, idCard.AAMVAVersionNumber);
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
                idCard.DateOfBirth = DateOfBirthParser.Parse(input: data, country, idCard.AAMVAVersionNumber);
                break;

            case SubfileElementIds.DocumentDiscriminator:
                idCard.DocumentDiscriminator = DocumentDiscriminatorParser.Parse(input: data);
                break;

            case SubfileElementIds.Ethnicity:
                idCard.Ethnicity = EthnicityParser.Parse(input: data);
                break;

            case SubfileElementIds.ExpirationDate:
                idCard.ExpirationDate = ExpirationDateParser.Parse(input: data, country, idCard.AAMVAVersionNumber);
                break;

            case SubfileElementIds.EyeColor:
                idCard.EyeColor = EyeColorParser.Parse(input: data);
                break;

            case SubfileElementIds.FirstName:
                var firstNameParts = FirstNameParser.Parse(input: data);
                idCard.Name.First = firstNameParts?.First;
                idCard.Name.Middle = firstNameParts?.Middle;
                break;

            case SubfileElementIds.GivenName:
                var givenNameParts = GivenNameParser.Parse(input: data);
                idCard.Name.First = givenNameParts.First;
                idCard.Name.Middle = givenNameParts.Middle;
                break;

            case SubfileElementIds.HairColor:
                idCard.HairColor = HairColorParser.Parse(input: data);
                break;

            case SubfileElementIds.HasTemporaryLawfulStatus:
                idCard.HasTemporaryLawfulStatus = HasTemporaryLawfulStatusParser.Parse(input: data);
                break;

            case SubfileElementIds.Height:
                idCard.Height = HeightParser.Parse(input: data, idCard.AAMVAVersionNumber);
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
                idCard.IsOrganDonor = IsOrganDonorLegacyParser.Parse(data, idCard.AAMVAVersionNumber);
                break;

            case SubfileElementIds.IssueDate:
                idCard.IssueDate = IssueDateParser.Parse(input: data, country, idCard.AAMVAVersionNumber);
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

            default:
                throw new ArgumentOutOfRangeException(nameof(elementId), elementId, $"Unsupported elementId '{elementId}'.");
        }
    }
}
