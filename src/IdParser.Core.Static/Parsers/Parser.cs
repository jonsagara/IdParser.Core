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

            default:
                throw new ArgumentOutOfRangeException(nameof(elementId), elementId, $"Unsupported elementId '{elementId}'.");
        }
    }
}
