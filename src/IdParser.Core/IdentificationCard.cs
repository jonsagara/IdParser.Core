namespace IdParser.Core;

public class IdentificationCard
{
    public IssuerIdentificationNumber IssuerIdentificationNumber { get; set; }
    public AAMVAVersion AAMVAVersionNumber { get; set; }
    public int JurisdictionVersionNumber { get; set; }
    public string IdNumber { get; set; } = null!;

    public Name Name { get; set; } = new Name();
    public Address Address { get; set; } = new Address();

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
