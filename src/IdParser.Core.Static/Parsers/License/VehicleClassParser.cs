namespace IdParser.Core.Static.Parsers.License;

//[Parser("DCA")]
internal static class VehicleClassParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.VehicleClass = input;
    }
}

//[Parser("DAR")]
internal static class VehicleClassLegacyParser
{
    internal static string? Parse(string input)
    {
        if (StringHasNoValue(input))
        {
            return;
        }

        License.Jurisdiction.VehicleClass = input;
    }
}
