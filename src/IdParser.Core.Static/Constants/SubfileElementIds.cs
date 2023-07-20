namespace IdParser.Core.Static.Constants;

internal static class SubfileElementIds
{
    //
    // Length/type: (A=alpha A-Z, N=numeric 0-9, S=special, F=fixed length, V=variable length).
    //

    /// <summary>
    /// Country Identification.
    /// </summary>
    /// <remarks>
    /// <para>Country in which DL/ID is issued. U.S. = USA, Canada = CAN.</para>
    /// <para>F3A</para>
    /// </remarks>
    internal const string DCG = "DCG";

    /// <summary>
    /// Alias / AKA Given Name
    /// </summary>
    /// <remarks>
    /// <para>Other given name by which cardholder is known.</para>
    /// <para>V15ANS</para>
    /// </remarks>
    internal const string DBG = "DBG";

    /// <summary>
    /// Alias / AKA Family Name
    /// </summary>
    /// <remarks>
    /// <para>Other family name by which cardholder is known.</para>
    /// <para>V10ANS</para>
    /// </remarks>
    internal const string DBN = "DBN";
}
