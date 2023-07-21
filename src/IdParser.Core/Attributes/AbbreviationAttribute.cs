namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies an abbreviated display value for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public sealed class AbbreviationAttribute : Attribute
{
    public string Abbreviation { get; }

    public AbbreviationAttribute(string abbreviation)
    {
        ArgumentNullException.ThrowIfNull(abbreviation);

        Abbreviation = abbreviation;
    }
}
