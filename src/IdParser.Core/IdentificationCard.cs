using System.Collections.ObjectModel;
using IdParser.Core.Parsers;

namespace IdParser.Core;

public class IdentificationCard
{
    public Field<IssuerIdentificationNumber> IssuerIdentificationNumber { get; internal set; } = FieldHelpers.UninitializedIssuerIdentificationNumber;

    public Field<AAMVAVersion> AAMVAVersionNumber { get; internal set; } = FieldHelpers.UninitializedAAMVAVersion;
    public Field<int> JurisdictionVersionNumber { get; internal set; } = FieldHelpers.UninitializedInt32;
    public Field<string> IdNumber { get; internal set; } = FieldHelpers.UninitializedString;

    public Field<string?> FirstName { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> MiddleName { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> LastName { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> Suffix { get; internal set; } = FieldHelpers.UninitializedNullableString;

    public Field<bool?> WasFirstNameTruncated { get; internal set; } = FieldHelpers.UninitializedNullableBoolean;
    public Field<bool?> WasMiddleNameTruncated { get; internal set; } = FieldHelpers.UninitializedNullableBoolean;
    public Field<bool?> WasLastNameTruncated { get; internal set; } = FieldHelpers.UninitializedNullableBoolean;

    public Field<string?> AliasFirstName { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> AliasLastName { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> AliasSuffix { get; internal set; } = FieldHelpers.UninitializedNullableString;

    public Field<string?> StreetLine1 { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> StreetLine2 { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> City { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> JurisdictionCode { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> PostalCode { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<Country> Country { get; internal set; } = FieldHelpers.UninitializedCountry;

    public string? PostalCodeDisplay
        => IdentificationCardHelper.PostalCodeDisplay(PostalCode.Value, Country.Value);

    public Field<DateTime?> DateOfBirth { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    public Field<DateTime?> Under18Until { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    public Field<DateTime?> Under19Until { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    public Field<DateTime?> Under21Until { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;

    public Field<DateTime?> ExpirationDate { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    public Field<DateTime?> IssueDate { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    public Field<DateTime?> RevisionDate { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;

    public Field<Sex?> Sex { get; internal set; } = FieldHelpers.UninitializedNullableSex;
    public Field<EyeColor?> EyeColor { get; internal set; } = FieldHelpers.UninitializedNullableEyeColor;
    public Field<HairColor?> HairColor { get; internal set; } = FieldHelpers.UninitializedNullableHairColor;
    public Field<Ethnicity?> Ethnicity { get; internal set; } = FieldHelpers.UninitializedNullableEthnicity;
    public Field<Height?> Height { get; internal set; } = FieldHelpers.UninitializedNullableHeight;
    public Field<Weight?> Weight { get; internal set; } = FieldHelpers.UninitializedNullableWeight;
    public Field<WeightRange?> WeightRange { get; internal set; } = FieldHelpers.UninitializedNullableWeightRange;

    public Field<string?> DocumentDiscriminator { get; internal set; } = FieldHelpers.UninitializedNullableString;

    public Field<string?> PlaceOfBirth { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> AuditInformation { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> InventoryControlNumber { get; internal set; } = FieldHelpers.UninitializedNullableString;

    public Field<ComplianceType?> ComplianceType { get; internal set; } = FieldHelpers.UninitializedNullableComplianceType;

    public Field<bool> HasTemporaryLawfulStatus { get; internal set; } = FieldHelpers.UninitializedBoolean;
    public Field<bool> IsOrganDonor { get; internal set; } = FieldHelpers.UninitializedBoolean;
    public Field<bool> IsVeteran { get; internal set; } = FieldHelpers.UninitializedBoolean;

    public Dictionary<string, Field<string?>> AdditionalJurisdictionElements { get; } = new();

    // Justification: this is a stupid rule.
#pragma warning disable CA1002 // Do not expose generic lists
    public List<string> UnhandledElementIds { get; private set; } = new();
#pragma warning restore CA1002 // Do not expose generic lists
}
