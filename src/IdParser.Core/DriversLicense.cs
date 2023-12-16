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
    //
    // Jurisdiction
    //

    public Field<string?> VehicleClass { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> RestrictionCodes { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> EndorsementCodes { get; internal set; } = FieldHelpers.UninitializedNullableString;

    // Optional elements
    public Field<string?> VehicleClassificationDescription { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> EndorsementCodeDescription { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> RestrictionCodeDescription { get; internal set; } = FieldHelpers.UninitializedNullableString;


    //
    // Standard
    //

    public Field<string?> StandardVehicleClassification { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> StandardEndorsementCode { get; internal set; } = FieldHelpers.UninitializedNullableString;
    public Field<string?> StandardRestrictionCode { get; internal set; } = FieldHelpers.UninitializedNullableString;

    public Field<DateTime?> HazmatEndorsementExpirationDate { get; internal set; } = FieldHelpers.UninitializedNullableDateTime;
}