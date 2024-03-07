using IdParser.Core.Attributes;

namespace IdParser.Core;

/// <remarks>
/// https://www.aamva.org/identity/issuer-identification-numbers-(iin)
/// </remarks>
public enum IssuerIdentificationNumber
{
    None = 0,

    [Country(Country.USA)]
    [Abbreviation("AL")]
    Alabama = 636033,

    [Country(Country.USA)]
    [Abbreviation("AK")]
    Alaska = 636059,

    [Country(Country.Canada)]
    [Abbreviation("AB")]
    Alberta = 604432,

    [Country(Country.Canada)]
    [Abbreviation("AB")]
    [Description("Alberta")]
    AlbertaNonIso = 990876,

    [Country(Country.USA)]
    [Abbreviation("AS")]
    [Description("American Samoa")]
    AmericanSamoa = 604427,

    [Country(Country.USA)]
    [Abbreviation("AZ")]
    Arizona = 636026,

    [Country(Country.USA)]
    [Abbreviation("AR")]
    Arkansas = 636021,

    [Country(Country.Canada)]
    [Abbreviation("BC")]
    [Description("British Columbia")]
    BritishColumbia = 636028,

    [Country(Country.USA)]
    [Abbreviation("CA")]
    California = 636014,

    [Country(Country.Mexico)]
    [Abbreviation("COA")]
    Coahuila = 636056,

    [Country(Country.USA)]
    [Abbreviation("CO")]
    Colorado = 636020,

    [Country(Country.USA)]
    [Abbreviation("CT")]
    Connecticut = 636006,

    [Country(Country.USA)]
    [Abbreviation("DE")]
    Delaware = 636011,

    [Country(Country.USA)]
    [Abbreviation("DC")]
    [Description("District of Columbia")]
    DistrictOfColumbia = 636043,

    [Country(Country.USA)]
    [Abbreviation("FL")]
    Florida = 636010,

    [Country(Country.USA)]
    [Abbreviation("GA")]
    Georgia = 636055,

    [Country(Country.USA)]
    [Abbreviation("GU")]
    Guam = 636019,

    [Country(Country.USA)]
    [Abbreviation("HI")]
    Hawaii = 636047,

    [Country(Country.Mexico)]
    [Abbreviation("HID")]
    Hidalgo = 636057,

    [Country(Country.USA)]
    [Abbreviation("ID")]
    Idaho = 636050,

    [Country(Country.USA)]
    [Abbreviation("IL")]
    Illinois = 636035,

    [Country(Country.USA)]
    [Abbreviation("IN")]
    Indiana = 636037,

    [Country(Country.USA)]
    [Abbreviation("IA")]
    Iowa = 636018,

    [Country(Country.USA)]
    [Abbreviation("KS")]
    Kansas = 636022,

    [Country(Country.USA)]
    [Abbreviation("KY")]
    Kentucky = 636046,

    [Country(Country.USA)]
    [Abbreviation("LA")]
    Louisiana = 636007,

    [Country(Country.USA)]
    [Abbreviation("ME")]
    Maine = 636041,

    [Country(Country.Canada)]
    [Abbreviation("MB")]
    Manitoba = 636048,

    [Country(Country.USA)]
    [Abbreviation("MD")]
    Maryland = 636003,

    [Country(Country.USA)]
    [Abbreviation("MA")]
    Massachusetts = 636002,

    [Country(Country.USA)]
    [Abbreviation("MI")]
    Michigan = 636032,

    [Country(Country.USA)]
    [Abbreviation("MN")]
    Minnesota = 636038,

    [Country(Country.USA)]
    [Abbreviation("MS")]
    Mississippi = 636051,

    [Country(Country.USA)]
    [Abbreviation("MO")]
    Missouri = 636030,

    [Country(Country.USA)]
    [Abbreviation("MT")]
    Montana = 636008,

    [Country(Country.USA)]
    [Abbreviation("NE")]
    Nebraska = 636054,

    [Country(Country.USA)]
    [Abbreviation("NV")]
    Nevada = 636049,

