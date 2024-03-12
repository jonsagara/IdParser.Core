namespace IdParser.Core;

/// <summary>
/// Contains the result of parsing a scanned ID: the ID card, a collection of unhandled fields, and any
/// field-level parsing errors that occurred.
/// </summary>
public class BarcodeParseResult
{
    /// <summary>
    /// Contains values of any elements extracted from the scanned ID text.
    /// </summary>
    public IdentificationCard Card { get; }

    /// <summary>
    /// Contains any unhandled element IDs and their raw values.
    /// </summary>
    public IReadOnlyCollection<UnhandledElement> UnhandledElements { get; }

    /// <summary>
    /// Contains element-level errors that occurred while trying to extract a meaningful value from the element's raw value.
    /// </summary>
    public IReadOnlyCollection<ElementParseError> ElementParseErrors { get; }


    internal BarcodeParseResult(IdentificationCard card, IReadOnlyCollection<UnhandledElement> unhandledElements, IReadOnlyCollection<ElementParseError> elementParseErrors)
    {
        ArgumentNullException.ThrowIfNull(card);
        ArgumentNullException.ThrowIfNull(unhandledElements);
        ArgumentNullException.ThrowIfNull(elementParseErrors);

        Card = card;
        UnhandledElements = unhandledElements;
        ElementParseErrors = elementParseErrors;
    }
}
