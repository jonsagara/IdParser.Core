namespace IdParser.Core;

/// <summary>
/// Represents the height of the person identified in the ID card.
/// Heights are approximated when converting between Metric and Imperial units.
/// </summary>
public record Height
{
    private const double CentimetersPerInch = 2.54;
    private const double InchesPerCentimeter = 1.0 / 2.54;
    private const int InchesPerFoot = 12;

    private double _centimeters;
    private int _feet;
    private int _inches;
    private int _totalInches;

    /// <summary>
    /// True if the scanned value was given in centimeters; false otherwise (scanned 
    /// value was in feet/inches, or just inches).
    /// </summary>
    public bool ParsedAsMetric { get; }

    /// <summary>
    /// Returns the height in centimeters.
    /// </summary>
    public double Centimeters
        => _centimeters;

    /// <summary>
    /// Return the feet component of the height.
    /// </summary>
    public int Feet
        => _feet;

    /// <summary>
    /// Return the inches component of the height.
    /// </summary>
    public int Inches
        => _inches;

    /// <summary>
    /// Returns the height in total inches.
    /// </summary>
    public int TotalInches
        => _totalInches;


    /// <summary>
    /// Construct an instance with centimeters.
    /// </summary>
    public Height(double centimeters)
    {
        ParsedAsMetric = true;

        _centimeters = centimeters;

        // These are approximations.
        var feetInches = CalculateFeetAndInchesFromCentimeters(centimeters);
        _feet = feetInches.Feet;
        _inches = feetInches.Inches;
        _totalInches = CalculateTotalInches(feet: feetInches.Feet, inches: feetInches.Inches);
    }

    /// <summary>
    /// Construct an instance with total inches.
    /// </summary>
    public Height(int totalInches)
        : this(feet: totalInches / InchesPerFoot, inches: totalInches % InchesPerFoot)
    { }

    /// <summary>
    /// Construct an instance with feet and inches.
    /// </summary>
    public Height(int feet, int inches)
    {
        ParsedAsMetric = false;

        _feet = feet;
        _inches = inches;
        _totalInches = CalculateTotalInches(feet: feet, inches: inches);

        // This is an approximation.
        _centimeters = CalculateCentimetersFromTotalInches(totalInches: _totalInches);
    }


    //
    // Private methods
    //

    private static int CalculateTotalInches(int feet, int inches)
        => feet * InchesPerFoot + inches;

    private static double CalculateCentimetersFromTotalInches(int totalInches)
        => totalInches * CentimetersPerInch;

    private static FeetInches CalculateFeetAndInchesFromCentimeters(double centimeters)
    {
        var totalInches = centimeters * InchesPerCentimeter;
        var feet = (int)Math.Floor(totalInches / InchesPerFoot);
        var inches = (int)Math.Round(totalInches % InchesPerFoot);

        if (inches == InchesPerFoot)
        {
            feet += 1;
            inches = 0;
        }

        return new FeetInches(Feet: feet, Inches: inches);
    }


    //
    // Types
    //

    private record struct FeetInches(int Feet, int Inches);
}
