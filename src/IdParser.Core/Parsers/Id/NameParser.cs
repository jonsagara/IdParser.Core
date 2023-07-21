namespace IdParser.Core.Parsers.Id;

internal record NameParts(string? First, string? Middle, string? Last, string? Suffix);

internal static class NameParser
{
    private static readonly char[] StandardSeparators = { ',', '$', '@' };
    private static readonly string[] Suffixes = { "JR", "SR", "1ST", "2ND", "3RD", "4TH", "5TH", "6TH", "7TH", "8TH", "9TH" };
    private const char SpaceSeparator = ' ';

    // AAMVA 2000
    internal static NameParts? Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        // According to A.2.1 (AAMVA Person Name Rule) in the D20 Data Dictionary,
        // last names can only contain alphabetic characters and up to one embedded hyphen.
        // First names can only contain alphabetic characters. All other characters, such as spaces,
        // are deleted and not encoded. Middle names are allowed to have spaces.
        // Therefore, the first and last name should only ever be one name part while the middle name
        // can consume the remaining name parts. If a jurisdiction doesn't follow the standard, there
        // is nothing more we can do. This isn't a natural language parser, so we can only go with our best attempt.
        // As such, don't throw if we can't parse.

        // Jurisdictions that (mostly) follow the AAMVA 2000 standard
        if (input.IndexOfAny(StandardSeparators) >= 0)
        {
            return ParseWithStandardSeparators(input);
        }

        // Jurisdictions like Pennsylvania that use non-standard separators
        if (input.Contains(SpaceSeparator, StringComparison.Ordinal))
        {
            return ParseWithSpaceSeparator(input);
        }

        return null;
    }

    /// <summary>
    /// Parses names separated by standard separators (e.g. PUBLIC,JOHN,Q)
    /// </summary>
    private static NameParts ParseWithStandardSeparators(string input)
    {
        // Some jurisdictions separate the first and middle names with a space
        var names = input.Split(StandardSeparators)
                         .SelectMany(n => n.Trim().Split(SpaceSeparator))
                         .ToList();

        var suffix = ParseSuffix(names);

        var last = names.Count > 0 ? names[0].Trim().ReplaceEmptyWithNull() : null;
        var first = names.Count > 1 ? names[1].Trim().ReplaceEmptyWithNull() : null;
        var middle = names.Count > 2 ? string.Join(" ", names.Skip(2).Select(n => n.ReplaceEmptyWithNull()).Where(n => n != null)).ReplaceEmptyWithNull() : null;

        return new NameParts(First: first, Middle: middle, Last: last, Suffix: suffix);
    }

    /// <summary>
    /// Parses names separated by non-standard space separators (e.g. JOHN Q PUBLIC)
    /// </summary>
    private static NameParts ParseWithSpaceSeparator(string input)
    {
        var names = input.Split(SpaceSeparator).ToList();

        var suffix = ParseSuffix(names);
        
        var first = names.Count > 0 ? names[0].Trim().ReplaceEmptyWithNull() : null;
        string? middle = null;
        string? last = null;

        if (names.Count == 2)
        {
            last = names[1].Trim().ReplaceEmptyWithNull();
        }
        else if (names.Count > 2)
        {
            middle = string.Join(" ", names.Skip(1).Take(names.Count - 2).Select(n => n.ReplaceEmptyWithNull()).Where(n => n != null)).ReplaceEmptyWithNull();
            last = names.Last().Trim().ReplaceEmptyWithNull();
        }

        return new NameParts(First: first, Middle: middle, Last: last, Suffix: suffix);
    }

    /// <summary>
    /// Parse the few suffixes allowed by AAMVA. Any other non-standard suffix (e.g. ESQ)
    /// will not be parsed and set in the <see cref="Name.Suffix"/> property.
    /// </summary>
    private static string? ParseSuffix(List<string> names)
    {
        var suffixCandidate = names.Last();

        if (Suffixes.Contains(RemovePunctuation(suffixCandidate)))
        {
            names.RemoveAt(names.Count - 1);
            return suffixCandidate;
        }

        return null;
    }

    private static string RemovePunctuation(string input) 
        => new string(input.Where(c => !char.IsPunctuation(c)).ToArray());
}
