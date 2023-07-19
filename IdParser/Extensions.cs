using System.Reflection;
using System.Text;
using IdParser.Attributes;

namespace IdParser;

public static class Extensions
{
    /// <summary>
    /// Gets the value of the <see cref="DescriptionAttribute"/> on the <see cref="Enum"/>.
    /// </summary>
    public static string GetDescription(this Enum enumValue)
    {
        ArgumentNullException.ThrowIfNull(enumValue);

        return enumValue.GetAttributeValueOrDefault<DescriptionAttribute, string>(a => a.Description);
    }

    /// <summary>
    /// Gets the value of the <see cref="AbbreviationAttribute"/> on the <see cref="Enum"/>.
    /// </summary>
    public static string GetAbbreviation(this Enum enumValue)
    {
        ArgumentNullException.ThrowIfNull(enumValue);

        return enumValue.GetAttributeValueOrDefault<AbbreviationAttribute, string>(a => a.Abbreviation);
    }

    /// <summary>
    /// Gets the value of the <see cref="CountryAttribute"/> on the <see cref="Enum"/>.
    /// </summary>
    public static Country GetCountry(this Enum enumValue)
    {
        ArgumentNullException.ThrowIfNull(enumValue);

        return enumValue.GetAttributeValueOrDefault<CountryAttribute, Country>(a => a.Country);
    }

    private static TValue GetAttributeValueOrDefault<TAttribute, TValue>(this Enum enumValue, Func<TAttribute, TValue> valueGetter)
        where TAttribute : Attribute
    {
        // This extension method is invoked on an existing enum value, so the enum value must exist.
        //   This might not be true if we were accepting random inputs from users and acting on those,
        //   but we're not. Hence, it's okay to mark this line as not returning null.
        var enumValueFieldInfo = enumValue.GetType().GetTypeInfo().GetField(enumValue.ToString())!;
        var enumValueAttribute = enumValueFieldInfo.GetCustomAttribute<TAttribute>();

        if (typeof(TValue) == typeof(string))
        {
            return enumValueAttribute is null 
                ? (TValue)(object)enumValue.ToString() 
                : valueGetter(enumValueAttribute);
        }

        if (enumValueAttribute is null)
        {
            // The enum value uses to invoke this extension method is missing the specific attribute declaration,
            //   where the attribute has a property of type TValue.
            // Practically speaking, this means that a IssuerIdentificationNumber enum value is missing a CountryAttribute,
            //   since the code above handles the case where the property value is of type string. Country attribute has
            //   a single property of type Country, another enum type.
            throw new InvalidOperationException($"Enum value {enumValue.GetType().FullName}.{enumValue} is missing a {typeof(TAttribute).FullName}.");
        }

        return valueGetter(enumValueAttribute);
    }

    internal static string? ReplaceEmptyWithNull(this string data)
        => string.IsNullOrEmpty(data) 
        ? null 
        : data;

    internal static string ConvertToHex(this string value)
        => $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(value))}";

    internal static string ConvertToHex(this char value)
        => $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(new[] { value }))}";

    internal static bool EqualsIgnoreCase(this string source, string value)
        => source.Equals(value, StringComparison.OrdinalIgnoreCase);
}
