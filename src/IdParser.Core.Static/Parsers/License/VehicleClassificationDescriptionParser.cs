namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCP")]
internal static class VehicleClassificationDescriptionParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.VehicleClassificationDescription = input;
    }
}
