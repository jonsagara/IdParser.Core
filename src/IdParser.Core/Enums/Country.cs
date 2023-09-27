using IdParser.Core.Attributes;

namespace IdParser.Core;

public enum Country
{
    [Description("United States of America")]
    [Abbreviation("US")]
    USA,

    [Abbreviation("CA")]
    Canada,

    [Abbreviation("MX")]
    Mexico,
}
