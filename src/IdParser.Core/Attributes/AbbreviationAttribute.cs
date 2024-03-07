namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies an abbreviated display value for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public sealed class AbbreviationAttribute : Attribute
{
    /// <summary>
    /// The abbreviation text.
    /// </summary>
    public string Abbreviation { get; }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="abbreviation">Required. The abbreviation text.</param>
    public AbbreviationAttribute(string abbreviation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(abbreviation);

        Abbreviation = abbreviation;
    }
}
