namespace IdParser.Core.Parsers.Id;

internal static class ComplianceTypeParser
{
    internal static Field<ComplianceType?> Parse(string elementId, string? rawValue)
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