    [Country(Country.Canada)]
    [Abbreviation("NB")]
    [Description("New Brunswick")]
    NewBrunswick = 636017,

    [Country(Country.USA)]
    [Abbreviation("NH")]
    [Description("New Hampshire")]
    NewHampshire = 636039,

    [Country(Country.USA)]
    [Abbreviation("NJ")]
    [Description("New Jersey")]
    NewJersey = 636036,

    [Country(Country.USA)]
    [Abbreviation("NM")]
    [Description("New Mexico")]
    NewMexico = 636009,

    [Country(Country.USA)]
    [Abbreviation("NY")]
    [Description("New York")]
    NewYork = 636001,

    [Country(Country.Canada)]
    [Abbreviation("NL")]
    Newfoundland = 636016,

    [Country(Country.USA)]
    [Abbreviation("NC")]
    [Description("North Carolina")]
    NorthCarolina = 636004,

    [Country(Country.USA)]
    [Abbreviation("ND")]
    [Description("North Dakota")]
    NorthDakota = 636034,

    [Country(Country.USA)]
    [Abbreviation("MP")]
    [Description("Northern Marianna Islands")]
    NorthernMariannaIslands = 604430,

    [Country(Country.Canada)]
    [Abbreviation("NS")]
    [Description("Nova Scotia")]
    NovaScotia = 636013,

    [Country(Country.Canada)]
    [Abbreviation("NU")]
    Nunavut = 604433,

    [Country(Country.USA)]
    [Abbreviation("OH")]
    Ohio = 636023,

    [Country(Country.USA)]
    [Abbreviation("OK")]
    Oklahoma = 636058,

    [Country(Country.Canada)]
    [Abbreviation("ON")]
    Ontario = 636012,

    [Country(Country.USA)]
    [Abbreviation("OR")]
    Oregon = 636029,

    [Country(Country.USA)]
    [Abbreviation("PA")]
    Pennsylvania = 636025,

    [Country(Country.Canada)]
    [Abbreviation("PE")]
    [Description("Price Edward Island")]
    PrinceEdwardIsland = 604426,

    [Country(Country.USA)]
    [Abbreviation("PR")]
    [Description("Puerto Rico")]
    PuertoRico = 604431,

    [Country(Country.Canada)]
    [Abbreviation("QC")]
    Quebec = 604428,

    [Country(Country.USA)]
    [Abbreviation("RI")]
    [Description("Rhode Island")]
    RhodeIsland = 636052,

    [Country(Country.Canada)]
    [Abbreviation("SK")]
    Saskatchewan = 636044,

    [Country(Country.USA)]
    [Abbreviation("SC")]
    [Description("South Carolina")]
    SouthCarolina = 636005,

    [Country(Country.USA)]
    [Abbreviation("SD")]
    [Description("South Dakota")]
    SouthDakota = 636042,

    [Country(Country.USA)]
    [Abbreviation("TN")]
    Tennessee = 636053,

    [Country(Country.USA)]
    [Description("US State Department")]
    UsStateDepartment = 636027,

    [Country(Country.USA)]
    [Abbreviation("TX")]
    Texas = 636015,

    [Country(Country.USA)]
    [Abbreviation("VI")]
    [Description("US Virgin Islands")]
    UsVirginIslands = 636062,

    [Country(Country.USA)]
    [Abbreviation("UT")]
    Utah = 636040,

    [Country(Country.USA)]
    [Abbreviation("VT")]
    Vermont = 636024,

    [Country(Country.USA)]
    [Abbreviation("VA")]
    Virginia = 636000,

    [Country(Country.USA)]
    [Abbreviation("WA")]
    Washington = 636045,

    [Country(Country.USA)]
    [Abbreviation("WV")]
    [Description("West Virginia")]
    WestVirginia = 636061,

    [Country(Country.USA)]
    [Abbreviation("WI")]
    Wisconsin = 636031,

    [Country(Country.USA)]
    [Abbreviation("WY")]
    Wyoming = 636060,

    [Country(Country.Canada)]
    [Abbreviation("YT")]
    Yukon = 604429,
}
