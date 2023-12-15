using IdParser.Core.Constants;

namespace IdParser.Core;

public class IdentificationCard
{
    public IssuerIdentificationNumber IssuerIdentificationNumber { get; set; }
    public AAMVAVersion AAMVAVersionNumber { get; set; }
    public int JurisdictionVersionNumber { get; set; }
    public string IdNumber { get; set; } = null!;

    public Name Name { get; init; } = new Name();
    public Address Address { get; init; } = new Address();

    public DateTime DateOfBirth { get; set; }
    public DateTime? Under18Until { get; set; }
    public DateTime? Under19Until { get; set; }
    public DateTime? Under21Until { get; set; }

    public DateTime ExpirationDate { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? RevisionDate { get; set; }

    public Sex Sex { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairColor? HairColor { get; set; }
    public Ethnicity? Ethnicity { get; set; }
    public Height? Height { get; set; }
    public Weight Weight { get; set; } = null!;
    public WeightRange? WeightRange { get; set; }

    public string? DocumentDiscriminator { get; set; }

    public string PlaceOfBirth { get; set; } = null!;
    public string AuditInformation { get; set; } = null!;
    public string InventoryControlNumber { get; set; } = null!;

    public ComplianceType? ComplianceType { get; set; }

    public bool? HasTemporaryLawfulStatus { get; set; }
    public bool? IsOrganDonor { get; set; }
    public bool? IsVeteran { get; set; }

    public Dictionary<string, string> AdditionalJurisdictionElements { get; } = new();
}

public readonly record struct Field<T>(
    string? ElementId,
    T? Value,
    string? RawValue,
    string? Error,
    bool Present
    )
{
}

public static class FieldHelpers
{
    private static Field<TValue> Uninitialized<TValue>(string? elementId)
        => new Field<TValue>(ElementId: elementId, Value: default, RawValue: null, Error: null, Present: false);

    public static Field<TValue> ParsedField<TValue>(string? elementId, TValue value, string? rawValue)
        => new Field<TValue>(ElementId: elementId, Value: value, RawValue: rawValue, Error: null, Present: true);

    public static Field<TValue> UnparsedField<TValue>(string? elementId, string? rawValue, string error)
        => new Field<TValue>(ElementId: elementId, Value: default, RawValue: rawValue, Error: error, Present: true);


    public static readonly Field<IssuerIdentificationNumber> UninitializedIssuerIdentificationNumber= Uninitialized<IssuerIdentificationNumber>(elementId: null);
    public static readonly Field<AAMVAVersion> UninitializedAAMVAVersion = Uninitialized<AAMVAVersion>(elementId: null);
    public static readonly Field<Country> UninitializedCountry = Uninitialized<Country>(elementId: SubfileElementIds.Country);
    public static readonly Field<Sex?> UninitializedNullableSex = Uninitialized<Sex?>(elementId: SubfileElementIds.Sex);
    public static readonly Field<EyeColor?> UninitializedNullableEyeColor = Uninitialized<EyeColor?>(elementId: SubfileElementIds.EyeColor);
    public static readonly Field<HairColor?> UninitializedNullableHairColor = Uninitialized<HairColor?>(elementId: SubfileElementIds.HairColor);
    public static readonly Field<Ethnicity?> UninitializedNullableEthnicity = Uninitialized<Ethnicity?>(elementId: SubfileElementIds.Ethnicity);
    public static readonly Field<Height?> UninitializedNullableHeight = Uninitialized<Height?>(elementId: SubfileElementIds.Height);
    public static readonly Field<Weight?> UninitializedNullableWeight = Uninitialized<Weight?>(elementId: null); // Could be WeightInPounds or WeightInKilograms.
    public static readonly Field<WeightRange?> UninitializedNullableWeightRange = Uninitialized<WeightRange?>(elementId: SubfileElementIds.WeightRange);

    public static readonly Field<bool?> UninitializedNullableBoolean = Uninitialized<bool?>(elementId: null);
    public static readonly Field<DateTime?> UninitializedNullableDateTime = Uninitialized<DateTime?>(null);
    public static readonly Field<int> UninitializedInt32 = Uninitialized<int>(elementId: null);
    public static readonly Field<string> UninitializedString = Uninitialized<string>(elementId: null);
    public static readonly Field<string?> UninitializedNullableString = Uninitialized<string?>(elementId: null);
}


public class IdentificationCard2
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

    //public string? DocumentDiscriminator { get; set; }

    //public string PlaceOfBirth { get; set; } = null!;
    //public string AuditInformation { get; set; } = null!;
    //public string InventoryControlNumber { get; set; } = null!;

    //public ComplianceType? ComplianceType { get; set; }

    //public bool? HasTemporaryLawfulStatus { get; set; }
    //public bool? IsOrganDonor { get; set; }
    //public bool? IsVeteran { get; set; }

    public Dictionary<string, Field<string?>> AdditionalJurisdictionElements { get; } = new();

    public List<string> UnhandledElementIds { get; private set; } = new();
}
