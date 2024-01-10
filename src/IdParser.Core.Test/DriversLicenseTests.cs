using IdParser.Core.Constants;
using Xunit;
using Xunit.Abstractions;

namespace IdParser.Core.Test;

public class DriversLicenseTests : BaseTest
{
    public DriversLicenseTests(ITestOutputHelper output)
        : base(output)
    {
    }


    [Fact]
    public void TestMA2009License()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ROBERT"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "LOWNEY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 MAIN STREET"),
            City = FV<string?>(SubfileElementIds.City, "BOSTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "021080"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1977, 7, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),

            IdNumber = FV(SubfileElementIds.IdNumber, "S65807412"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 6, 29)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 7, 7)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2009, 07, 15)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("MA 2009");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMA2016License()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MORRIS"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "T"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SAMPLE"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "24 BEACON STREET"),
            City = FV<string?>(SubfileElementIds.City, "BOSTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "02133"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1971, 12, 31)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 62)),

            IdNumber = FV(SubfileElementIds.IdNumber, "S12345678"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 8, 9)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 8, 16)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 02, 22)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("MA 2016");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("02133", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal("08102016 REV 02222016", parseResult.Card.DocumentDiscriminator.Value);
        Assert.Equal("12345S123456780612", parseResult.Card.InventoryControlNumber.Value);

        Assert.Equal("MA504", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMZ").Value.Value);
        Assert.Equal("08102016", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMB").Value.Value);
    }

    [Fact]
    public void TestMALicenseWithNoMiddleName()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "TONY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "ROBERT"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 MAIN STREET"),
            City = FV<string?>(SubfileElementIds.City, "BOSTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "021080"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1977, 7, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),

            IdNumber = FV(SubfileElementIds.IdNumber, "S65807412"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 06, 29)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 07, 07)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2009, 7, 15)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("MA No Middle Name");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNYLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "M"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "Motorist"),
            LastName = FV<string?>(SubfileElementIds.LastName, "Michael"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "2345 ANYWHERE STREET"),
            City = FV<string?>(SubfileElementIds.City, "YOUR CITY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NY"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "12345"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(2013, 8, 31)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 64)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "NONE"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2012),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 8, 31)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2013, 8, 31)),
        };

        var file = License("NY");
        var parseResult = Barcode.Parse(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("New York", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestVALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JUSTIN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "WILLIAM"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MAURY"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "17 FIRST STREET"),
            City = FV<string?>(SubfileElementIds.City, "STAUNTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "VA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "24401"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1958, 7, 15)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 75)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "T16700185"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2009, 8, 14)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2017, 8, 14)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2008, 12, 10)),

            HasTemporaryLawfulStatus = FV<bool>(SubfileElementIds.HasTemporaryLawfulStatus, false),
            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),

            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "158X9"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "S")
        };

        var file = License("VA");
        var parseResult = Barcode.Parse(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Virginia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.IsType<DriversLicense>(parseResult.Card);

        if (parseResult.Card is DriversLicense license)
        {
            Assert.Equal("158X9", license.RestrictionCodes.Value);
        }
    }

    [Fact]
    public void TestGALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JANICE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SAMPLE"),
            Suffix = FV<string?>(SubfileElementIds.NameSuffix, "PH.D."),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 NORTH STATE ST."),
            City = FV<string?>(SubfileElementIds.City, "ANYTOWN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "GA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "30334"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1957, 7, 1)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 64)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs101To130),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),

            IdNumber = FV(SubfileElementIds.IdNumber, "100000001"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2006, 7, 1)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2013, 2, 1)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "P")
        };

        var file = License("GA");
        var parseResult = Barcode.Parse(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Georgia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ADULT"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "A"),
            LastName = FV<string?>(SubfileElementIds.LastName, "CTLIC"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "60 STATE ST"),
            City = FV<string?>(SubfileElementIds.City, "WETHERSFIELD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "061091896"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1961, 1, 1)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 6)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),

            IdNumber = FV(SubfileElementIds.IdNumber, "990000001"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2009, 2, 23)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2015, 1, 1)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B")
        };

        var file = License("CT");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.IsType<DriversLicense>(parseResult.Card);

        if (parseResult.Card is DriversLicense license)
        {
            Assert.Equal("D", license.VehicleClass.Value);
            Assert.Equal("B", license.RestrictionCodes.Value);
        }
    }

    [Fact]
    public void TestCTLicenseWebBrowser()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ADULT"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "A"),
            LastName = FV<string?>(SubfileElementIds.LastName, "CTLIC"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "60 STATE ST"),
            City = FV<string?>(SubfileElementIds.City, "WETHERSFIELD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "061091896"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1961, 1, 1)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 6)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),

            IdNumber = FV(SubfileElementIds.IdNumber, "990000001"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2009, 2, 23)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2015, 1, 1)),

            IsOrganDonor = FV<bool>(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B")
        };

        var file = License("CT Web Browser");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicenseNoMiddleName()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "CHUNG"),
            LastName = FV<string?>(SubfileElementIds.LastName, "WANG"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 SIDE ST"),
            City = FV<string?>(SubfileElementIds.City, "WATERBURY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "067081897"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1949, 3, 3)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 8)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "035032278"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 1, 19)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 3, 3)),

            IsOrganDonor = FV<bool>(SubfileElementIds.IsOrganDonor, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("CT No Middle Name");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMOLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "FirstNameTest"),
            LastName = FV<string?>(SubfileElementIds.LastName, "LastNameTest"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 ABC TEST ADDRESS 2ND FL"),
            City = FV<string?>(SubfileElementIds.City, "ST LOUIS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MO"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "633011"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(2017, 8, 9)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 8)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 155)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "X100097001"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2011, 6, 30)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 2, 4)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "F"),
        };

        var file = License("MO");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Missouri", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal("MAST LOUIS CITY", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMZ").Value.Value);
        Assert.Equal("112001810097", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMB").Value.Value);

        Assert.IsType<DriversLicense>(parseResult.Card);

        if (parseResult.Card is DriversLicense license)
        {
            Assert.Equal("F", license.VehicleClass.Value);
        }
    }

    [Fact]
    public void TestFLLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOEY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MIDLAND"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1234 PARK ST LOT 504"),
            City = FV<string?>(SubfileElementIds.City, "KEY WEST"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "FL"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "330400504"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1941, 5, 9)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 6, inches: 1)),

            IdNumber = FV(SubfileElementIds.IdNumber, "H574712510891"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 5, 1)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 3, 9)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "E"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "A"),
        };

        var file = License("FL");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("33040-0504", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Florida", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal(5, parseResult.Card.AdditionalJurisdictionElements.Count);
        Assert.Equal("FA", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZFZ").Value.Value);

        if (parseResult.Card is DriversLicense license)
        {
            Assert.Equal("A", license.RestrictionCodes.Value);
            Assert.Equal("E", license.VehicleClass.Value);
        }
    }

    [Fact]
    public void TestNHLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DONNIE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "G"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "802 WILLIAMS ST"),
            City = FV<string?>(SubfileElementIds.City, "SOMETOWN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NH"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "01234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1977, 11, 6)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 69)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),

            IdNumber = FV(SubfileElementIds.IdNumber, "NHI17128755"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 12, 19)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 11, 6)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 6, 9)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "C"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "MC"),
        };

        var file = License("NH");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("01234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Hampshire", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestTXLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ROBERTO"),
            LastName = FV<string?>(SubfileElementIds.LastName, "GONSALVES"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1254 FIRST"),
            City = FV<string?>(SubfileElementIds.City, "EL PASO"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "TX"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "79936"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1993, 10, 24)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 65)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.HairColor, HairColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "37110073"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 10, 25)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 10, 24)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("TX");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("79936", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Texas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestTX_DBDNONE_License()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ROBERTO"),
            LastName = FV<string?>(SubfileElementIds.LastName, "GONSALVES"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1254 FIRST"),
            City = FV<string?>(SubfileElementIds.City, "EL PASO"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "TX"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "79936"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1993, 10, 24)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 65)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.HairColor, HairColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "37110073"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, null),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 10, 24)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("TX_DBDNONE");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("79936", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Texas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOHN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "P"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "140 MAIN ST"),
            City = FV<string?>(SubfileElementIds.City, "PHILADELPHIA"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "PA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "19130"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1986, 2, 2)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 6, inches: 0)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),

            IdNumber = FV(SubfileElementIds.IdNumber, "26798765"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 1, 4)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 2, 3)),

            IsOrganDonor = FV<bool>(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "*/1"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "----"),
        };

        var file = License("PA");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("19130", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Pennsylvania", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPALicenseTwoMiddleNames()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOHN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ROBERT LEE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "140 MAIN ST"),
            City = FV<string?>(SubfileElementIds.City, "PHILADELPHIA"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "PA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "19130"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1986, 2, 2)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 6, inches: 0)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),

            IdNumber = FV(SubfileElementIds.IdNumber, "26798765"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 1, 4)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 2, 3)),

            IsOrganDonor = FV<bool>(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "*/1"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "----"),
        };

        var file = License("PA Two Middle Names");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("19130", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Pennsylvania", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPALicenseThreeMiddleNames()
    {
        var expected = new
        {
            FirstName = "JOHN",
            MiddleName = "ROBERT LEE JOHNSON",
            LastName = "SMITH"
        };

        var file = License("PA Three Middle Names");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        Assert.Equal(expected.FirstName, parseResult.Card.FirstName.Value);
        Assert.Equal(expected.MiddleName, parseResult.Card.MiddleName.Value);
        Assert.Equal(expected.LastName, parseResult.Card.LastName.Value);
        Assert.Null(parseResult.Card.Suffix.Value);
    }

    [Fact]
    public void TestPA2016License()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "CAPTAIN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "JACK"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MORGAN"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1725 SLOUGH AVE"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "APT 4"),
            City = FV<string?>(SubfileElementIds.City, "SCRANTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "PA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "18503"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1960, 5, 22)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 71)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),

            IdNumber = FV(SubfileElementIds.IdNumber, "25881776"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2016),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 11, 28)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 5, 23)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 6, 7)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "1"),
        };

        var file = License("PA 2016");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("18503", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Pennsylvania", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestRILicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "LOIS"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "PATRICE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "GRIFFIN"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "31 SPOONER ST"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "APT T2"),
            City = FV<string?>(SubfileElementIds.City, "QUAHOG"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "RI"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "000931760"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1988, 4, 21)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 66)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 170)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.HairColor, HairColor.Black),

            IdNumber = FV(SubfileElementIds.IdNumber, "30005037"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 10, 17)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 4, 21)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 1, 26)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "10"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "A"),
        };

        var file = License("RI");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("00093-1760", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Rhode Island", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNJLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MELISSA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "R"),
            LastName = FV<string?>(SubfileElementIds.LastName, "FOX"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1435 AUBURN AVE"),
            City = FV<string?>(SubfileElementIds.City, "VERNON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NJ"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "074182554"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1983, 2, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 62)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),

            IdNumber = FV(SubfileElementIds.IdNumber, "P62472647457903"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2015, 2, 28)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 2, 28)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2010, 7, 23)),

            HasTemporaryLawfulStatus = FV(SubfileElementIds.HasTemporaryLawfulStatus, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("NJ");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("07418-2554", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Jersey", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNJHZLEyesLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MELISSA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "R"),
            LastName = FV<string?>(SubfileElementIds.LastName, "FOX"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1435 AUBURN AVE"),
            City = FV<string?>(SubfileElementIds.City, "VERNON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NJ"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "074182554"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1983, 2, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 62)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),

            IdNumber = FV(SubfileElementIds.IdNumber, "P62472647457903"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2015, 2, 28)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 2, 28)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2010, 7, 23)),

            HasTemporaryLawfulStatus = FV(SubfileElementIds.HasTemporaryLawfulStatus, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("NJ HZL Eyes");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("07418-2554", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Jersey", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNCLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "RICK"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "SANTIAGO"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MORALES MARTIZ"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1440 BROWN TER"),
            City = FV<string?>(SubfileElementIds.City, "FAYETTEVILLE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NC"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "283041234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1986, 6, 12)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 69)),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.HairColor, HairColor.Black),

            IdNumber = FV(SubfileElementIds.IdNumber, "00004985690"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 11, 16)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2025, 6, 12)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2014, 10, 24)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("NC");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("28304-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("North Carolina", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestSCLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ROBINS"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "209 CEDAR HILL DR UNIT 12"),
            City = FV<string?>(SubfileElementIds.City, "SURFSIDE BEACH"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "SC"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "295754321"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1972, 2, 12)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 10)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 128)),

            IdNumber = FV(SubfileElementIds.IdNumber, "102639206"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2009, 6, 19)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 2, 12)),

            IsOrganDonor = FV<bool>(SubfileElementIds.IsOrganDonor, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "J"),
        };

        var file = License("SC");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("29575-4321", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("South Carolina", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMELicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "HARRY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "DRIVER"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "48 MAIN ST"),
            City = FV<string?>(SubfileElementIds.City, "BANGOR"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "ME"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "04401"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1947, 10, 2)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 69)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 175)),

            IdNumber = FV(SubfileElementIds.IdNumber, "2407225"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 9, 17)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 10, 2)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("ME");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("04401", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Maine", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestOHLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DEBBIE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "T"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "102 PARK AVE"),
            City = FV<string?>(SubfileElementIds.City, "NORTHWOOD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "OH"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "436191234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1956, 2, 23)),
            PlaceOfBirth = FV<string?>(SubfileElementIds.PlaceOfBirth, "US,OHIO"),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 60)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 140)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs131To160),

            IdNumber = FV(SubfileElementIds.IdNumber, "PJ842270"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 12, 2)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 2, 23)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2013, 12, 4)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.MateriallyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("OH");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("43619-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Ohio", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMILicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ROBERT"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1348 E MAPLE CT"),
            City = FV<string?>(SubfileElementIds.City, "ROCHESTER HILLS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MI"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "483064321"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1968, 3, 23)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),

            IdNumber = FV(SubfileElementIds.IdNumber, "L 341 567 071 342"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 3, 25)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 3, 25)),
        };

        var file = License("MI");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("48306-4321", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Michigan", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestONLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ANN"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 ST GEORGE ST E"),
            City = FV<string?>(SubfileElementIds.City, "FERGUS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "ON"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "N1M3J6"),
            Country = FV<Country>(SubfileElementIds.Country, Country.Canada),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1996, 6, 3)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(centimeters: 170)),

            IdNumber = FV(SubfileElementIds.IdNumber, "S9244-43879-65702"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 6, 7)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 6, 3)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "G"),
        };

        var file = License("ON");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("N1M 3J6", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Ontario", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestVTLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "BOBBY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "L"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TABLES"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "304 PARK ST APT 5"),
            City = FV<string?>(SubfileElementIds.City, "BENNINGTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "VT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "05201"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1978, 8, 9)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 67)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 195)),

            IdNumber = FV(SubfileElementIds.IdNumber, "92265728"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2012),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 8, 14)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 8, 9)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2013, 2, 20)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("VT");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("05201", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Vermont", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPRLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "LAURENCIA"),
            LastName = FV<string?>(SubfileElementIds.LastName, "ORTIZ ORTIZ"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "CAM CUBA LIBRE 800 KM"),
            City = FV<string?>(SubfileElementIds.City, "COROZAL"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "PR"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "00783"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1972, 3, 6)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 62)),

            IdNumber = FV(SubfileElementIds.IdNumber, "4696735"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2010),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 3, 3)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 3, 6)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "3"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "7"),
        };

        var file = License("PR");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("00783", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Puerto Rico", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMDLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DIANA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ROSE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12 DOGWOOD CT APT B"),
            City = FV<string?>(SubfileElementIds.City, "BALTIMORE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MD"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "21201"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1992, 10, 10)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 66)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 170)),

            IdNumber = FV(SubfileElementIds.IdNumber, "S-512-887-236-780"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 6, 10)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2025, 10, 10)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 6, 20)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("MD");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("21201", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Maryland", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ELIJAH"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MASON"),
            LastName = FV<string?>(SubfileElementIds.LastName, "HARPER"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "671 BLUEBERRY HILL DR"),
            City = FV<string?>(SubfileElementIds.City, "MILPITAS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "95035"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1973, 7, 5)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 68)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 165)),

            IdNumber = FV(SubfileElementIds.IdNumber, "F1485768"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 2, 2)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 7, 5)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2010, 4, 16)),

            HasTemporaryLawfulStatus = FV(SubfileElementIds.HasTemporaryLawfulStatus, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("CA");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("95035", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("California", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNMLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "LUIS"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SINCLAIR-ESCUEVA"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1675 W 54TH ST"),
            City = FV<string?>(SubfileElementIds.City, "LOS ALAMOS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NM"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "87544"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1981, 10, 27)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),

            IdNumber = FV(SubfileElementIds.IdNumber, "513577879"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 8, 22)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 11, 27)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("NM");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("87544", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Mexico", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestUTLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARIE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "RAYE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "CALENDAR"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "200 E 1900 N"),
            City = FV<string?>(SubfileElementIds.City, "LEHI"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "UT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "84043"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1981, 8, 14)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 64)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 205)),

            IdNumber = FV(SubfileElementIds.IdNumber, "0163375279"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2012),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 8, 14)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 8, 14)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2013, 1, 1)),

            Under18Until = FV<DateTime?>(SubfileElementIds.Under18Until, new DateTime(1999, 8, 14)),
            Under19Until = FV<DateTime?>(SubfileElementIds.Under19Until, new DateTime(2000, 8, 14)),
            Under21Until = FV<DateTime?>(SubfileElementIds.Under21Until, new DateTime(2002, 8, 14)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "A"),
        };

        var file = License("UT");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("84043", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Utah", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestIALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARK"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 ANY MAIN ST"),
            City = FV<string?>(SubfileElementIds.City, "RED OAK"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "IA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "51566"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1991, 7, 11)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),

            IdNumber = FV(SubfileElementIds.IdNumber, "109BB2608"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 10, 16)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 7, 11)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2011, 7, 25)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "L"),
        };

        var file = License("IA");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("51566", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Iowa", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestORLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "JONES"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "4455 SE 25TH ST"),
            City = FV<string?>(SubfileElementIds.City, "CORVALLIS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "OR"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "97330"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1950, 6, 26)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 2)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 185)),

            IdNumber = FV(SubfileElementIds.IdNumber, "4066452"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 6, 24)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2024, 6, 26)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "BD"),
        };

        var file = License("OR");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("97330", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Oregon", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestLALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARCIA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "JONES"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1234 HWY 57"),
            City = FV<string?>(SubfileElementIds.City, "EROS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "LA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "71238"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1974, 7, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 2)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 220)),

            IdNumber = FV(SubfileElementIds.IdNumber, "005799564"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 5, 20)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 7, 7)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "E"),
        };

        var file = License("LA");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("71238", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Louisiana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestKYLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ANN"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 WISTERIA LN 23"),
            City = FV<string?>(SubfileElementIds.City, "LOUISVILLE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "KY"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "40218"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1954, 11, 12)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 65)),

            IdNumber = FV(SubfileElementIds.IdNumber, "K12340057"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2010),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 11, 22)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 12, 13)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2012, 3, 16)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "1"),
        };

        var file = License("KY");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("40218", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Kentucky", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestWILicense()
    {
        // Wisconsin defines a subfile in the header but we don't follow it
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOEY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "M"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "N1234 PINEWOOD RD"),
            City = FV<string?>(SubfileElementIds.City, "CHEESY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "WI"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "54767"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1983, 8, 15)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),

            IdNumber = FV(SubfileElementIds.IdNumber, "M2861738629325"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2010),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 4, 11)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 8, 15)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2012, 3, 16)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "ABCD"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "N"),
        };

        var file = License("WI");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("54767", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Wisconsin", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestDELicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "7895 CHERRYBLOSSOM HL"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "APT @ CRAWFORD INN"),
            City = FV<string?>(SubfileElementIds.City, "NEWARK"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "DE"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "197521234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1989, 9, 9)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 71)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 130)),

            IdNumber = FV(SubfileElementIds.IdNumber, "1824873"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 10, 27)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 1, 9)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2010, 2, 13)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.MateriallyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("DE");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("19752-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Delaware", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCOLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "CODY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "909 COUNTRY ROAD 206"),
            City = FV<string?>(SubfileElementIds.City, "BOULDER"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CO"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "81635"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1992, 7, 13)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 73)),

            IdNumber = FV(SubfileElementIds.IdNumber, "102367033"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2012),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 8, 8)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 7, 13)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2013, 6, 1)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.MateriallyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "R"),
        };

        var file = License("CO");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("81635", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Colorado", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCO2013License()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JANE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "LYNN"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),
            Suffix = FV<string?>(SubfileElementIds.NameSuffix, "SR"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "98765 W 23RD AVE"),
            City = FV<string?>(SubfileElementIds.City, "LAKEWOOD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CO"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "80401"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1972, 2, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 63)),

            IdNumber = FV(SubfileElementIds.IdNumber, "124336019"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 12, 27)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 1, 4)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2015, 10, 30)),

            HasTemporaryLawfulStatus = FV(SubfileElementIds.HasTemporaryLawfulStatus, false),
            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "16455534969"),
            AuditInformation = FV<string?>(SubfileElementIds.AuditInformation, "20170104_000227_9_3776"),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "R"),
            AdditionalJurisdictionElements =
            {
                { "ZCZ", FV<string?>("ZCZ", "CANONE") }
            }
        };

        var file = License("CO 2013");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("80401", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Colorado", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestALLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 COUNTY DR"),
            City = FV<string?>(SubfileElementIds.City, "BLUE RIDGE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "AL"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "360931234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1967, 3, 27)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 70)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs191To220),

            IdNumber = FV(SubfileElementIds.IdNumber, "5677922"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 11, 26)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 11, 18)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2009, 11, 6)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "DMV"),
        };

        var file = License("AL");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("36093-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Alabama", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestAZLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "SUSAN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "T"),
            LastName = FV<string?>(SubfileElementIds.LastName, "WILLIAMS"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "5123 WACO DR"),
            City = FV<string?>(SubfileElementIds.City, "TUSCON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "AZ"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "856414321"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1989, 1, 24)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 5)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 160)),

            IdNumber = FV(SubfileElementIds.IdNumber, "D04852767"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 6, 4)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2054, 1, 24)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("AZ");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("85641-4321", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Arizona", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestARLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "RALPH"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "321 MAIN ST"),
            City = FV<string?>(SubfileElementIds.City, "HOT SPRINGS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "AR"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "719014455"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1946, 11, 22)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Ethnicity = FV<Ethnicity?>(SubfileElementIds.Ethnicity, Ethnicity.White),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 70)),

            IdNumber = FV(SubfileElementIds.IdNumber, "9298847972"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2010),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 9, 13)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2024, 11, 22)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2012, 9, 15)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            HasTemporaryLawfulStatus = FV(SubfileElementIds.HasTemporaryLawfulStatus, false),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("AR");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("71901-4455", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Arkansas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestWALicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "S"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "16255 PEWDER CT SE"),
            City = FV<string?>(SubfileElementIds.City, "REDMOND"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "WA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "980081234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1950, 5, 23)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 61)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs131To160),

            IdNumber = FV(SubfileElementIds.IdNumber, "TESTEDM504K9"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2015, 4, 16)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 5, 23)),
        };

        var file = License("WA");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("98008-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Washington", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMTLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ROSE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1254 MAGNOLIA AVE"),
            City = FV<string?>(SubfileElementIds.City, "HELENA"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "59601"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1994, 5, 14)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 67)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs131To160),

            IdNumber = FV(SubfileElementIds.IdNumber, "0504928899117"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2015, 7, 2)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 5, 14)),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("MT");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("59601", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Montana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestKSLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOEY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "SMITH"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12345 S 110TH TER"),
            City = FV<string?>(SubfileElementIds.City, "OVERLAND PARK"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "KS"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "66210"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1980, 1, 26)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 71)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "K04-76-5990"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2016),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 11, 29)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 1, 26)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2017, 2, 26)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("KS");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("66210", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Kansas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestINLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "RYAN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MICHAEL"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12345 W HENCHMEN CIR"),
            City = FV<string?>(SubfileElementIds.City, "ANYCITY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "IN"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "47458"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1993, 2, 25)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Hazel),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Blond),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 69)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 245)),

            IdNumber = FV(SubfileElementIds.IdNumber, "3249-09-7547"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 8, 3)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 2, 25)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2009, 9, 21)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
        };

        var file = License("IN");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("47458", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Indiana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestILLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "SUSAN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "T"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 LAKE SHORE DR APT"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "6431"),
            City = FV<string?>(SubfileElementIds.City, "CHICAGO"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "IL"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "60611"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1969, 6, 27)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 68)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 200)),

            IdNumber = FV(SubfileElementIds.IdNumber, "W63177069784"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 4, 13)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 6, 27)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2015, 9, 17)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("IL");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("60611", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Illinois", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestHILicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "JAY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "456 MOANA ST 2"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "O"),
            City = FV<string?>(SubfileElementIds.City, "HONOLULU"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "HI"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "96826"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1988, 3, 12)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Blond),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 72)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "H01387330"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 5, 13)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2024, 3, 12)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "3"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = License("HI");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("96826", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Hawaii", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestWVLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "JOE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "BOB"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "518   S RANDOM STREET"),
            City = FV<string?>(SubfileElementIds.City, "ANYTOWN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "WV"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "12345"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1972, 11, 3)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 71)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 190)),

            IdNumber = FV(SubfileElementIds.IdNumber, "F123456"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 10, 1)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 11, 3)),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "E"),
        };

        var file = License("WV");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("12345", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("West Virginia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestAKLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "JOE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12345 E MAIN HY"),
            City = FV<string?>(SubfileElementIds.City, "ANCHORAGE"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "AK"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "99645"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1955, 4, 2)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Gray),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 64)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 160)),

            IdNumber = FV(SubfileElementIds.IdNumber, "7559886"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 3, 22)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 4, 2)),
            Under21Until = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(1976, 4, 2)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            IsVeteran = FV(SubfileElementIds.IsVeteran, false),
            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "2881111"),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "1"),
        };

        var file = License("AK");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("99645", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Alaska", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestDCLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DIANA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "ROBIN"),
            LastName = FV<string?>(SubfileElementIds.LastName, "AL-MAAR"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1234 14TH ST SW 1A"),
            City = FV<string?>(SubfileElementIds.City, "WASHINGTON"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "DC"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "200091234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1985, 7, 29)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 6)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 140)),

            IdNumber = FV(SubfileElementIds.IdNumber, "3234567"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2013, 7, 30)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 7, 29)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "2881111"),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("DC");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("20009-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("District of Columbia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPELicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "PATTY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "FLOWERS"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 NORTH LAKE SHORE DR"),
            City = FV<string?>(SubfileElementIds.City, "ANYTOWN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "PE"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "C0A2B4"),
            Country = FV<Country>(SubfileElementIds.Country, Country.Canada),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1955, 9, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(centimeters: 157)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "247725"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2016),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 12, 22)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 9, 4)),

            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "PE2017122200000019550904"),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "5"),
        };

        var file = License("PE");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("C0A 2B4", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Price Edward Island", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNVLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "M"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "6543 ADORAMA DR"),
            City = FV<string?>(SubfileElementIds.City, "NORTH LAS VEGAS"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NV"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "890311234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1976, 1, 15)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 68)),
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs191To220),

            IdNumber = FV(SubfileElementIds.IdNumber, "0003456789"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 3, 1)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2025, 1, 15)),

            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "000123456789098765432"),
            InventoryControlNumber = FV<string?>(SubfileElementIds.InventoryControlNumber, "0012345678900"),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),

            AdditionalJurisdictionElements =
            {
                { "ZNZ", FV<string?>("ZNZ", "NAY") },
                { "ZNB", FV<string?>("ZNB", "10102008") },
                { "ZNC", FV<string?>("ZNC", "5?08??") },
                { "ZND", FV<string?>("ZND", "210") },
                { "ZNE", FV<string?>("ZNE", "NCDL") },
                { "ZNF", FV<string?>("ZNZ", "NCDL") },
                { "ZNG", FV<string?>("ZNG", "S") },
                { "ZNH", FV<string?>("ZNH", "00123456780") },
                { "ZNI", FV<string?>("ZNI", "00000001234") }
            }
        };

        var file = License("NV");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("89031-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Nevada", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNDLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "DOE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 W HIGHWAY AVE"),
            City = FV<string?>(SubfileElementIds.City, "ANYCITY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "ND"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "58503"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1985, 7, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 74)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "RUN812345"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 10, 26)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 7, 4)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2014, 1, 8)),

            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "8RUN812345RC22110GA75NDZ"),
            InventoryControlNumber = FV<string?>(SubfileElementIds.InventoryControlNumber, "0123456789098765"),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "DM"),

            AdditionalJurisdictionElements =
            {
                { "ZNZ", FV<string?>("ZNZ", "NA704") },
                { "ZNB", FV<string?>("ZNB", "1") },
            }
        };

        var file = License("ND");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("58503", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("North Dakota", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicenseUndefinedCharacters()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "WENDY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "SMITH"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12 SACAGAWEA DR"),
            City = FV<string?>(SubfileElementIds.City, "WEST HARTFORD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "061171234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1966, 1, 26)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 67)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "123456780"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2016),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2019, 1, 8)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2026, 1, 26)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2017, 2, 10)),

            DocumentDiscriminator = FV<string?>(SubfileElementIds.DocumentDiscriminator, "12345678909870MVK3"),
            InventoryControlNumber = FV<string?>(SubfileElementIds.InventoryControlNumber, "123456780CTRBTL02"),

            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.FullyCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),

            AdditionalJurisdictionElements =
            {
                { "ZCZ", FV<string?>("ZNZ", "CA") },
                { "ZCB", FV<string?>("ZNB", "0005276677") },
            }
        };

        var file = License("CT Undefined Characters");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("06117-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestABLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "SMITH"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 MAPLE LEAF TERR SW"),
            City = FV<string?>(SubfileElementIds.City, "CALGARY"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "AB"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "T4G7A7"),
            Country = FV<Country>(SubfileElementIds.Country, Country.Canada),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1993, 2, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(centimeters: 155)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(kilograms: 50)),
            // Alberta specifies the weight range following the weight in kilograms
            WeightRange = FV<WeightRange?>(SubfileElementIds.WeightRange, WeightRange.Lbs101To130),

            IdNumber = FV(SubfileElementIds.IdNumber, "123400-056"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2005),

            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 1, 8)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "5"),
            EndorsementCodes = FV<string?>(SubfileElementIds.EndorsementCodes, "A"),
        };

        var file = License("AB");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("T4G 7A7", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Alberta", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMNLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DALE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "THOR"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SPARKS"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12345 MAIN ST"),
            StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "UNIT 91"),
            City = FV<string?>(SubfileElementIds.City, "AITKIN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MN"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "564311234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1995, 1, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 70)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 138)),

            IdNumber = FV(SubfileElementIds.IdNumber, "H868087743210"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2016),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2018, 12, 22)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2020, 1, 4)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2017, 10, 23)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            ComplianceType = FV<ComplianceType?>(SubfileElementIds.ComplianceType, ComplianceType.NonCompliant),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "2"),

            AdditionalJurisdictionElements =
            {
                { "ZMZ", FV<string?>("ZMZ", "MAN") },
                { "ZMB", FV<string?>("ZMB", "N") },
            }
        };

        var file = License("MN");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("56431-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Minnesota", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    }

    [Fact]
    public void TestMSLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "PATRICK"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 BUCK RUN"),
            City = FV<string?>(SubfileElementIds.City, "HATTIESBURG"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MS"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "39402"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1963, 5, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 70)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "800448123"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2013),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2018, 5, 10)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 5, 7)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2016, 2, 22)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "R"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "BF"),

            AdditionalJurisdictionElements =
            {
                { "ZMZ", FV<string?>("ZMZ", "MAN") },
                { "ZMB", FV<string?>("ZMB", "N") },
                { "ZMC", FV<string?>("ZMC", "N") },
                { "ZMD", FV<string?>("ZMD", "123 BUCK RUN") },
                { "ZME", FV<string?>("ZME", "HATTIESBURG") },
                { "ZMF", FV<string?>("ZMZ", "MS") },
                { "ZMG", FV<string?>("ZMG", "394020000") },
            }
        };

        var file = License("MS");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("39402", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Mississippi", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    }

    [Fact]
    public void TestIDLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "CLAY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "MOTORIST"),
            LastName = FV<string?>(SubfileElementIds.LastName, "JENSEN"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1234 MAIN STREET"),
            City = FV<string?>(SubfileElementIds.City, "GRANGEVILL"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "ID"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "83530"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1967, 12, 8)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Blond),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 73)),
            Weight = FV<Weight?>(SubfileElementIds.WeightInPounds, new Weight(pounds: 240)),

            IdNumber = FV(SubfileElementIds.IdNumber, "WA104577G"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2010),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2011, 11, 20)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2019, 12, 8)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2011, 5, 9)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),

            AdditionalJurisdictionElements =
            {
                { "ZIZ", FV<string?>("ZIZ", "IADONOR") },
                { "ZIB", FV<string?>("ZIB", "") },
                { "ZIC", FV<string?>("ZIC", "") },
            }
        };

        var file = License("ID");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("83530", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Idaho", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    }

    [Fact]
    public void TestLeadingWhitespaceLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MOTORIST"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "R"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SHEEHAN"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasMiddleNameTruncated, false),
            WasLastNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "2 ROBERTS DRIVE"),
            City = FV<string?>(SubfileElementIds.City, "PLYMOUTH"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "MA"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "023601234"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1939, 12, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 71)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "S58239477"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2014, 11, 14)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2018, 12, 7)),
            RevisionDate = FV<DateTime?>(SubfileElementIds.RevisionDate, new DateTime(2009, 7, 15)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "DM"),
            RestrictionCodes = FV<string?>(SubfileElementIds.RestrictionCodes, "B"),
        };

        var file = File.ReadAllText("Leading Whitespace.txt");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("02360-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestInvalidHeader()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MICHAEL"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "G"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "12 MAIN AVE"),
            City = FV<string?>(SubfileElementIds.City, "WEST HAVEN"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "06516"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1961, 2, 4)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Brown),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 5, inches: 4)),

            IdNumber = FV(SubfileElementIds.IdNumber, "025995434"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 11, 14)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2023, 2, 4)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = File.ReadAllText("Invalid Header.txt");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("06516", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicenseSuffix()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "PABLO"),
            LastName = FV<string?>(SubfileElementIds.LastName, "CORTEZ"),
            Suffix = FV<string?>(SubfileElementIds.NameSuffix, "JR"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "715 MAIN LN"),
            City = FV<string?>(SubfileElementIds.City, "STRATFORD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "066140123"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1976, 10, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 6, inches: 0)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "227881513"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 8, 23)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 10, 7)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("CT Suffix");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("06614-0123", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicenseMultipleMiddleNames()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "PABLO"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "LUIS RODRIGUEZ"),
            LastName = FV<string?>(SubfileElementIds.LastName, "CORTEZ"),
            Suffix = FV<string?>(SubfileElementIds.NameSuffix, "JR"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "715 MAIN LN"),
            City = FV<string?>(SubfileElementIds.City, "STRATFORD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "CT"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "066140123"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1976, 10, 7)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Male),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Green),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(feet: 6, inches: 0)),

            IdNumber = FV(SubfileElementIds.IdNumber, "227881513"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2000),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2016, 8, 23)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2022, 10, 7)),

            IsOrganDonor = FV(SubfileElementIds.IsOrganDonor, true),
            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "D"),
        };

        var file = License("CT Multiple Middle Names");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("06614-0123", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNBLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MARY"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "M"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MOTORIST"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "123 EAGLEHEAD DR"),
            City = FV<string?>(SubfileElementIds.City, "GRND-BAY-WFLD"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "NB"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "E5K1Y3"),
            Country = FV<Country>(SubfileElementIds.Country, Country.Canada),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1962, 8, 8)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(centimeters: 168)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "1234567"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2003),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 8, 12)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 8, 8)),
        };

        var file = License("NB");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("E5K 1Y3", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Brunswick", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestWYLicense()
    {
        var expected = new DriversLicense
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MOTORIST"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "E"),
            LastName = FV<string?>(SubfileElementIds.LastName, "O NEIL"),

            StreetLine1 = FV<string?>(SubfileElementIds.StreetLine1, "1234 MAIN WAY"),
            //StreetLine2 = FV<string?>(SubfileElementIds.StreetLine2, "BLUE STREAM, WY  82930"), // TODO: Check if this is the same as city, state, zip and remove if so
            City = FV<string?>(SubfileElementIds.City, "BLUE STREAM"),
            JurisdictionCode = FV<string?>(SubfileElementIds.JurisdictionCode, "WY"),
            PostalCode = FV<string?>(SubfileElementIds.PostalCode, "82930"),
            Country = FV<Country>(SubfileElementIds.Country, Country.USA),

            DateOfBirth = FV<DateTime?>(SubfileElementIds.DateOfBirth, new DateTime(1958, 10, 31)),
            Sex = FV<Sex?>(SubfileElementIds.Sex, Sex.Female),
            EyeColor = FV<EyeColor?>(SubfileElementIds.EyeColor, EyeColor.Blue),
            HairColor = FV<HairColor?>(SubfileElementIds.EyeColor, HairColor.Blond),
            Height = FV<Height?>(SubfileElementIds.Height, new Height(totalInches: 69)),
            
            IdNumber = FV(SubfileElementIds.IdNumber, "123456-789"),
            AAMVAVersionNumber = FV(null, AAMVAVersion.AAMVA2009),

            IssueDate = FV<DateTime?>(SubfileElementIds.IssueDate, new DateTime(2017, 10, 11)),
            ExpirationDate = FV<DateTime?>(SubfileElementIds.ExpirationDate, new DateTime(2021, 10, 31)),

            VehicleClass = FV<string?>(SubfileElementIds.VehicleClass, "C"),
        };

        var file = License("WY");
        var parseResult = Barcode.Parse(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("82930", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Wyoming", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }
}
