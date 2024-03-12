namespace IdParser.Core;

/// <summary>
/// Details about failure to parse or extract a meaningful value from an element's raw value obtained from 
/// the scanned ID text.
/// </summary>
/// <param name="Error">A message describing the error that occurred.</param>
/// <param name="ElementId">If element-specific, the 3-character element ID; otherwise, null.</param>
/// <param name="RawValue">If element-specific, the element's raw value from the scanned ID text; otherwise, null.</param>
public record ParseError(string Error, string? ElementId, string? RawValue);
