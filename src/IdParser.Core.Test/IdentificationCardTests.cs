using IdParser.Core.Constants;
using Xunit;
using Xunit.Abstractions;

namespace IdParser.Core.Test;

public class IdentificationCardTests : BaseTest
{
    public IdentificationCardTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TestTNIdCard()
    {
        var expected = new IdentificationCard
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ELIZABETH"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "21078 MAGNOLIA RD"),
            City = FV<string?>(SubfileElementIds.City, "NASHVILLE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "TN"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "370115509"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1961, 12, 13)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 63)),

            IdNumber = FV(SubfileElementIds.IdNumber, "115775955"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2011),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2018, 2, 6)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2026, 2, 6)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2011, 12, 2)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
        };

        var file = Id("TN");
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);

        Assert.Equal("37011-5509", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Tennessee", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }
}
