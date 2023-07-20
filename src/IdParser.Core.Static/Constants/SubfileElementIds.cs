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
    internal const string Country = "DCG";

    /// <summary>
    /// Alias / AKA Given Name
    /// </summary>
    /// <remarks>
    /// <para>Other given name by which cardholder is known.</para>
    /// <para>V15ANS</para>
    /// </remarks>
    internal const string AliasFirstName = "DBG";

    /// <summary>
    /// Alias / AKA Family Name
    /// </summary>
    /// <remarks>
    /// <para>Other family name by which cardholder is known.</para>
    /// <para>V10ANS</para>
    /// </remarks>
    internal const string AliasLastName = "DBN";

    /// <summary>
    /// Alias / AKA Suffix Name
    /// </summary>
    /// <remarks>
    /// <para>Other suffix by which cardholder is known.</para>
    /// <para>V5ANS</para>
    /// </remarks>
    internal const string AliasSuffix = "DBS";

    /// <summary>
    /// Audit information
    /// </summary>
    /// <remarks>
    /// <para>A string of letters and/or numbers that identifies when, where, and by whom a driver license/ID card 
    /// was made.If audit information is not used on the card or the MRT, it must be included in the driver record.</para>
    /// <para>V25ANS</para>
    /// </remarks>
    internal const string AuditInformation = "DCJ";

    /// <summary>
    /// Address – City
    /// </summary>
    /// <remarks>
    /// <para>City portion of the cardholder address.</para>
    /// <para>V20ANS</para>
    /// </remarks>
    internal const string City = "DAI";
}
