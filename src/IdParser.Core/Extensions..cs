using System.Text;

namespace IdParser.Core;

public static class Extensions
{
    internal static string? ReplaceEmptyWithNull(this string data)
        => string.IsNullOrEmpty(data)
        ? null
        : data;

    /// <summary>
    /// UTF8-encode the string and convert the bytes to hex. Prefix the return string with 0x.
    /// </summary>
    internal static string ToHexString(this string value)
        => $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(value))}";

    /// <summary>
    /// UTF8-encode the character and convert the bytes to hex. Prefix the return string with 0x.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static string ToHexString(this char value)
        => $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(new[] { value }))}";

    internal static bool EqualsIgnoreCase(this string source, string value)
        => source.Equals(value, StringComparison.OrdinalIgnoreCase);
}
