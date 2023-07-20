using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace IdParser.Core.Benchmarks.Runners;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class DriversLicenseHarness
{
    private readonly string _licenseText;

    public DriversLicenseHarness()
    {
        Console.WriteLine("Reading File...");
        _licenseText = File.ReadAllText(Path.Combine(@"data\Licenses\CA.txt"));
        Console.WriteLine("Done Reading File.");
    }

    [Benchmark(Baseline = true)]
    public void Instance_ParseLicense()
        => Barcode.Parse(_licenseText, Validation.None);
}
