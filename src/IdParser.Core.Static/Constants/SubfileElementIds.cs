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

    /// <summary>
    /// Compliance Type
    /// </summary>
    /// <remarks>
    /// <para>DHS required field that indicates compliance: &quot;F&quot; = compliant; and, &quot;N&quot; = non-compliant.</para>
    /// <para>F1A</para>
    /// </remarks>
    internal const string ComplianceType = "DDA";

    /// <summary>
    /// Date of Birth
    /// </summary>
    /// <remarks>
    /// <para>Date on which the cardholder was born. (MMDDCCYY for U.S., CCYYMMDD for Canada)</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string DateOfBirth = "DBB";

    /// <summary>
    /// Document Discriminator
    /// </summary>
    /// <remarks>
    /// <para>Number must uniquely identify a particular document issued to that customer from others that may have been issued in 
    /// the past.This number may serve multiple purposes of document discrimination, audit information number, and/or inventory 
    /// control.</para>
    /// <para>V25ANS</para>
    /// </remarks>
    internal const string DocumentDiscriminator = "DCF";

    /// <summary>
    /// Race / ethnicity
    /// </summary>
    /// <remarks>
    /// <para>Codes for race or ethnicity of the cardholder, as defined in AAMVA D20.</para>
    /// <para>V3A</para>
    /// </remarks>
    internal const string Ethnicity = "DCL";

    /// <summary>
    /// Document Expiration Date
    /// </summary>
    /// <remarks>
    /// <para>Date on which the driving and identification privileges granted by the document are no longer valid. 
    /// (MMDDCCYY for U.S., CCYYMMDD for Canada)</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string ExpirationDate = "DBA";

    /// <summary>
    /// Physical Description – Eye Color
    /// </summary>
    /// <remarks>
    /// <para>Color of cardholder's eyes. (ANSI D-20 codes)</para>
    /// <para>F3A</para>
    /// </remarks>
    internal const string EyeColor = "DAY";

    /// <summary>
    /// Customer First Name
    /// </summary>
    /// <remarks>
    /// <para>First name of the cardholder.</para>
    /// <para>V40ANS</para>
    /// </remarks>
    internal const string FirstName = "DAC";

    /// <summary>
    /// Not in the 2020 spec. Comment said AAMVA 2003-2005.
    /// </summary>
    internal const string GivenName = "DCT";

    /// <summary>
    /// Hair color
    /// </summary>
    /// <remarks>
    /// <para>Bald, black, blonde, brown, gray, red/auburn, sandy, white, unknown. If the issuing jurisdiction wishes to 
    /// abbreviate colors, the three-character codes provided in AAMVA D20 must be used.</para>
    /// <para>V12A</para>
    /// </remarks>
    internal const string HairColor = "DAZ";

    /// <summary>
    /// Limited Duration Document Indicator
    /// </summary>
    /// <remarks>
    /// <para>DHS required field that indicates that the cardholder has temporary lawful status = &quot;1&quot;.</para>
    /// <para>F1N</para>
    /// </remarks>
    internal const string HasTemporaryLawfulStatus = "DDD";

    /// <summary>
    /// Physical Description – Height
    /// </summary>
    /// <remarks>
    /// <para>Height of cardholder.</para>
    /// <para>Inches(in) : number of inches followed by &quot; in&quot;</para>
    /// <para>ex. 6'1&quot; = &quot;073 in&quot;</para>
    /// <para>Centimeters(cm) : number of centimeters followed by &quot; cm&quot;</para>
    /// <para>ex. 181 centimeters=&quot;181 cm&quot;</para>
    /// <para>F6ANS</para>
    /// </remarks>
    internal const string Height = "DAU";

    /// <summary>
    /// Customer ID Number
    /// </summary>
    /// <remarks>
    /// <para>The number assigned or calculated by the issuing authority.</para>
    /// <para>V25ANS</para>
    /// </remarks>
    internal const string IdNumber = "DAQ";

    /// <summary>
    /// Inventory control number
    /// </summary>
    /// <remarks>
    /// <para>A string of letters and/or numbers that is affixed to the raw materials (card stock, laminate, etc.) used in 
    /// producing driver licenses and ID cards. (DHS recommended field)</para>
    /// <para>V25ANS</para>
    /// </remarks>
    internal const string InventoryControlNumber = "DCK";
}
