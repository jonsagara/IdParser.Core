namespace IdParser.Core.Static.Parsers.Id;

internal record GivenNameParts(string First, string? Middle);

/// <summary>
/// This appears to be from an old AAMVA version: 2003-2005
/// </summary>
internal static class GivenNameParser
{
    private static readonly char[] _splits = new[] { ',', '$', ' ' };

    internal static GivenNameParts Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var givenNames = input.Split(_splits);
        var first = givenNames[0].Trim();
        var middle = givenNames.Length > 1 ? givenNames[1].Trim().ReplaceEmptyWithNull() : null;

        return new GivenNameParts(First: first, Middle: middle);
    }
}
