namespace IdParser.Core.Attributes;

/// <summary>
/// Specifies a country display value for the target.
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public sealed class CountryAttribute : Attribute
{
    public Country Country { get; }

    public CountryAttribute(Country country)
    {
        Country = country;
    }
}
