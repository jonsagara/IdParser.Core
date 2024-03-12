namespace IdParser.Core;

/// <summary>
/// Details about failure to parse or extract a meaningful value from an element's raw value obtained from 
/// the scanned ID text.
/// </summary>
/// <remarks>
/// NOTE: Name ParseError instead of Error because otherwise the compiler complains: CA1716: Rename type 
/// Error so that it no longer conflicts with the reserved language keyword 'Error'. Using a reserved keyword 
/// as the name of a type makes it harder for consumers in other languages to use the type.
/// </remarks>
/// <param name="Message">A message describing the error that occurred.</param>
/// <param name="ElementId">If element-specific, the 3-character element ID; otherwise, null.</param>
/// <param name="RawValue">If element-specific, the element's raw value from the scanned ID text; otherwise, null.</param>
public record ParseError(string Message, string? ElementId, string? RawValue); 
