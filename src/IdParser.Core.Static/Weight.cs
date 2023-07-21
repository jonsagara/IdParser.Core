namespace IdParser.Core.Static;

/// <summary>
/// Represents the weight of the person identified in the ID card.
/// The level of detail provided varies by jurisdiction, as some provide
/// no information on weight, others give a range, and some give an exact measurement.
/// </summary>
public record Weight
{
    private const double PoundsPerKilogram = 2.20462262;

    public WeightRange? WeightRange { get; internal set; }
    public double? Kilograms { get; private set; }
    public bool IsMetric { get; private set; }

    public Weight(WeightRange weightRange)
    {
        WeightRange = weightRange;
        IsMetric = false;
    }

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


    private static double PoundsToKilograms(int pounds)
         => pounds / PoundsPerKilogram;
}
