using IdParser.Core.Constants;
using Xunit;
using Xunit.Abstractions;

namespace IdParser.Core.Test;

public class DriversLicenseTests2 : BaseTest2
{
    public DriversLicenseTests2(ITestOutputHelper output)
        : base(output)
    {
    }

    private Field<T> FV<T>(string? elementId, T value)
        => FieldHelpers.ParsedField(elementId, value, rawValue: null);


    [Fact]
    public void TestMA2009License()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "ROBERT"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "LOWNEY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SMITH"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMA2016License()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MORRIS"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "T"),
            LastName = FV<string?>(SubfileElementIds.LastName, "SAMPLE"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
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
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "TONY"),
            LastName = FV<string?>(SubfileElementIds.LastName, "ROBERT"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNYLicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "M"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "Motorist"),
            LastName = FV<string?>(SubfileElementIds.LastName, "Michael"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("New York", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestVALicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Virginia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.IsType<DriversLicense2>(parseResult.Card);

        if (parseResult.Card is DriversLicense2 license)
        {
            Assert.Equal("158X9", license.RestrictionCodes.Value);
        }
    }

    [Fact]
    public void TestGALicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Georgia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.IsType<DriversLicense2>(parseResult.Card);

        if (parseResult.Card is DriversLicense2 license)
        {
            Assert.Equal("D", license.VehicleClass.Value);
            Assert.Equal("B", license.RestrictionCodes.Value);
        }
    }

