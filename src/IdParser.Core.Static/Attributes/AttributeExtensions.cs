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
