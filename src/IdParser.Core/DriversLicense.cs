﻿namespace IdParser.Core.Static;

public class DriversLicense : IdentificationCard
{
    public DriversLicenseJurisdiction Jurisdiction { get; set; } = new DriversLicenseJurisdiction();
    public string? StandardVehicleClassification { get; set; }
    public string? StandardEndorsementCode { get; set; }
    public string? StandardRestrictionCode { get; set; }
    public DateTime? HazmatEndorsementExpirationDate { get; set; }
}