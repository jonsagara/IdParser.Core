using IdParser.Core.Attributes;

// ReSharper disable once CheckNamespace
namespace IdParser.Core;

public enum ComplianceType
{
    [Description("Materially Compliant")]
    MateriallyCompliant,

    [Description("Fully Compliant")]
    FullyCompliant,

    [Description("Non-Compliant")]
    NonCompliant
}