using Xunit;
using Xunit.Abstractions;

namespace IdParser.Core.Test;

public class BaseTest
{
    protected readonly XUnitTextWriter _output;

    public BaseTest(ITestOutputHelper output)
    {
        _output = new XUnitTextWriter(output);
        Console.SetOut(_output);
    }

    protected string Id(string jurisdiction) 
        => File.ReadAllText(Path.Combine("Ids", $"{jurisdiction}.txt"));

    protected string License(string jurisdiction) 
        => File.ReadAllText(Path.Combine("Licenses", $"{jurisdiction}.txt"));

    /// <summary>
    /// Create a Field value. elementId and rawValue don't matter.
    /// </summary>
    protected Field<T> FV<T>(string? elementId, T value)
        => FieldHelpers.ParsedField(elementId, value, rawValue: null);

    protected void AssertIdCard(IdentificationCard expected, IdentificationCard actual)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actual);

        Assert.Equal(expected.FirstName.Value, actual.FirstName.Value);
        Assert.Equal(expected.MiddleName.Value, actual.MiddleName.Value);
        Assert.Equal(expected.LastName.Value, actual.LastName.Value);
        Assert.Equal(expected.Suffix.Value, actual.Suffix.Value);

        Assert.Equal(expected.WasFirstNameTruncated.Value, actual.WasFirstNameTruncated.Value);
        Assert.Equal(expected.WasMiddleNameTruncated.Value, actual.WasMiddleNameTruncated.Value);
        Assert.Equal(expected.WasLastNameTruncated.Value, actual.WasLastNameTruncated.Value);

        Assert.Equal(expected.City.Value, actual.City.Value);
        Assert.Equal(expected.StreetLine1.Value, actual.StreetLine1.Value);
        Assert.Equal(expected.StreetLine2.Value, actual.StreetLine2.Value);
        Assert.Equal(expected.JurisdictionCode.Value, actual.JurisdictionCode.Value);
        Assert.Equal(expected.JurisdictionCode.Value, actual.IssuerIdentificationNumber.Value.GetAbbreviationOrDefault());
        Assert.Equal(expected.PostalCode.Value, actual.PostalCode.Value);
        Assert.Equal(expected.Country.Value, actual.Country.Value);

        Assert.Equal(expected.DateOfBirth.Value, actual.DateOfBirth.Value);
        Assert.Equal(expected.PlaceOfBirth.Value, actual.PlaceOfBirth.Value);
        Assert.Equal(expected.Sex.Value, actual.Sex.Value);
        Assert.Equal(expected.Height.Value, actual.Height.Value);
        Assert.Equal(expected.Weight.Value, actual.Weight.Value);
        Assert.Equal(expected.WeightRange.Value, actual.WeightRange.Value);

        Assert.Equal(expected.EyeColor.Value, actual.EyeColor.Value);
        Assert.Equal(expected.HairColor.Value, actual.HairColor.Value);
        Assert.Equal(expected.Ethnicity.Value, actual.Ethnicity.Value);

        Assert.Equal(expected.IdNumber.Value, actual.IdNumber.Value);
        Assert.Equal(expected.AAMVAVersionNumber.Value, actual.AAMVAVersionNumber.Value);

        Assert.Equal(expected.IssueDate.Value, actual.IssueDate.Value);
        Assert.Equal(expected.ExpirationDate.Value, actual.ExpirationDate.Value);
        Assert.Equal(expected.RevisionDate.Value, actual.RevisionDate.Value);

        Assert.Equal(expected.Under18Until.Value, actual.Under18Until.Value);
        Assert.Equal(expected.Under19Until.Value, actual.Under19Until.Value);
        Assert.Equal(expected.Under21Until.Value, actual.Under21Until.Value);

        Assert.Equal(expected.ComplianceType.Value, actual.ComplianceType.Value);
        Assert.Equal(expected.HasTemporaryLawfulStatus.Value, actual.HasTemporaryLawfulStatus.Value);
        Assert.Equal(expected.IsOrganDonor.Value, actual.IsOrganDonor.Value);
        Assert.Equal(expected.IsVeteran.Value, actual.IsVeteran.Value);
    }

    protected void AssertLicense(DriversLicense expected, IdentificationCard actualId)
    {
        Assert.NotNull(expected);
        Assert.NotNull(actualId);
        Assert.IsType<DriversLicense>(actualId);

        var actual = (DriversLicense)actualId;

        Assert.Equal(expected.VehicleClass.Value, actual.VehicleClass.Value);
        Assert.Equal(expected.RestrictionCodes.Value, actual.RestrictionCodes.Value);
        Assert.Equal(expected.EndorsementCodes.Value, actual.EndorsementCodes.Value);
        Assert.Equal(expected.VehicleClassificationDescription.Value, actual.VehicleClassificationDescription.Value);
        Assert.Equal(expected.EndorsementCodeDescription.Value, actual.EndorsementCodeDescription.Value);
        Assert.Equal(expected.RestrictionCodeDescription.Value, actual.RestrictionCodeDescription.Value);

        Assert.Equal(expected.StandardVehicleClassification.Value, actual.StandardVehicleClassification.Value);
        Assert.Equal(expected.StandardEndorsementCode.Value, actual.StandardEndorsementCode.Value);
        Assert.Equal(expected.StandardRestrictionCode.Value, actual.StandardRestrictionCode.Value);
        Assert.Equal(expected.HazmatEndorsementExpirationDate.Value, actual.HazmatEndorsementExpirationDate.Value);
    }

    protected void LogUnhandledElementIds(IdentificationCard idCard)
    {
        if (idCard.UnhandledElementIds.Count == 0)
        {
            return;
        }

        _output.WriteLine($"State '{idCard.IssuerIdentificationNumber.Value.GetAbbreviationOrDefault()}' has unhandled element Ids: {string.Join(", ", idCard.UnhandledElementIds)}.");
    }
}
