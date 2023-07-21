namespace IdParser.Core;

/// <summary>
/// Represents the weight of the person identified in the ID card.
/// The level of detail provided varies by jurisdiction, as some provide
/// no information on weight, others give a range, and some give an exact measurement.
/// </summary>
public record Weight
{
    private const double PoundsPerKilogram = 2.20462262;

    public double? Kilograms { get; private set; }
    public bool IsMetric { get; private set; }

    public Weight(double kilograms)
    {
        Kilograms = kilograms;
        IsMetric = true;
    }

    public Weight(int pounds)
    {
        Kilograms = PoundsToKilograms(pounds);
        IsMetric = false;
    }

    public override string ToString()
        => IsMetric
        ? $"{Kilograms} kg"
        : $"{KilogramsToPounds(kilograms: Kilograms!.Value)} lbs";


    //
    // Private methods
    //

    private static double PoundsToKilograms(int pounds)
         => pounds / PoundsPerKilogram;

    private static int KilogramsToPounds(double kilograms)
        => (int)Math.Round(kilograms * PoundsPerKilogram, digits: 0);
}
