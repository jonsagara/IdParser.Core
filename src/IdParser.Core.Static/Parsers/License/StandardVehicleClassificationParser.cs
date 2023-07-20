namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCM")]
internal static class StandardVehicleClassificationParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.StandardVehicleClassification = input;
    }
}
