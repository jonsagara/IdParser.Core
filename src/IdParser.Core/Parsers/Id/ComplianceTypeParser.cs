namespace IdParser.Core.Parsers.Id;

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

    internal static Field<ComplianceType?> Parse2(string elementId, string rawValue)
    {
        ArgumentNullException.ThrowIfNull(elementId);

        ComplianceType? complianceType = rawValue switch
        {
            "M" => ComplianceType.MateriallyCompliant,
            "F" => ComplianceType.FullyCompliant,
            "N" => ComplianceType.NonCompliant,
            _ => null,
        };

        return FieldHelpers.ParsedField(elementId: elementId, value: complianceType, rawValue: rawValue);
    }
}
