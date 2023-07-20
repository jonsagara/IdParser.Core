namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies an abbreviated display value for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class AbbreviationAttribute : Attribute
{
    public string Abbreviation { get; }

    public AbbreviationAttribute(string abbreviation)
    {
        Abbreviation = abbreviation;
    }
}
