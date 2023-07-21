namespace IdParser.Core;

/// <summary>
/// Represents the address of the person identified in the ID card.
/// </summary>
public class Address
{
    public string StreetLine1 { get; set; } = null!;
    public string? StreetLine2 { get; set; }
    public string City { get; set; } = null!;
    public string JurisdictionCode { get; set; } = null!;
    public string? PostalCode { get; set; }
    public Country Country { get; set; }

    public string? PostalCodeDisplay
    {
        get
        {
            if (PostalCode is null)
            {
                return null;
            }

            if (Country == Country.Usa && PostalCode.Length > 5)
            {
                return $"{PostalCode.Substring(0, 5)}-{PostalCode.Substring(5)}";
            }

            if (Country == Country.Canada && PostalCode.Length == 6)
            {
                return $"{PostalCode.Substring(0, 3)} {PostalCode.Substring(3)}";
            }

            return PostalCode;
        }
    }

    //public override string ToString()
    //    => StreetLine2 is null
    //    ? $"{StreetLine1}{Environment.NewLine}{City}, {JurisdictionCode} {PostalCodeDisplay}"
    //    : $"{StreetLine1}{Environment.NewLine}{StreetLine2}{Environment.NewLine}{City}, {JurisdictionCode} {PostalCodeDisplay}";
}
