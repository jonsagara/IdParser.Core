namespace IdParser.Core;

/// <summary>
/// Any element with an unrecognized 3-character element ID and its associated value.
/// </summary>
/// <param name="ElementId">The 3-character element ID.</param>
/// <param name="RawValue">The raw value from the scanned ID text.</param>
public record UnhandledElement(string ElementId, string? RawValue);
