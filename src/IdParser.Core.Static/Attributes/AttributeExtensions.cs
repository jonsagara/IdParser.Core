using System.Reflection;

namespace IdParser.Core.Static.Attributes;

internal static class AttributeExtensions
{
    /// <summary>
    /// If an enum value is decorated with an <see cref="AbbreviationAttribute"/>, get its Abbreviation. Otherwise, return
    /// the enum value as a string.
    /// </summary>
    internal static string GetAbbreviationFromAbbreviationAttribute<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        // Multiple AbbreviationAttributes are not allowed on the same member.
        var abbreviationAttribute = value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<AbbreviationAttribute>();

        // If we found the attribute, return the abbreviation. Otherwise, return the enum value as a string.
        return abbreviationAttribute is not null
            ? abbreviationAttribute.Abbreviation
            : value.ToString();
    }

    /// <summary>
    /// If an enum value is decorated with an <see cref="CountryAttribute"/>, get its Country. Otherwise, return
    /// the enum value as a string.
    /// </summary>
    internal static Country GetCountryFromCountryAttribute<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        // Multiple CountryAttributes are not allowed on the same member.
        var countryAttribute = value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<CountryAttribute>();

        if (countryAttribute is null)
        {
            // The enum value used to invoke this extension method is missing the specific attribute declaration,
            //   where the attribute has a property of type Country.
            // Practically speaking, this means that a IssuerIdentificationNumber enum value is missing a CountryAttribute,
            //   Country attribute has a single property of type Country, another enum type.
            throw new InvalidOperationException($"Enum value {value.GetType().FullName}.{value} is missing a {typeof(CountryAttribute).FullName}.");
        }

        // If we found the attribute, return the country. Otherwise, return the enum value as a string.
        return countryAttribute.Country;
    }

    /// <summary>
    /// If an enum value is decorated with a <see cref="DescriptionAttribute"/>, get its Abbreviation. Otherwise, return
    /// the enum value as a string.
    /// </summary>
    internal static string GetDescriptionFromDescriptionAttribute<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        // Multiple DescriptionAttributes are not allowed on the same member.
        var descriptionAttribute = value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DescriptionAttribute>();

        // If we found the attribute, return the abbreviation. Otherwise, return the enum value as a string.
        return descriptionAttribute is not null
            ? descriptionAttribute.Description
            : value.ToString();
    }
}
