namespace IdParser.Core.Constants;

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

    /// <summary>
    /// Organ Donor Indicator
    /// </summary>
    /// <remarks>
    /// <para>Field that indicates that the cardholder is an organ donor = &quot;1&quot;.</para>
    /// <para>F1N</para>
    /// </remarks>
    internal const string IsOrganDonor = "DDK";

    /// <summary>
    /// Organ Donor Indicator from a pre-2020 spec.
    /// </summary>
    internal const string IsOrganDonorLegacy = "DBH";

    /// <summary>
    /// Document Issue Date
    /// </summary>
    /// <remarks>
    /// <para>Date on which the document was issued. (MMDDCCYY for U.S., CCYYMMDD for Canada)</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string IssueDate = "DBD";

    /// <summary>
    /// Veteran Indicator
    /// </summary>
    /// <remarks>
    /// <para>Field that indicates that the cardholder is a veteran = &quot;1&quot;.</para>
    /// <para>F1N</para>
    /// </remarks>
    internal const string IsVeteran = "DDL";

    /// <summary>
    /// Address – Jurisdiction Code
    /// </summary>
    /// <remarks>
    /// <para>State portion of the cardholder address.</para>
    /// <para>F2A</para>
    /// </remarks>
    internal const string JurisdictionCode = "DAJ";

    /// <summary>
    /// Customer Family Name
    /// </summary>
    /// <remarks>
    /// <para>Family name of the cardholder. (Family name is sometimes also called &quot;last name&quot; 
    /// or &quot;surname.&quot;) Collect full name for record, print as many characters as possible on 
    /// portrait side of DL/ID.</para>
    /// <para>V40ANS</para>
    /// </remarks>
    internal const string LastName = "DCS";

    /// <summary>
    /// Customer Middle Name(s)
    /// </summary>
    /// <remarks>
    /// <para>Middle name(s) of the cardholder. In the case of multiple middle names they shall be separated 
    /// by a comma &quot;,&quot;.</para>
    /// <para>V40ANS</para>
    /// </remarks>
    internal const string MiddleName = "DAD";

    /// <summary>
    /// Name for 2000 spec. Legacy.
    /// </summary>
    internal const string Name = "DAA";

    /// <summary>
    /// Name Suffix
    /// </summary>
    /// <remarks>
    /// <para>Name Suffix (If jurisdiction participates in systems requiring name suffix(PDPS, CDLIS, etc.), 
    /// the suffix must be collected and displayed on the DL/ID and in the MRT). Collect full name for record, 
    /// print as many characters as possible on portrait side of DL/ID.</para>
    /// <para>JR (Junior); SR (Senior); 1ST or I (First); 2ND or II (Second); 3RD or III (Third); 4TH or IV (Fourth);
    /// 5TH or V (Fifth); 6TH or VI (Sixth); 7TH or VII (Seventh); 8TH or VIII (Eighth); 9TH or IX (Ninth).</para>
    /// <para></para>
    /// </remarks>
    internal const string NameSuffix = "DCU";

    /// <summary>
    /// Place of birth
    /// </summary>
    /// <remarks>
    /// <para>Country and municipality and/or state/province.</para>
    /// <para>V33A</para>
    /// </remarks>
    internal const string PlaceOfBirth = "DCI";

    /// <summary>
    /// Address – Postal Code
    /// </summary>
    /// <remarks>
    /// <para>Postal code portion of the cardholder address in the U.S.and Canada.If the trailing portion of the 
    /// postal code in the U.S. is not known, zeros will be used to fill the trailing set of numbers up to nine 
    /// (9) digits.</para>
    /// <para>F11ANS</para>
    /// </remarks>
    internal const string PostalCode = "DAK";

    /// <summary>
    /// Card Revision Date
    /// </summary>
    /// <remarks>
    /// <para>DHS required field that indicates date of the most recent version change or modification to the 
    /// visible format of the DL/ID. (MMDDCCYY for U.S., CCYYMMDD for Canada).</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string RevisionDate = "DDB";

    /// <summary>
    /// Physical Description – Sex
    /// </summary>
    /// <remarks>
    /// <para>Gender of the cardholder. 1 = male, 2 = female, 9 = not specified.</para>
    /// <para>F1N</para>
    /// </remarks>
    internal const string Sex = "DBC";

    /// <summary>
    /// Address – Street 1
    /// </summary>
    /// <remarks>
    /// <para>Street portion of the cardholder address.</para>
    /// <para>V35ANS</para>
    /// </remarks>
    internal const string StreetLine1 = "DAG";

    /// <summary>
    /// Address – Street 1 from pre-2020 spec.
    /// </summary>
    internal const string StreetLine1Legacy = "DAL";

    /// <summary>
    /// Address – Street 2
    /// </summary>
    /// <remarks>
    /// <para>Second line of street portion of the cardholder address.</para>
    /// <para>V35ANS</para>
    /// </remarks>
    internal const string StreetLine2 = "DAH";

    /// <summary>
    /// Under 18 Until
    /// </summary>
    /// <remarks>
    /// <para>Date on which the cardholder turns 18 years old. (MMDDCCYY for U.S., CCYYMMDD for Canada).</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string Under18Until = "DDH";

    /// <summary>
    /// Under 19 Until
    /// </summary>
    /// <remarks>
    /// <para>Date on which the cardholder turns 19 years old. (MMDDCCYY for U.S., CCYYMMDD for Canada).</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string Under19Until = "DDI";

    /// <summary>
    /// Under 21 Until
    /// </summary>
    /// <remarks>
    /// <para>Date on which the cardholder turns 21 years old. (MMDDCCYY for U.S., CCYYMMDD for Canada).</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string Under21Until = "DDJ";

    /// <summary>
    /// First name truncation
    /// </summary>
    /// <remarks>
    /// <para>A code that indicates whether a field has been truncated(T), has not been truncated(N), or – unknown whether truncated(U).</para>
    /// <para>F1A</para>
    /// </remarks>
    internal const string WasFirstNameTruncated = "DDF";

    /// <summary>
    /// Family name truncation
    /// </summary>
    /// <remarks>
    /// <para>A code that indicates whether a field has been truncated(T), has not been truncated(N), or – unknown whether truncated(U).</para>
    /// <para>F1A</para>
    /// </remarks>
    internal const string WasLastNameTruncated = "DDE";

    /// <summary>
    /// Middle name truncation
    /// </summary>
    /// <remarks>
    /// <para>A code that indicates whether a field has been truncated(T), has not been truncated(N), or – unknown whether truncated(U).</para>
    /// <para>F1A</para>
    /// </remarks>
    internal const string WasMiddleNameTruncated = "DDG";

    /// <summary>
    /// Weight (kilograms)
    /// </summary>
    /// <remarks>
    /// <para>Cardholder weight in kilograms. Ex. 84 kg = &quot;084&quot;.</para>
    /// <para>F3N</para>
    /// </remarks>
    internal const string WeightInKilograms = "DAX";

    /// <summary>
    /// Weight (pounds)
    /// </summary>
    /// <remarks>
    /// <para>Cardholder weight in pounds. Ex. 185 lb = &quot;185&quot;.</para>
    /// <para>F3N</para>
    /// </remarks>
    internal const string WeightInPounds = "DAW";

    /// <summary>
    /// Physical Description – Weight Range
    /// </summary>
    /// <remarks>
    /// <para>Indicates the approximate weight range of the cardholder. See <see cref="Core.WeightRange"/> for descriptions.</para>
    /// <para>F1N</para>
    /// </remarks>
    internal const string WeightRange = "DCE";


    //
    // Drivers License
    //

    /// <summary>
    /// Jurisdiction-specific endorsement code description
    /// </summary>
    /// <remarks>
    /// <para>Text that explains the jurisdiction-specific code(s) that indicates additional driving privileges 
    /// granted to the cardholder beyond the vehicle class.</para>
    /// <para>V50ANS</para>
    /// </remarks>
    internal const string EndorsementCodeDescription = "DCQ";

    /// <summary>
    /// Jurisdiction-specific endorsement codes
    /// </summary>
    /// <remarks>
    /// <para>Jurisdiction-specific codes that represent additional privileges granted to the cardholder beyond 
    /// the vehicle class (such as transportation of passengers, hazardous materials, operation of motorcycles, 
    /// etc.).</para>
    /// <para>V5ANS</para>
    /// </remarks>
    internal const string EndorsementCodes = "DCD";

    /// <summary>
    /// Jurisdiction-specific endorsement codes from pre-2020 spec.
    /// </summary>
    internal const string EndorsementCodesLegacy = "DAT";

    /// <summary>
    /// HAZMAT Endorsement Expiration Date
    /// </summary>
    /// <remarks>
    /// <para>Date on which the hazardous material endorsement granted by the document is no longer valid. 
    /// (MMDDCCYY for U.S., CCYYMMDD for Canada).</para>
    /// <para>F8N</para>
    /// </remarks>
    internal const string HazmatEndorsementExpirationDate = "DDC";

    /// <summary>
    /// Jurisdiction-specific restriction code description
    /// </summary>
    /// <remarks>
    /// <para>Text describing the jurisdiction-specific restriction code(s) that curtail driving privileges.</para>
    /// <para>V50ANS</para>
    /// </remarks>
    internal const string RestrictionCodeDescription = "DCR";

    /// <summary>
    /// Jurisdiction-specific restriction codes
    /// </summary>
    /// <remarks>
    /// <para>Jurisdiction-specific codes that represent restrictions to driving privileges (such as airbrakes, 
    /// automatic transmission, daylight only, etc.).</para>
    /// <para>V12ANS</para>
    /// </remarks>
    internal const string RestrictionCodes = "DCB";

    /// <summary>
    /// Jurisdiction-specific restriction codes from pre-2020 spec.
    /// </summary>
    internal const string RestrictionCodesLegacy = "DAS";

    /// <summary>
    /// Standard endorsement code
    /// </summary>
    /// <remarks>
    /// <para>Standard endorsement code(s) for cardholder. See codes in D20.This data element is a placeholder 
    /// for future efforts to standardize endorsement codes.</para>
    /// <para>F5AN</para>
    /// </remarks>
    internal const string StandardEndorsementCode = "DCN";

    /// <summary>
    /// Standard restriction code
    /// </summary>
    /// <remarks>
    /// <para>Standard restriction code(s) for cardholder. See codes in D20.This data element is a placeholder 
    /// for future efforts to standardize restriction codes.</para>
    /// <para>F12AN</para>
    /// </remarks>
    internal const string StandardRestrictionCode = "DCO";

    /// <summary>
    /// Standard vehicle classification
    /// </summary>
    /// <remarks>
    /// <para>Standard vehicle classification code(s) for cardholder.This data element is a placeholder for 
    /// future efforts to standardize vehicle classifications.</para>
    /// <para>F4AN</para>
    /// </remarks>
    internal const string StandardVehicleClassification = "DCM";

    /// <summary>
    /// Jurisdiction-specific vehicle classification description
    /// </summary>
    /// <remarks>
    /// <para>Text that explains the jurisdiction-specific code(s) for classifications of vehicles cardholder 
    /// is authorized to drive.</para>
    /// <para>V50ANS</para>
    /// </remarks>
    internal const string VehicleClassificationDescription = "DCP";

    /// <summary>
    /// Jurisdiction-specific vehicle class
    /// </summary>
    /// <remarks>
    /// <para>Jurisdiction-specific vehicle class / group code, designating the type of vehicle the cardholder 
    /// has privilege to drive.</para>
    /// <para>V6ANS</para>
    /// </remarks>
    internal const string VehicleClass = "DCA";

    /// <summary>
    /// Jurisdiction-specific vehicle class from pre-2020 spec.
    /// </summary>
    internal const string VehicleClassLegacy = "DAR";
}
