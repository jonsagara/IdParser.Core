namespace IdParser.Core;

/// <summary>
/// Represents the height of the person identified in the ID card.
/// Heights are approximated when converting between Metric and Imperial units.
/// </summary>
public record Height
{
    private const double CentimetersPerInch = 2.54;
    private const byte InchesPerFoot = 12;

    public double Centimeters { get; private set; }
    public bool IsMetric { get; private set; }

    public Height(double centimeters)
    {
        Centimeters = centimeters;
        IsMetric = true;
    }

    public Height(int inches)
    {
        Centimeters = inches * CentimetersPerInch;
        IsMetric = false;
    }

    public Height(int feet, int inches)
        : this(inches: feet * InchesPerFoot + inches)
    {
    }
}
