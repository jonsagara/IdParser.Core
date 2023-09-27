using System.Reflection;

namespace IdParser.Core.Attributes;

internal static class AttributeExtensions
{
    /// <summary>
    /// If an enum value is decorated with an <see cref="AbbreviationAttribute"/>, get its Abbreviation. Otherwise, return
    /// the enum value as a string.
    /// </summary>
    internal static string GetAbbreviationOrDefaultFromAbbreviationAttribute<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        // Multiple AbbreviationAttributes are not allowed on the same member.
        var abbreviationAttr = value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<AbbreviationAttribute>();

        // If we found the attribute, return the abbreviation. Otherwise, return the enum value as a string.
        return abbreviationAttr is not null
            ? abbreviationAttr.Abbreviation
            : value.ToString();
    }

    /// <summary>
    /// If an enum value is decorated with an <see cref="AbbreviationAttribute"/>, get its Abbreviation. If the enum
    /// value is not decorated with an <see cref="AbbreviationAttribute"/>, throw an exception.
    /// </summary>
    /// <remarks>
    /// This is intended primarily for <see cref="Country"/>. There is a unit test that checks to ensure all of its
    /// values have the attribute, but this runtime check also enforces this.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown if the enum value is not decorated with an <see cref="AbbreviationAttribute"/>.</exception>
    internal static string GetAbbreviationFromAbbreviationAttribute<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        // Multiple AbbreviationAttributes are not allowed on the same member.
        var abbreviationAttr = value
            .GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<AbbreviationAttribute>();

        if (abbreviationAttr is not null)
        {
            // We found the attribute. Return the abbreviation.
            return abbreviationAttr.Abbreviation;
        }

        // This should never happen, but perhaps someone checked in TEnum without running unit tests. Regardless, throw.
        throw new ArgumentException($"{typeof(TEnum).FullName} enum value {value} is missing an {nameof(AbbreviationAttribute)}.", nameof(value));
    }

    /// <summary>
    /// If an enum value is decorated with an <see cref="CountryAttribute"/>, get its Country. Otherwise, return
    /// the enum value as a string.
    /// </summary>
    internal static Country GetCountryFromCountryAttribute<TEnum>(this TEnum value)
        where TEnum : struct, Enum
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
        where TEnum : struct, Enum
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
