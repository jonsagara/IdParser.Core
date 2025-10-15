using System.Reflection;
using IdParser.Core.Attributes;

namespace IdParser.Core.Test.Metadata;

public class AbbreviationAttributeTests : BaseTest
{
    [Fact]
    public void AllCountryValuesHaveAnAbbreviationAttribute()
    {
        EnsureAllEnumValuesHaveAttribute<Country, AbbreviationAttribute>();
    }


    //
    // Private methods
    //

    private void EnsureAllEnumValuesHaveAttribute<TEnum, TAttribute>()
        where TEnum : struct, Enum
        where TAttribute : Attribute
    {
        var enumValues = Enum.GetValues<TEnum>();
        var enumValueType = typeof(TEnum);

        foreach (var enumValue in enumValues)
        {
            var abbreviationAttr = enumValueType
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();

            Assert.True(abbreviationAttr is not null, $"{enumValueType.FullName} enum value {enumValue} is not decorated with an {typeof(TAttribute).FullName}.");
        }
    }
}
