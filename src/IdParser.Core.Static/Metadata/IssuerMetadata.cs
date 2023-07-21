using System.Diagnostics.CodeAnalysis;

namespace IdParser.Core.Static.Metadata;

internal record IssuerMetadata(Country Country, string Abbreviation, string Description);

internal static class IssuerMetadataHelper
{
    private static readonly Dictionary<IssuerIdentificationNumber, IssuerMetadata> _issuerMetadata = new()
    {
        { IssuerIdentificationNumber.Alabama , new IssuerMetadata(Country.Usa, Abbreviation: "AL", Description: "Alabama") },
        { IssuerIdentificationNumber.Alaska , new IssuerMetadata(Country.Usa, Abbreviation: "AK", Description: "Alaska") },
        { IssuerIdentificationNumber.Alberta , new IssuerMetadata(Country.Canada, Abbreviation: "AB", Description: "Alberta") },
        { IssuerIdentificationNumber.AlbertaNonIso , new IssuerMetadata(Country.Canada, Abbreviation: "AB", Description: "Alberta") },
        { IssuerIdentificationNumber.AmericanSamoa , new IssuerMetadata(Country.Usa, Abbreviation: "AS", Description: "American Samoa") },
        { IssuerIdentificationNumber.Arizona , new IssuerMetadata(Country.Usa, Abbreviation: "AZ", Description: "Arizona") },
        { IssuerIdentificationNumber.Arkansas , new IssuerMetadata(Country.Usa, Abbreviation: "AR", Description: "Arkansas") },
        { IssuerIdentificationNumber.BritishColumbia , new IssuerMetadata(Country.Canada, Abbreviation: "BC", Description: "British Columbia") },
        { IssuerIdentificationNumber.California , new IssuerMetadata(Country.Usa, Abbreviation: "CA", Description: "California") },
        { IssuerIdentificationNumber.Coahuila , new IssuerMetadata(Country.Mexico, Abbreviation: "COA", Description: "Coahuila") },
        { IssuerIdentificationNumber.Colorado , new IssuerMetadata(Country.Usa, Abbreviation: "CO", Description: "Colorado") },
        { IssuerIdentificationNumber.Connecticut , new IssuerMetadata(Country.Usa, Abbreviation: "CT", Description: "Connecticut") },
        { IssuerIdentificationNumber.Delaware , new IssuerMetadata(Country.Usa, Abbreviation: "DE", Description: "Delaware") },
        { IssuerIdentificationNumber.DistrictOfColumbia , new IssuerMetadata(Country.Usa, Abbreviation: "DC", Description: "District of Columbia") },
        { IssuerIdentificationNumber.Florida , new IssuerMetadata(Country.Usa, Abbreviation: "FL", Description: "Florida") },
        { IssuerIdentificationNumber.Georgia , new IssuerMetadata(Country.Usa, Abbreviation: "GA", Description: "Georgia") },
        { IssuerIdentificationNumber.Guam , new IssuerMetadata(Country.Usa, Abbreviation: "GU", Description: "Guam") },
        { IssuerIdentificationNumber.Hawaii , new IssuerMetadata(Country.Usa, Abbreviation: "HI", Description: "Hawaii") },
        { IssuerIdentificationNumber.Hidalgo , new IssuerMetadata(Country.Mexico, Abbreviation: "HID", Description: "Hidalgo") },
        { IssuerIdentificationNumber.Idaho , new IssuerMetadata(Country.Usa, Abbreviation: "ID", Description: "Idaho") },
        { IssuerIdentificationNumber.Illinois , new IssuerMetadata(Country.Usa, Abbreviation: "IL", Description: "Illinois") },
        { IssuerIdentificationNumber.Indiana , new IssuerMetadata(Country.Usa, Abbreviation: "IN", Description: "Indiana") },
        { IssuerIdentificationNumber.Iowa , new IssuerMetadata(Country.Usa, Abbreviation: "IA", Description: "Iowa") },
        { IssuerIdentificationNumber.Kansas , new IssuerMetadata(Country.Usa, Abbreviation: "KS", Description: "Kansas") },
        { IssuerIdentificationNumber.Kentucky , new IssuerMetadata(Country.Usa, Abbreviation: "KY", Description: "Kentucky") },
        { IssuerIdentificationNumber.Louisiana , new IssuerMetadata(Country.Usa, Abbreviation: "LA", Description: "Louisiana") },
        { IssuerIdentificationNumber.Maine , new IssuerMetadata(Country.Usa, Abbreviation: "ME", Description: "Maine") },
        { IssuerIdentificationNumber.Manitoba , new IssuerMetadata(Country.Canada, Abbreviation: "MB", Description: "Manitoba") },
        { IssuerIdentificationNumber.Maryland , new IssuerMetadata(Country.Usa, Abbreviation: "MD", Description: "Maryland") },
        { IssuerIdentificationNumber.Massachusetts , new IssuerMetadata(Country.Usa, Abbreviation: "MA", Description: "Massachusetts") },
        { IssuerIdentificationNumber.Michigan , new IssuerMetadata(Country.Usa, Abbreviation: "MI", Description: "Michigan") },
        { IssuerIdentificationNumber.Minnesota , new IssuerMetadata(Country.Usa, Abbreviation: "MN", Description: "Minnesota") },
        { IssuerIdentificationNumber.Mississippi , new IssuerMetadata(Country.Usa, Abbreviation: "MS", Description: "Mississippi") },
        { IssuerIdentificationNumber.Missouri , new IssuerMetadata(Country.Usa, Abbreviation: "MO", Description: "Missouri") },
        { IssuerIdentificationNumber.Montana , new IssuerMetadata(Country.Usa, Abbreviation: "MT", Description: "Montana") },
        { IssuerIdentificationNumber.Nebraska , new IssuerMetadata(Country.Usa, Abbreviation: "NE", Description: "Nebraska") },
        { IssuerIdentificationNumber.Nevada , new IssuerMetadata(Country.Usa, Abbreviation: "NV", Description: "Nevada") },
        { IssuerIdentificationNumber.NewBrunswick , new IssuerMetadata(Country.Canada, Abbreviation: "NB", Description: "New Brunswick") },
        { IssuerIdentificationNumber.Newfoundland , new IssuerMetadata(Country.Canada, Abbreviation: "NL", Description: "Newfoundland") },
        { IssuerIdentificationNumber.NewHampshire , new IssuerMetadata(Country.Usa, Abbreviation: "NH", Description: "New Hampshire") },
        { IssuerIdentificationNumber.NewJersey , new IssuerMetadata(Country.Usa, Abbreviation: "NJ", Description: "New Jersey") },
        { IssuerIdentificationNumber.NewMexico , new IssuerMetadata(Country.Usa, Abbreviation: "NM", Description: "New Mexico") },
        { IssuerIdentificationNumber.NewYork , new IssuerMetadata(Country.Usa, Abbreviation: "NY", Description: "New York") },
        { IssuerIdentificationNumber.NorthCarolina , new IssuerMetadata(Country.Usa, Abbreviation: "NC", Description: "North Carolina") },
        { IssuerIdentificationNumber.NorthDakota , new IssuerMetadata(Country.Usa, Abbreviation: "ND", Description: "North Dakota") },
        { IssuerIdentificationNumber.NorthernMariannaIslands , new IssuerMetadata(Country.Usa, Abbreviation: "MP", Description: "Northern Marianna Islands") },
        { IssuerIdentificationNumber.NovaScotia , new IssuerMetadata(Country.Canada, Abbreviation: "NS", Description: "Nova Scotia") },
        { IssuerIdentificationNumber.Nunavut , new IssuerMetadata(Country.Canada, Abbreviation: "NU", Description: "Nunavut") },
        { IssuerIdentificationNumber.Ohio , new IssuerMetadata(Country.Usa, Abbreviation: "OH", Description: "Ohio") },
        { IssuerIdentificationNumber.Oklahoma , new IssuerMetadata(Country.Usa, Abbreviation: "OK", Description: "Oklahoma") },
        { IssuerIdentificationNumber.Ontario , new IssuerMetadata(Country.Canada, Abbreviation: "ON", Description: "Ontario") },
        { IssuerIdentificationNumber.Oregon , new IssuerMetadata(Country.Usa, Abbreviation: "OR", Description: "Oregon") },
        { IssuerIdentificationNumber.Pennsylvania , new IssuerMetadata(Country.Usa, Abbreviation: "PA", Description: "Pennsylvania") },
        { IssuerIdentificationNumber.PrinceEdwardIsland , new IssuerMetadata(Country.Canada, Abbreviation: "PE", Description: "Price Edward Island") },
        { IssuerIdentificationNumber.PuertoRico , new IssuerMetadata(Country.Usa, Abbreviation: "PR", Description: "Puerto Rico") },
        { IssuerIdentificationNumber.Quebec , new IssuerMetadata(Country.Canada, Abbreviation: "QC", Description: "Quebec") },
        { IssuerIdentificationNumber.RhodeIsland , new IssuerMetadata(Country.Usa, Abbreviation: "RI", Description: "Rhode Island") },
        { IssuerIdentificationNumber.Saskatchewan , new IssuerMetadata(Country.Canada, Abbreviation: "SK", Description: "Saskatchewan") },
        { IssuerIdentificationNumber.SouthCarolina , new IssuerMetadata(Country.Usa, Abbreviation: "SC", Description: "South Carolina") },
        { IssuerIdentificationNumber.SouthDakota , new IssuerMetadata(Country.Usa, Abbreviation: "SD", Description: "South Dakota") },
        { IssuerIdentificationNumber.Tennessee , new IssuerMetadata(Country.Usa, Abbreviation: "TN", Description: "Tennessee") },
        { IssuerIdentificationNumber.Texas , new IssuerMetadata(Country.Usa, Abbreviation: "TX", Description: "Texas") },
        { IssuerIdentificationNumber.UsStateDepartment , new IssuerMetadata(Country.Usa, Abbreviation: "UsStateDepartment", Description: "US State Department") },
        { IssuerIdentificationNumber.UsVirginIslands , new IssuerMetadata(Country.Usa, Abbreviation: "VI", Description: "US Virgin Islands") },
        { IssuerIdentificationNumber.Utah , new IssuerMetadata(Country.Usa, Abbreviation: "UT", Description: "Utah") },
        { IssuerIdentificationNumber.Vermont , new IssuerMetadata(Country.Usa, Abbreviation: "VT", Description: "Vermont") },
        { IssuerIdentificationNumber.Virginia , new IssuerMetadata(Country.Usa, Abbreviation: "VA", Description: "Virginia") },
        { IssuerIdentificationNumber.Washington , new IssuerMetadata(Country.Usa, Abbreviation: "WA", Description: "Washington") },
        { IssuerIdentificationNumber.WestVirginia , new IssuerMetadata(Country.Usa, Abbreviation: "WV", Description: "West Virginia") },
        { IssuerIdentificationNumber.Wisconsin , new IssuerMetadata(Country.Usa, Abbreviation: "WI", Description: "Wisconsin") },
        { IssuerIdentificationNumber.Wyoming , new IssuerMetadata(Country.Usa, Abbreviation: "WY", Description: "Wyoming") },
        { IssuerIdentificationNumber.Yukon , new IssuerMetadata(Country.Canada, Abbreviation: "YT", Description: "Yukon") },
    };


    internal static bool TryGetIssuerCountry(IssuerIdentificationNumber issuerIdentificationNumber, [NotNullWhen(true)] out Country? country)
    {
        country = null;

        if (_issuerMetadata.TryGetValue(issuerIdentificationNumber, out IssuerMetadata? issuerMetadata))
        {
            country = issuerMetadata.Country;
            return true;
        }

        return false;
    }
}
