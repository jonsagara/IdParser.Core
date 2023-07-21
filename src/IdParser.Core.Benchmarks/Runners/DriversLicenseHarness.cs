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
        _licenseText = File.ReadAllText(Path.Combine(@"data\Licenses\CA.txt"));
    }

    [Benchmark(Baseline = true)]
    public void Instance_ParseLicense()
        => Barcode.Parse(_licenseText, Validation.None);

    [Benchmark]
    public void Static_ParseLicense()
        => Static.Barcode.Parse(_licenseText, Static.Validation.None);
}
