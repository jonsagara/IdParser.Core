using System.Text;

namespace IdParser.Core;

public static class IdParserExtensions
{
    /// <summary>
    /// If <paramref name="data"/> is an empty string, return null. Otherwise, return data as-is.
    /// </summary>
    internal static string? ReplaceEmptyWithNull(this string data)
        => string.IsNullOrEmpty(data)
        ? null
        : data;

    /// <summary>
    /// If <paramref name="data"/> is null or white space, return null. Otherwise, return data as-is.
    /// </summary>
    internal static string? ToNullIfWhiteSpace(this string? data)
        => string.IsNullOrWhiteSpace(data)
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
    internal static string ToHexString(this char value)
        => $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(new[] { value }))}";

    /// <summary>
    /// Do an ordinal string comparison, ignoring case.
    /// </summary>
    internal static bool EqualsIgnoreCase(this string source, string value)
        => source.Equals(value, StringComparison.OrdinalIgnoreCase);
}
