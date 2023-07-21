namespace IdParser.Core.Metadata;

internal record EthnicityMetadata(string Abbreviation, string? Description);

internal static class EthnicityMetadataHelper
{
    private static readonly Dictionary<Ethnicity, EthnicityMetadata> _ethnicityMetadata = new()
    {
        { Ethnicity.AlaskanAmericanIndian, new EthnicityMetadata(Abbreviation: "AI", Description: "Alaskan or American Indian") },
        { Ethnicity.AsianPacificIslander, new EthnicityMetadata(Abbreviation: "AP", Description: "Asian or Pacific Islander") },
        { Ethnicity.Black, new EthnicityMetadata(Abbreviation: "BK", Description: null) },
        { Ethnicity.HispanicOrigin, new EthnicityMetadata(Abbreviation: "H", Description: "Hispanic Origin") },
        { Ethnicity.NonHispanic, new EthnicityMetadata(Abbreviation: "O", Description: "Non-Hispanic") },
        { Ethnicity.White, new EthnicityMetadata(Abbreviation: "W", Description: null) },
    };


    internal static string GetAbbreviationOrDefault(this Ethnicity ethnicity)
    {
        return _ethnicityMetadata.TryGetValue(ethnicity, out EthnicityMetadata? ethnicityMetadata)
            ? ethnicityMetadata.Abbreviation
            : ethnicity.ToString();
    }
}
