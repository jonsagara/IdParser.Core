namespace IdParser.Core;

internal static class Fixes
{
    /// <summary>
    /// If the header is invalid, try to correct it. Otherwise, return the string as-is.
    /// </summary>
    internal static string TryToCorrectHeader(string input)
    {
        return input
            .RemoveUndefinedCharacters()
            .RemoveInvalidCharactersFromHeader()
            .FixIncorrectHeader()
            .RemoveIncorrectCarriageReturns();
    }


    /// <remarks>
    /// Characters in the hex range [0x80, 0x9F].
    /// </remarks>
    private static readonly string[] UndefinedCharacters = GetUndefinedCharacters();

    private static string[] GetUndefinedCharacters()
    {
        List<string> undefinedCharacters = new();

        for (int i = 0x80; i <= 0x9F; i++)
        {
            undefinedCharacters.Add(((char)i).ToString());
        }

        return undefinedCharacters.ToArray();
    }

    /// <summary>
    /// The DS4308 scanner using HID keyboard emulation tends to insert undefined characters (0xC2,0x80 through 0xC2,0x9F)
    /// at the start and end of every subfile record. That throws off the parsing of the offsets in the header.
    /// This removes the undefined noise characters.
    /// </summary>
    /// <remarks>
    /// https://www.aamva.org/identity/issuer-identification-numbers-(iin)
    /// </remarks>
    private static string RemoveUndefinedCharacters(this string input)
    {
        foreach (var undefinedCharacter in UndefinedCharacters)
        {
            input = input.Replace(undefinedCharacter, "", StringComparison.Ordinal);
        }

        return input;
    }

    /// <summary>
    /// Sometimes bad characters (e.g. @a ANSI) get into the header (usually through HID keyboard emulation).
    /// Replace the header with what we are expecting.
    /// </summary>
    private static string RemoveInvalidCharactersFromHeader(this string input)
    {
        input = input.TrimStart();

        if (input[0] != '@')
        {
            // Text doesn't start with an input. Don't try to parse it further. Return it as-is.
            return input;
        }

        if (input.StartsWith(Barcode.ExpectedHeader, StringComparison.Ordinal))
        {
            // The header is already valid. Return the text without modifications.
            return input;
        }

        // AAMVA 2003+
        var ixANSI = input.IndexOf(Barcode.ExpectedFileType, StringComparison.Ordinal);

        if (ixANSI >= 0)
        {
            // The string "ANSI " exists in the text. Starting with the expected header value, append everything from
            //   the input string after the "ANSI " text.
            // This ensures that the input text has a valid header.
            return string.Concat(Barcode.ExpectedHeader, input.AsSpan(start: ixANSI + Barcode.ExpectedFileType.Length));
        }

        // AAMVA 2000 and earlier
        const string AAMVA = "AAMVA";
        var aamvaPosition = input.IndexOf(AAMVA, StringComparison.Ordinal);

        if (aamvaPosition >= 0)
        {
            // Earlier versions of the spec must have had "AMMVA" instead of "ANSI " in the header. Starting with 
            //   the current expected header value, append everything from the input string after the "AMMVA" text.
            // This ensures that the input text has a valid header.
            return string.Concat(Barcode.ExpectedHeader, input.AsSpan(start: aamvaPosition + AAMVA.Length));
        }

        // Unknown header, as it doesn't contain the expected file type. Return it as-is.
        return input;
    }

    /// <summary>
    /// HID keyboard emulation, especially entered via a web browser, tends to mutilate the header.
    /// As long as part of the header is correct, this will fix the rest of it to make it parse-able.
    /// </summary>
    private static string FixIncorrectHeader(this string input)
    {
        if (input[0] == Barcode.ExpectedComplianceIndicator &&
            input[1] == Barcode.ExpectedSegmentTerminator &&
            input[2] == Barcode.ExpectedDataElementSeparator &&
            input[3] == Barcode.ExpectedRecordSeparator &&
            input[4] == 'A')
        {
            return input.Insert(startIndex: 4, value: Barcode.ExpectedSegmentTerminator.ToString() + Barcode.ExpectedDataElementSeparator);
        }

        return input;
    }

    /// <summary>
    /// HID keyboard emulation (and some other methods) tend to replace the \r with \r\n, which is invalid and doesn't 
    /// conform to the AAMVA standard. This fixes it before attempting to parse the fields.
    /// </summary>
    private static string RemoveIncorrectCarriageReturns(this string input)
    {
        if (input.Contains("\r\n", StringComparison.Ordinal))
        {
            // Input contains CRLFs (\r\n). Remove all CRs (\r).
            var inputWithoutCRs = input.Replace("\r", string.Empty, StringComparison.Ordinal);

            // Add back the one CR (\r) that is required in the header.
            return $"{inputWithoutCRs.Substring(0, 3)}\r{inputWithoutCRs.Substring(4)}";
        }

        return input;
    }
}
