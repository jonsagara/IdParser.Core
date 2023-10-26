namespace IdParser.Core;

/// <summary>
/// Represents the weight of the person identified in the ID card.
/// The level of detail provided varies by jurisdiction, as some provide
/// no information on weight, others give a range, and some give an exact measurement.
/// </summary>
public record Weight
{
    private const double PoundsPerKilogram = 2.20462262;

    public int Pounds { get; private set; }
    public double Kilograms { get; private set; }
    public bool IsMetric { get; private set; }

    public Weight(double kilograms)
    {
        IsMetric = true;
        Kilograms = kilograms;
        Pounds = KilogramsToPounds(kilograms: kilograms);
    }

    public Weight(int pounds)
    {
        IsMetric = false;
        Kilograms = PoundsToKilograms(pounds);
        Pounds = pounds;
    }

    public override string ToString()
        => IsMetric
        ? $"{Kilograms} kg"
        : $"{Pounds} lbs";


    //
    // Private methods
    //

    private static double PoundsToKilograms(int pounds)
         => pounds / PoundsPerKilogram;

    private static int KilogramsToPounds(double kilograms)
        => (int)Math.Round(kilograms * PoundsPerKilogram, digits: 0);
}
