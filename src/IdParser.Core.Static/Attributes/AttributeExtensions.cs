using System.Reflection;

namespace IdParser.Core.Static.Attributes;

internal static class AttributeExtensions
{
    /// <summary>
    /// If an enum value is decorated with an Abbreviation attribute, get its Abbreviation. Otherwise, return
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
}
