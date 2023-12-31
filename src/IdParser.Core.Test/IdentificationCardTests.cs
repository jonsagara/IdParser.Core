﻿using Xunit;
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
            Name = new Name
            {
                First = "ELIZABETH",
                Middle = "MOTORIST",
                Last = "SMITH",

                WasFirstTruncated = false,
                WasMiddleTruncated = false,
                WasLastTruncated = false
            },

            Address = new Address
            {
                StreetLine1 = "21078 MAGNOLIA RD",
                City = "NASHVILLE",
                JurisdictionCode = "TN",
                PostalCode = "370115509",
                Country = Country.USA
            },

            DateOfBirth = new DateTime(1961, 12, 13),
            Sex = Sex.Female,
            EyeColor = EyeColor.Green,
            Height = new Height(totalInches: 63),

            IdNumber = "115775955",
            AAMVAVersionNumber = AAMVAVersion.AAMVA2011,

            IssueDate = new DateTime(2018, 02, 06),
            ExpirationDate = new DateTime(2026, 02, 06),
            RevisionDate = new DateTime(2011, 12, 02),

            IsOrganDonor = true
        };

        var file = Id("TN");
        var (idCard, unhandledElementIds) = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(idCard, unhandledElementIds);

        AssertIdCard(expected, idCard);

        Assert.Equal("37011-5509", idCard.Address.PostalCodeDisplay);
        Assert.Equal("Tennessee", idCard.IssuerIdentificationNumber.GetDescriptionOrDefault());
    }
}
