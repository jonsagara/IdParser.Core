namespace IdParser.Core;

/// <summary>
/// Details about failure to parse or extract a meaningful value from an element's raw value obtained from 
/// the scanned ID text.
/// </summary>
/// <param name="ElementId">The 3-character element ID.</param>
/// <param name="RawValue">The element's raw value from the scanned ID text.</param>
/// <param name="Error">A message describing the error that occurred.</param>
public record ElementParseError(string ElementId, string? RawValue, string Error);
