﻿namespace IdParser.Core;

public enum Validation
{
    /// <summary>
    /// Don't throw if the header is invalid. Instead, try to fix it to make it valid.
    /// </summary>
    None,

    /// <summary>
    /// Throw if the header is invalid.
    /// </summary>
    Strict,
}
