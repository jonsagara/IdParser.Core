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

    private bool _isMetric;
    private double? _centimeters;
    private int? _feet;
    private int? _inches;
    private FeetInches? _feetAndInches;

    /// <summary>
    /// Returns the height in centimeters.
    /// </summary>
    public double Centimeters
        => ToCentimeters();

    /// <summary>
    /// Returns the height in feet and inches.
    /// </summary>
    public FeetInches FeetAndInches
        => _feetAndInches ??= ToFeetInches();

    /// <summary>
    /// Returns the height in total inches.
    /// </summary>
    public int TotalInches
        => FeetAndInches.Feet * InchesPerFoot + FeetAndInches.Inches;


    /// <summary>
    /// Construct an instance with centimeters.
    /// </summary>
    public Height(double centimeters)
    {
        _centimeters = centimeters;

        _isMetric = true;
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
        _feet = feet;
        _inches = inches;

        _isMetric = false;
    }


    //
    // Private methods
    //

    private double ToCentimeters()
    {
        if (_isMetric)
        {
            return _centimeters!.Value;
        }
        else
        {
            var totalInches = _feet!.Value * InchesPerFoot + _inches!.Value;
            return totalInches * CentimetersPerInch;
        }
    }

    private FeetInches ToFeetInches()
    {
        if (_isMetric)
        {
            var totalInches = _centimeters!.Value * InchesPerCentimeter;
            int feet = (int)Math.Floor(totalInches / InchesPerFoot);
            int inches = (int)Math.Round(totalInches % InchesPerFoot);

            if (inches == InchesPerFoot)
            {
                feet += 1;
                inches = 0;
            }

            return new FeetInches(Feet: feet, Inches: inches);
        }
        else
        {
            return new FeetInches(Feet: _feet!.Value, Inches: _inches!.Value);
        }
    }
}

/// <summary>
/// A person's height in Feet and Inches.
/// </summary>
public record struct FeetInches(int Feet, int Inches);