    [Fact]
    public void TestCTLicenseWebBrowser()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestCTLicenseNoMiddleName()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestMOLicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("Missouri", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal("MAST LOUIS CITY", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMZ").Value.Value);
        Assert.Equal("112001810097", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZMB").Value.Value);

        Assert.IsType<DriversLicense2>(parseResult.Card);

        if (parseResult.Card is DriversLicense2 license)
        {
            Assert.Equal("F", license.VehicleClass.Value);
        }
    }

    [Fact]
    public void TestFLLicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("33040-0504", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Florida", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

        Assert.Equal(5, parseResult.Card.AdditionalJurisdictionElements.Count);
        Assert.Equal("FA", parseResult.Card.AdditionalJurisdictionElements.Single(e => e.Key == "ZFZ").Value.Value);

        if (parseResult.Card is DriversLicense2 license)
        {
            Assert.Equal("A", license.RestrictionCodes.Value);
            Assert.Equal("E", license.VehicleClass.Value);
        }
    }

    [Fact]
    public void TestNHLicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "DONNIE"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "G"),
            LastName = FV<string?>(SubfileElementIds.LastName, "TESTER"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("01234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Hampshire", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestTXLicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("79936", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Texas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPALicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("19130", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Pennsylvania", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestPALicenseTwoMiddleNames()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        Assert.Equal(expected.FirstName, parseResult.Card.FirstName.Value);
        Assert.Equal(expected.MiddleName, parseResult.Card.MiddleName.Value);
        Assert.Equal(expected.LastName, parseResult.Card.LastName.Value);
        Assert.Null(parseResult.Card.Suffix.Value);
    }

    [Fact]
    public void TestPA2016License()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "CAPTAIN"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "JACK"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MORGAN"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("18503", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Pennsylvania", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestRILicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "LOIS"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "PATRICE"),
            LastName = FV<string?>(SubfileElementIds.LastName, "GRIFFIN"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("00093-1760", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("Rhode Island", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNJLicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MELISSA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "R"),
            LastName = FV<string?>(SubfileElementIds.LastName, "FOX"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("07418-2554", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Jersey", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNJHZLEyesLicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "MELISSA"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "R"),
            LastName = FV<string?>(SubfileElementIds.LastName, "FOX"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("07418-2554", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("New Jersey", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestNCLicense()
    {
        var expected = new DriversLicense2
        {
            FirstName = FV<string?>(SubfileElementIds.FirstName, "RICK"),
            MiddleName = FV<string?>(SubfileElementIds.MiddleName, "SANTIAGO"),
            LastName = FV<string?>(SubfileElementIds.LastName, "MORALES MARTIZ"),

            WasFirstNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
            WasMiddleNameTruncated = FV<bool?>(SubfileElementIds.WasFirstNameTruncated, false),
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("28304-1234", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("North Carolina", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    [Fact]
    public void TestSCLicense()
    {
        var expected = new DriversLicense2
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
        var parseResult = Barcode.Parse2(file, Validation.None);
        LogUnhandledElementIds(parseResult.Card);

        AssertIdCard(expected, parseResult.Card);
        AssertLicense(expected, parseResult.Card);

        Assert.Equal("29575-4321", parseResult.Card.PostalCodeDisplay);
        Assert.Equal("South Carolina", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    }

    //[Fact]
    //public void TestMELicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "HARRY",
    //            Last = "DRIVER",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "48 MAIN ST",
    //            City = "BANGOR",
    //            JurisdictionCode = "ME",
    //            PostalCode = "04401",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1947, 10, 02),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 69),
    //        Weight = new Weight(pounds: 175),

    //        IdNumber = "2407225",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2017, 09, 17),
    //        ExpirationDate = new DateTime(2021, 10, 02),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("ME");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("04401", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Maine", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestOHLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "DEBBIE",
    //            Middle = "T",
    //            Last = "MOTORIST",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "102 PARK AVE",
    //            City = "NORTHWOOD",
    //            JurisdictionCode = "OH",
    //            PostalCode = "436191234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1956, 02, 23),
    //        PlaceOfBirth = "US,OHIO",
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Brown,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(totalInches: 60),
    //        Weight = new Weight(pounds: 140),
    //        WeightRange = WeightRange.Lbs131To160,

    //        IdNumber = "PJ842270",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2016, 12, 02),
    //        ExpirationDate = new DateTime(2020, 02, 23),
    //        RevisionDate = new DateTime(2013, 12, 04),

    //        ComplianceType = ComplianceType.MateriallyCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("OH");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("43619-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Ohio", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestMILicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "ROBERT",
    //            Last = "SMITH"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1348 E MAPLE CT",
    //            City = "ROCHESTER HILLS",
    //            JurisdictionCode = "MI",
    //            PostalCode = "483064321",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1968, 03, 23),
    //        Sex = Sex.Male,

    //        IdNumber = "L 341 567 071 342",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2016, 03, 25),
    //        ExpirationDate = new DateTime(2020, 03, 25)
    //    };

    //    var file = License("MI");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("48306-4321", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Michigan", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestONLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "ANN",
    //            Last = "TESTER"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 ST GEORGE ST E",
    //            City = "FERGUS",
    //            JurisdictionCode = "ON",
    //            PostalCode = "N1M3J6",
    //            Country = Country.Canada
    //        },

    //        DateOfBirth = new DateTime(1996, 06, 03),
    //        Sex = Sex.Female,
    //        Height = new Height(centimeters: 170),

    //        IdNumber = "S9244-43879-65702",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2017, 06, 07),
    //        ExpirationDate = new DateTime(2020, 06, 03),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "G"
    //        }
    //    };

    //    var file = License("ON");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("N1M 3J6", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Ontario", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestVTLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "BOBBY",
    //            Middle = "L",
    //            Last = "TABLES",

    //            WasFirstTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "304 PARK ST APT 5",
    //            City = "BENNINGTON",
    //            JurisdictionCode = "VT",
    //            PostalCode = "05201",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1978, 08, 09),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 67),
    //        Weight = new Weight(pounds: 195),

    //        IdNumber = "92265728",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2012,

    //        IssueDate = new DateTime(2016, 08, 14),
    //        ExpirationDate = new DateTime(2018, 08, 09),
    //        RevisionDate = new DateTime(2013, 02, 20),

    //        IsOrganDonor = true,
    //        ComplianceType = ComplianceType.FullyCompliant,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("VT");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("05201", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Vermont", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestPRLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "LAURENCIA",
    //            Last = "ORTIZ ORTIZ",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "CAM CUBA LIBRE 800 KM",
    //            City = "COROZAL",
    //            JurisdictionCode = "PR",
    //            PostalCode = "00783",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1972, 03, 06),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 62),

    //        IdNumber = "4696735",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2010,

    //        IssueDate = new DateTime(2017, 03, 03),
    //        ExpirationDate = new DateTime(2023, 03, 06),

    //        ComplianceType = ComplianceType.NonCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "3",
    //            RestrictionCodes = "7"
    //        }
    //    };

    //    var file = License("PR");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("00783", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Puerto Rico", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestMDLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "DIANA",
    //            Middle = "ROSE",
    //            Last = "SMITH",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12 DOGWOOD CT APT B",
    //            City = "BALTIMORE",
    //            JurisdictionCode = "MD",
    //            PostalCode = "21201",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1992, 10, 10),
    //        Sex = Sex.Female,
    //        Height = new Height(totalInches: 66),
    //        Weight = new Weight(pounds: 170),

    //        IdNumber = "S-512-887-236-780",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2017, 06, 10),
    //        ExpirationDate = new DateTime(2025, 10, 10),
    //        RevisionDate = new DateTime(2016, 06, 20),

    //        ComplianceType = ComplianceType.FullyCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("MD");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("21201", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Maryland", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCALicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "ELIJAH",
    //            Middle = "MASON",
    //            Last = "HARPER"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "671 BLUEBERRY HILL DR",
    //            City = "MILPITAS",
    //            JurisdictionCode = "CA",
    //            PostalCode = "95035",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1973, 07, 05),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(totalInches: 68),
    //        Weight = new Weight(pounds: 165),

    //        IdNumber = "F1485768",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2016, 02, 02),
    //        ExpirationDate = new DateTime(2019, 07, 05),
    //        RevisionDate = new DateTime(2010, 04, 16),

    //        HasTemporaryLawfulStatus = false,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C"
    //        }
    //    };

    //    var file = License("CA");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("95035", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("California", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestNMLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "LUIS",
    //            Last = "SINCLAIR-ESCUEVA"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1675 W 54TH ST",
    //            City = "LOS ALAMOS",
    //            JurisdictionCode = "NM",
    //            PostalCode = "87544",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1981, 10, 27),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 72),

    //        IdNumber = "513577879",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2013, 08, 22),
    //        ExpirationDate = new DateTime(2021, 11, 27),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("NM");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("87544", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("New Mexico", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestUTLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARIE",
    //            Middle = "RAYE",
    //            Last = "CALENDAR"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "200 E 1900 N",
    //            City = "LEHI",
    //            JurisdictionCode = "UT",
    //            PostalCode = "84043",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1981, 08, 14),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Green,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(totalInches: 64),
    //        Weight = new Weight(pounds: 205),

    //        IdNumber = "0163375279",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2012,

    //        IssueDate = new DateTime(2013, 08, 14),
    //        ExpirationDate = new DateTime(2018, 08, 14),
    //        RevisionDate = new DateTime(2013, 01, 01),

    //        Under18Until = new DateTime(1999, 08, 14),
    //        Under19Until = new DateTime(2000, 08, 14),
    //        Under21Until = new DateTime(2002, 08, 14),

    //        IsOrganDonor = true,
    //        ComplianceType = ComplianceType.FullyCompliant,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "A"
    //        }
    //    };

    //    var file = License("UT");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("84043", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Utah", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestIALicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARK",
    //            Middle = "MOTORIST",
    //            Last = "SMITH",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 ANY MAIN ST",
    //            City = "RED OAK",
    //            JurisdictionCode = "IA",
    //            PostalCode = "51566",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1991, 07, 11),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 72),

    //        IdNumber = "109BB2608",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2013, 10, 16),
    //        ExpirationDate = new DateTime(2020, 07, 11),
    //        RevisionDate = new DateTime(2011, 07, 25),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C",
    //            EndorsementCodes = "L"
    //        }
    //    };

    //    var file = License("IA");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("51566", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Iowa", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestORLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "JONES",
    //            Last = "SMITH"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "4455 SE 25TH ST",
    //            City = "CORVALLIS",
    //            JurisdictionCode = "OR",
    //            PostalCode = "97330",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1950, 06, 26),
    //        Sex = Sex.Female,
    //        Height = new Height(feet: 5, inches: 2),
    //        Weight = new Weight(pounds: 185),

    //        IdNumber = "4066452",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2016, 06, 24),
    //        ExpirationDate = new DateTime(2024, 06, 26),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C",
    //            RestrictionCodes = "BD"
    //        }
    //    };

    //    var file = License("OR");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("97330", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Oregon", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestLALicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARCIA",
    //            Middle = "MOTORIST",
    //            Last = "JONES"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1234 HWY 57",
    //            City = "EROS",
    //            JurisdictionCode = "LA",
    //            PostalCode = "71238",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1974, 07, 07),
    //        Sex = Sex.Female,
    //        Height = new Height(feet: 5, inches: 2),
    //        Weight = new Weight(pounds: 220),

    //        IdNumber = "005799564",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2014, 05, 20),
    //        ExpirationDate = new DateTime(2018, 07, 07),

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "E"
    //        }
    //    };

    //    var file = License("LA");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("71238", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Louisiana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestKYLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "ANN",
    //            Last = "SMITH",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 WISTERIA LN 23",
    //            City = "LOUISVILLE",
    //            JurisdictionCode = "KY",
    //            PostalCode = "40218",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1954, 11, 12),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Hazel,
    //        Height = new Height(totalInches: 65),

    //        IdNumber = "K12340057",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2010,

    //        IssueDate = new DateTime(2017, 11, 22),
    //        ExpirationDate = new DateTime(2021, 12, 13),
    //        RevisionDate = new DateTime(2012, 03, 16),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "1"
    //        }
    //    };

    //    var file = License("KY");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("40218", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Kentucky", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestWILicense()
    //{
    //    // Wisconsin defines a subfile in the header but we don't follow it
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "JOEY",
    //            Middle = "M",
    //            Last = "TESTER",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "N1234 PINEWOOD RD",
    //            City = "CHEESY",
    //            JurisdictionCode = "WI",
    //            PostalCode = "54767",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1983, 08, 15),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 72),

    //        IdNumber = "M2861738629325",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2010,

    //        IssueDate = new DateTime(2013, 04, 11),
    //        ExpirationDate = new DateTime(2018, 08, 15),
    //        RevisionDate = new DateTime(2012, 03, 16),

    //        ComplianceType = ComplianceType.NonCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "ABCD",
    //            RestrictionCodes = "B",
    //            EndorsementCodes = "N"
    //        }
    //    };

    //    var file = License("WI");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("54767", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Wisconsin", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestDELicense()
    //{
    //    // Wisconsin defines a subfile in the header but we don't follow it
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MOTORIST",
    //            Last = "TESTER",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "7895 CHERRYBLOSSOM HL",
    //            StreetLine2 = "APT @ CRAWFORD INN",
    //            City = "NEWARK",
    //            JurisdictionCode = "DE",
    //            PostalCode = "197521234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1989, 09, 09),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 71),
    //        Weight = new Weight(pounds: 130),

    //        IdNumber = "1824873",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2017, 10, 27),
    //        ExpirationDate = new DateTime(2019, 01, 09),
    //        RevisionDate = new DateTime(2010, 02, 13),

    //        ComplianceType = ComplianceType.MateriallyCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("DE");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("19752-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Delaware", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCOLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "CODY",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "909 COUNTRY ROAD 206",
    //            City = "BOULDER",
    //            JurisdictionCode = "CO",
    //            PostalCode = "81635",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1992, 07, 13),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 73),

    //        IdNumber = "102367033",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2012,

    //        IssueDate = new DateTime(2013, 08, 08),
    //        ExpirationDate = new DateTime(2018, 07, 13),
    //        RevisionDate = new DateTime(2013, 06, 01),

    //        ComplianceType = ComplianceType.MateriallyCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "R"
    //        }
    //    };

    //    var file = License("CO");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("81635", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Colorado", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCO2013License()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "JANE",
    //            Middle = "LYNN",
    //            Last = "MOTORIST",
    //            Suffix = "SR"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "98765 W 23RD AVE",
    //            City = "LAKEWOOD",
    //            JurisdictionCode = "CO",
    //            PostalCode = "80401",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1972, 02, 04),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 63),

    //        IdNumber = "124336019",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2016, 12, 27),
    //        ExpirationDate = new DateTime(2022, 01, 04),
    //        RevisionDate = new DateTime(2015, 10, 30),

    //        HasTemporaryLawfulStatus = false,
    //        DocumentDiscriminator = "16455534969",
    //        AuditInformation = "20170104_000227_9_3776",

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "R"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZCZ", "CANONE" }
    //        }
    //    };

    //    var file = License("CO 2013");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("80401", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Colorado", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestALLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "MOTORIST",
    //            Last = "SMITH"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 COUNTY DR",
    //            City = "BLUE RIDGE",
    //            JurisdictionCode = "AL",
    //            PostalCode = "360931234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1967, 03, 27),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(totalInches: 70),
    //        WeightRange = WeightRange.Lbs191To220,

    //        IdNumber = "5677922",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2014, 11, 26),
    //        ExpirationDate = new DateTime(2018, 11, 18),
    //        RevisionDate = new DateTime(2009, 11, 06),

    //        ComplianceType = ComplianceType.NonCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "DMV"
    //        }
    //    };

    //    var file = License("AL");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("36093-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Alabama", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestAZLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "SUSAN",
    //            Middle = "T",
    //            Last = "WILLIAMS"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "5123 WACO DR",
    //            City = "TUSCON",
    //            JurisdictionCode = "AZ",
    //            PostalCode = "856414321",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1989, 01, 24),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(feet: 5, inches: 5),
    //        Weight = new Weight(pounds: 160),

    //        IdNumber = "D04852767",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2013, 06, 04),
    //        ExpirationDate = new DateTime(2054, 01, 24),

    //        IsOrganDonor = false,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("AZ");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("85641-4321", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Arizona", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestARLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "RALPH",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "321 MAIN ST",
    //            City = "HOT SPRINGS",
    //            JurisdictionCode = "AR",
    //            PostalCode = "719014455",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1946, 11, 22),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Ethnicity = Ethnicity.White,
    //        Height = new Height(totalInches: 70),

    //        IdNumber = "9298847972",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2010,

    //        IssueDate = new DateTime(2016, 09, 13),
    //        ExpirationDate = new DateTime(2024, 11, 22),
    //        RevisionDate = new DateTime(2012, 09, 15),

    //        ComplianceType = ComplianceType.NonCompliant,
    //        HasTemporaryLawfulStatus = false,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("AR");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("71901-4455", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Arkansas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestWALicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "S",
    //            Last = "TESTER"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "16255 PEWDER CT SE",
    //            City = "REDMOND",
    //            JurisdictionCode = "WA",
    //            PostalCode = "980081234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1950, 05, 23),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 61),
    //        WeightRange = WeightRange.Lbs131To160,

    //        IdNumber = "TESTEDM504K9",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2015, 04, 16),
    //        ExpirationDate = new DateTime(2021, 05, 23)
    //    };

    //    var file = License("WA");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("98008-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Washington", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestMTLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "ROSE",
    //            Last = "TESTER"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1254 MAGNOLIA AVE",
    //            City = "HELENA",
    //            JurisdictionCode = "MT",
    //            PostalCode = "59601",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1994, 05, 14),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Hazel,
    //        Height = new Height(totalInches: 67),
    //        WeightRange = WeightRange.Lbs131To160,

    //        IdNumber = "0504928899117",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2015, 07, 02),
    //        ExpirationDate = new DateTime(2023, 05, 14),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("MT");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("59601", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Montana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestKSLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "JOEY",
    //            Middle = "SMITH",
    //            Last = "MOTORIST",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12345 S 110TH TER",
    //            City = "OVERLAND PARK",
    //            JurisdictionCode = "KS",
    //            PostalCode = "66210",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1980, 01, 26),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 71),

    //        IdNumber = "K04-76-5990",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2016,

    //        IssueDate = new DateTime(2017, 11, 29),
    //        ExpirationDate = new DateTime(2023, 01, 26),
    //        RevisionDate = new DateTime(2017, 02, 26),

    //        ComplianceType = ComplianceType.FullyCompliant,
    //        IsOrganDonor = true,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C"
    //        }
    //    };

    //    var file = License("KS");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("66210", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Kansas", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestINLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "RYAN",
    //            Middle = "MICHAEL",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12345 W HENCHMEN CIR",
    //            City = "ANYCITY",
    //            JurisdictionCode = "IN",
    //            PostalCode = "47458",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1993, 02, 25),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Hazel,
    //        HairColor = HairColor.Blond,
    //        Height = new Height(totalInches: 69),
    //        Weight = new Weight(pounds: 245),

    //        IdNumber = "3249-09-7547",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2016, 08, 03),
    //        ExpirationDate = new DateTime(2023, 02, 25),
    //        RevisionDate = new DateTime(2009, 09, 21),

    //        ComplianceType = ComplianceType.FullyCompliant
    //    };

    //    var file = License("IN");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("47458", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Indiana", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestILLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "SUSAN",
    //            Middle = "T",
    //            Last = "MOTORIST",

    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 LAKE SHORE DR APT",
    //            StreetLine2 = "6431",
    //            City = "CHICAGO",
    //            JurisdictionCode = "IL",
    //            PostalCode = "60611",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1969, 06, 27),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 68),
    //        Weight = new Weight(pounds: 200),

    //        IdNumber = "W63177069784",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2017, 04, 13),
    //        ExpirationDate = new DateTime(2021, 06, 27),
    //        RevisionDate = new DateTime(2015, 09, 17),

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("IL");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("60611", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Illinois", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestHILicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "JAY",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "456 MOANA ST 2",
    //            StreetLine2 = "O",
    //            City = "HONOLULU",
    //            JurisdictionCode = "HI",
    //            PostalCode = "96826",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1988, 03, 12),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Blond,
    //        Height = new Height(totalInches: 72),

    //        IdNumber = "H01387330",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2016, 05, 13),
    //        ExpirationDate = new DateTime(2024, 03, 12),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "3",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = License("HI");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("96826", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Hawaii", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestWVLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "JOE",
    //            Middle = "BOB",
    //            Last = "SMITH"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "518   S RANDOM STREET",
    //            City = "ANYTOWN",
    //            JurisdictionCode = "WV",
    //            PostalCode = "12345",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1972, 11, 03),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 71),
    //        Weight = new Weight(pounds: 190),

    //        IdNumber = "F123456",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2017, 10, 01),
    //        ExpirationDate = new DateTime(2022, 11, 03),
    //        ComplianceType = ComplianceType.NonCompliant,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "E"
    //        }
    //    };

    //    var file = License("WV");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("12345", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("West Virginia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestAKLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "JOE",
    //            Last = "SMITH"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12345 E MAIN HY",
    //            City = "ANCHORAGE",
    //            JurisdictionCode = "AK",
    //            PostalCode = "99645",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1955, 04, 02),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Gray,
    //        Height = new Height(totalInches: 64),
    //        Weight = new Weight(pounds: 160),

    //        IdNumber = "7559886",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2016, 03, 22),
    //        ExpirationDate = new DateTime(2021, 04, 02),
    //        Under21Until = new DateTime(1976, 04, 02),

    //        IsOrganDonor = true,
    //        IsVeteran = false,
    //        DocumentDiscriminator = "2881111",

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "1"
    //        }
    //    };

    //    var file = License("AK");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("99645", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Alaska", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestDCLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "DIANA",
    //            Middle = "ROBIN",
    //            Last = "AL-MAAR"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1234 14TH ST SW 1A",
    //            City = "WASHINGTON",
    //            JurisdictionCode = "DC",
    //            PostalCode = "200091234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1985, 07, 29),
    //        Sex = Sex.Female,
    //        Height = new Height(feet: 5, inches: 6),
    //        Weight = new Weight(pounds: 140),

    //        IdNumber = "3234567",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2013, 07, 30),
    //        ExpirationDate = new DateTime(2021, 07, 29),

    //        IsOrganDonor = true,
    //        DocumentDiscriminator = "2881111",

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("DC");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("20009-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("District of Columbia", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestPELicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "PATTY",
    //            Last = "FLOWERS",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 NORTH LAKE SHORE DR",
    //            City = "ANYTOWN",
    //            JurisdictionCode = "PE",
    //            PostalCode = "C0A2B4",
    //            Country = Country.Canada
    //        },

    //        DateOfBirth = new DateTime(1955, 09, 04),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Blue,
    //        Height = new Height(centimeters: 157),

    //        IdNumber = "247725",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2016,

    //        IssueDate = new DateTime(2017, 12, 22),
    //        ExpirationDate = new DateTime(2020, 09, 04),

    //        DocumentDiscriminator = "PE2017122200000019550904",

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "5"
    //        }
    //    };

    //    var file = License("PE");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("C0A 2B4", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Price Edward Island", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestNVLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "M",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "6543 ADORAMA DR",
    //            City = "NORTH LAS VEGAS",
    //            JurisdictionCode = "NV",
    //            PostalCode = "890311234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1976, 01, 15),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(totalInches: 68),
    //        WeightRange = WeightRange.Lbs191To220,

    //        IdNumber = "0003456789",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        IssueDate = new DateTime(2017, 03, 01),
    //        ExpirationDate = new DateTime(2025, 01, 15),

    //        DocumentDiscriminator = "000123456789098765432",
    //        InventoryControlNumber = "0012345678900",

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZNZ", "NAY" },
    //            { "ZNB", "10102008" },
    //            { "ZNC", "5?08??" },
    //            { "ZND", "210" },
    //            { "ZNE", "NCDL" },
    //            { "ZNF", "NCDL" },
    //            { "ZNG", "S" },
    //            { "ZNH", "00123456780" },
    //            { "ZNI", "00000001234" }
    //        }
    //    };

    //    var file = License("NV");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("89031-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Nevada", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestNDLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "DOE",
    //            Last = "MOTORIST",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 W HIGHWAY AVE",
    //            City = "ANYCITY",
    //            JurisdictionCode = "ND",
    //            PostalCode = "58503",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1985, 07, 04),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        Height = new Height(totalInches: 74),

    //        IdNumber = "RUN812345",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2014, 10, 26),
    //        ExpirationDate = new DateTime(2019, 07, 04),
    //        RevisionDate = new DateTime(2014, 01, 08),

    //        DocumentDiscriminator = "8RUN812345RC22110GA75NDZ",
    //        InventoryControlNumber = "0123456789098765",

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "DM"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZNZ", "NA704" },
    //            { "ZNB", "1" }
    //        }
    //    };

    //    var file = License("ND");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("58503", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("North Dakota", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCTLicenseUndefinedCharacters()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "WENDY",
    //            Middle = "SMITH",
    //            Last = "MOTORIST",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12 SACAGAWEA DR",
    //            City = "WEST HARTFORD",
    //            JurisdictionCode = "CT",
    //            PostalCode = "061171234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1966, 01, 26),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(totalInches: 67),

    //        IdNumber = "123456780",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2016,

    //        IssueDate = new DateTime(2019, 01, 08),
    //        ExpirationDate = new DateTime(2026, 01, 26),
    //        RevisionDate = new DateTime(2017, 02, 10),

    //        DocumentDiscriminator = "12345678909870MVK3",
    //        InventoryControlNumber = "123456780CTRBTL02",

    //        ComplianceType = ComplianceType.FullyCompliant,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZCZ", "CA" },
    //            { "ZCB", "0005276677" }
    //        }
    //    };

    //    var file = License("CT Undefined Characters");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("06117-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestABLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "SMITH",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 MAPLE LEAF TERR SW",
    //            City = "CALGARY",
    //            JurisdictionCode = "AB",
    //            PostalCode = "T4G7A7",
    //            Country = Country.Canada
    //        },

    //        DateOfBirth = new DateTime(1993, 02, 04),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Brown,
    //        HairColor = HairColor.Brown,
    //        Height = new Height(centimeters: 155),
    //        Weight = new Weight(kilograms: 50),
    //        // Alberta specifies the weight range following the weight in kilograms
    //        WeightRange = WeightRange.Lbs101To130,

    //        IdNumber = "123400-056",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2005,

    //        ExpirationDate = new DateTime(2019, 01, 08),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "5",
    //            EndorsementCodes = "A"
    //        }
    //    };

    //    var file = License("AB");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("T4G 7A7", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Alberta", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestMNLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "DALE",
    //            Middle = "THOR",
    //            Last = "SPARKS",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12345 MAIN ST",
    //            StreetLine2 = "UNIT 91",
    //            City = "AITKIN",
    //            JurisdictionCode = "MN",
    //            PostalCode = "564311234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1995, 01, 04),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(totalInches: 70),
    //        Weight = new Weight(pounds: 138),

    //        IdNumber = "H868087743210",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2016,

    //        IssueDate = new DateTime(2018, 12, 22),
    //        ExpirationDate = new DateTime(2020, 01, 04),
    //        RevisionDate = new DateTime(2017, 10, 23),

    //        IsOrganDonor = true,
    //        ComplianceType = ComplianceType.NonCompliant,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "2"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZMZ", "MAN" },
    //            { "ZMB", "N" }
    //        }
    //    };

    //    var file = License("MN");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("56431-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Minnesota", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

    //    Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    //}

    //[Fact]
    //public void TestMSLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "PATRICK",
    //            Last = "MOTORIST",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 BUCK RUN",
    //            City = "HATTIESBURG",
    //            JurisdictionCode = "MS",
    //            PostalCode = "39402",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1963, 05, 07),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Blue,
    //        Height = new Height(totalInches: 70),

    //        IdNumber = "800448123",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2013,

    //        IssueDate = new DateTime(2018, 05, 10),
    //        ExpirationDate = new DateTime(2022, 05, 07),
    //        RevisionDate = new DateTime(2016, 02, 22),

    //        IsOrganDonor = true,

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "R",
    //            RestrictionCodes = "BF"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZMZ", "MAN" },
    //            { "ZMB", "N" },
    //            { "ZMC", "N" },
    //            { "ZMD", "123 BUCK RUN" },
    //            { "ZME", "HATTIESBURG" },
    //            { "ZMF", "MS" },
    //            { "ZMG", "394020000" }
    //        }
    //    };

    //    var file = License("MS");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("39402", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Mississippi", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

    //    Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    //}

    //[Fact]
    //public void TestIDLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "CLAY",
    //            Middle = "MOTORIST",
    //            Last = "JENSEN"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1234 MAIN STREET",
    //            City = "GRANGEVILL",
    //            JurisdictionCode = "ID",
    //            PostalCode = "83530",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1967, 12, 08),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        HairColor = HairColor.Blond,
    //        Height = new Height(totalInches: 73),
    //        Weight = new Weight(pounds: 240),

    //        IdNumber = "WA104577G",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2010,

    //        IssueDate = new DateTime(2011, 11, 20),
    //        ExpirationDate = new DateTime(2019, 12, 08),
    //        RevisionDate = new DateTime(2011, 05, 09),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D",
    //            RestrictionCodes = "B"
    //        },

    //        AdditionalJurisdictionElements =
    //        {
    //            { "ZIZ", "IADONOR" },
    //            { "ZIB", "" },
    //            { "ZIC", "" }
    //        }
    //    };

    //    var file = License("ID");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("83530", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Idaho", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());

    //    Assert.Equal(expected.AdditionalJurisdictionElements.Count, parseResult.Card.AdditionalJurisdictionElements.Count);
    //}

    //[Fact]
    //public void TestLeadingWhitespaceLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MOTORIST",
    //            Middle = "R",
    //            Last = "SHEEHAN",
    //            WasFirstTruncated = false,
    //            WasMiddleTruncated = false,
    //            WasLastTruncated = false
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "2 ROBERTS DRIVE",
    //            City = "PLYMOUTH",
    //            JurisdictionCode = "MA",
    //            PostalCode = "023601234",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1939, 12, 07),
    //        Sex = Sex.Male,
    //        Height = new Height(totalInches: 71),

    //        IdNumber = "S58239477",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2014, 11, 14),
    //        ExpirationDate = new DateTime(2018, 12, 07),
    //        RevisionDate = new DateTime(2009, 07, 15),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "DM",
    //            RestrictionCodes = "B"
    //        }
    //    };

    //    var file = File.ReadAllText("Leading Whitespace.txt");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("02360-1234", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Massachusetts", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestInvalidHeader()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MICHAEL",
    //            Middle = "G",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "12 MAIN AVE",
    //            City = "WEST HAVEN",
    //            JurisdictionCode = "CT",
    //            PostalCode = "06516",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1961, 02, 04),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Brown,
    //        Height = new Height(feet: 5, inches: 4),

    //        IdNumber = "025995434",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2016, 11, 14),
    //        ExpirationDate = new DateTime(2023, 02, 04),

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = File.ReadAllText("Invalid Header.txt");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("06516", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCTLicenseSuffix()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "PABLO",
    //            Last = "CORTEZ",
    //            Suffix = "JR"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "715 MAIN LN",
    //            City = "STRATFORD",
    //            JurisdictionCode = "CT",
    //            PostalCode = "066140123",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1976, 10, 07),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(feet: 6, inches: 0),

    //        IdNumber = "227881513",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2016, 08, 23),
    //        ExpirationDate = new DateTime(2022, 10, 07),

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("CT Suffix");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("06614-0123", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestCTLicenseMultipleMiddleNames()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "PABLO",
    //            Middle = "LUIS RODRIGUEZ",
    //            Last = "CORTEZ",
    //            Suffix = "JR"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "715 MAIN LN",
    //            City = "STRATFORD",
    //            JurisdictionCode = "CT",
    //            PostalCode = "066140123",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1976, 10, 07),
    //        Sex = Sex.Male,
    //        EyeColor = EyeColor.Green,
    //        Height = new Height(feet: 6, inches: 0),

    //        IdNumber = "227881513",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2000,

    //        IssueDate = new DateTime(2016, 08, 23),
    //        ExpirationDate = new DateTime(2022, 10, 07),

    //        IsOrganDonor = true,
    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "D"
    //        }
    //    };

    //    var file = License("CT Multiple Middle Names");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("06614-0123", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Connecticut", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestNBLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MARY",
    //            Middle = "M",
    //            Last = "MOTORIST"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "123 EAGLEHEAD DR",
    //            City = "GRND-BAY-WFLD",
    //            JurisdictionCode = "NB",
    //            PostalCode = "E5K1Y3",
    //            Country = Country.Canada
    //        },

    //        DateOfBirth = new DateTime(1962, 08, 08),
    //        Sex = Sex.Female,
    //        Height = new Height(centimeters: 168),

    //        IdNumber = "1234567",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2003,

    //        IssueDate = new DateTime(2017, 08, 12),
    //        ExpirationDate = new DateTime(2021, 08, 08)
    //    };

    //    var file = License("NB");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("E5K 1Y3", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("New Brunswick", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}

    //[Fact]
    //public void TestWYLicense()
    //{
    //    var expected = new DriversLicense
    //    {
    //        Name = new Name
    //        {
    //            First = "MOTORIST",
    //            Middle = "E",
    //            Last = "O NEIL"
    //        },

    //        Address = new Address
    //        {
    //            StreetLine1 = "1234 MAIN WAY",
    //            //StreetLine2 = "BLUE STREAM, WY  82930", TODO: Check if this is the same as city, state, zip and remove if so
    //            City = "BLUE STREAM",
    //            JurisdictionCode = "WY",
    //            PostalCode = "82930",
    //            Country = Country.USA
    //        },

    //        DateOfBirth = new DateTime(1958, 10, 31),
    //        Sex = Sex.Female,
    //        EyeColor = EyeColor.Blue,
    //        HairColor = HairColor.Blond,
    //        Height = new Height(totalInches: 69),

    //        IdNumber = "123456-789",
    //        AAMVAVersionNumber = AAMVAVersion.AAMVA2009,

    //        IssueDate = new DateTime(2017, 10, 11),
    //        ExpirationDate = new DateTime(2021, 10, 31),

    //        Jurisdiction = new DriversLicenseJurisdiction
    //        {
    //            VehicleClass = "C"
    //        }
    //    };

    //    var file = License("WY");
    //    var parseResult = Barcode.Parse2(file, Validation.None);
    //    LogUnhandledElementIds(parseResult.Card);

    //    AssertIdCard(expected, parseResult.Card);
    //    AssertLicense(expected, parseResult.Card);

    //    Assert.Equal("82930", parseResult.Card.PostalCodeDisplay);
    //    Assert.Equal("Wyoming", parseResult.Card.IssuerIdentificationNumber.Value.GetDescriptionOrDefault());
    //}
}
