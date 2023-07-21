using IdParser.Core.Attributes;

namespace IdParser.Core;

public enum ComplianceType
{
    [Description("Materially Compliant")]
    MateriallyCompliant,

    [Description("Fully Compliant")]
    FullyCompliant,

    [Description("Non-Compliant")]
    NonCompliant,
}
