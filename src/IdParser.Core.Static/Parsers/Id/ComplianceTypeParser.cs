namespace IdParser.Core.Static.Parsers.Id;

//[Parser("DDA")]
internal static class ComplianceTypeParser
{
    internal static string? Parse(string input)
    {
        switch (input)
        {
            case "M":
                IdCard.ComplianceType = ComplianceType.MateriallyCompliant;
                break;
            case "F":
                IdCard.ComplianceType = ComplianceType.FullyCompliant;
                break;
            case "N":
                IdCard.ComplianceType = ComplianceType.NonCompliant;
                break;
        }
    }
}
