namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies a description for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class DescriptionAttribute : Attribute
{
    public string Description { get; }

    public DescriptionAttribute(string description)
    {
        Description = description;
    }
}
