namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies a description for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public sealed class DescriptionAttribute : Attribute
{
    /// <summary>
    /// The description text.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="description">Required. The description text.</param>
    public DescriptionAttribute(string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Description = description;
    }
}
