namespace IdParser.Core.Static.Parsers.Id;

internal static class ComplianceTypeParser
{
    internal static ComplianceType? Parse(string input)
    {
        switch (input)
        {
            case "M":
                return ComplianceType.MateriallyCompliant;

            case "F":
                return ComplianceType.FullyCompliant;

            case "N":
                return ComplianceType.NonCompliant;

            default:
                return null;
        }
    }
}
