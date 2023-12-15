namespace IdParser.Core;

public class DriversLicense : IdentificationCard
{
    public DriversLicenseJurisdiction Jurisdiction { get; init; } = new DriversLicenseJurisdiction();
    public string? StandardVehicleClassification { get; set; }
    public string? StandardEndorsementCode { get; set; }
    public string? StandardRestrictionCode { get; set; }
    public DateTime? HazmatEndorsementExpirationDate { get; set; }
}

public class DriversLicense2 : IdentificationCard2
{
    public DriversLicenseJurisdiction Jurisdiction { get; init; } = new DriversLicenseJurisdiction();
    public string? StandardVehicleClassification { get; set; }
    public string? StandardEndorsementCode { get; set; }
    public string? StandardRestrictionCode { get; set; }
    public DateTime? HazmatEndorsementExpirationDate { get; set; }
}