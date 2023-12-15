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

    public static readonly Field<int> UninitializedInt32 = Uninitialized<int>(elementId: null);
    public static readonly Field<DateTime?> UninitializedNullableDateTime = Uninitialized<DateTime?>(null);
}


public class IdentificationCard2
{
    public Field<IssuerIdentificationNumber> IssuerIdentificationNumber { get; internal set; } = FieldHelpers.UninitializedIssuerIdentificationNumber;

    public Field<AAMVAVersion> AAMVAVersionNumber { get; internal set; } = FieldHelpers.UninitializedAAMVAVersion;
    public Field<int> JurisdictionVersionNumber { get; internal set; } = FieldHelpers.UninitializedInt32;
    //public string IdNumber { get; set; } = null!;

    //public Name Name { get; init; } = new Name();
    //public Address Address { get; init; } = new Address();

    public Field<DateTime?> DateOfBirth { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
    //public DateTime? Under18Until { get; set; }
    //public DateTime? Under19Until { get; set; }
    //public DateTime? Under21Until { get; set; }

    //public DateTime ExpirationDate { get; set; }
    //public DateTime IssueDate { get; set; }
    //public DateTime? RevisionDate { get; set; }

    //public Sex Sex { get; set; }
    //public EyeColor? EyeColor { get; set; }
    //public HairColor? HairColor { get; set; }
    //public Ethnicity? Ethnicity { get; set; }
    //public Height? Height { get; set; }
    //public Weight Weight { get; set; } = null!;
    //public WeightRange? WeightRange { get; set; }

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
