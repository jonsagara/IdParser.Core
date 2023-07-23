using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace IdParser.Core.Benchmarks.Runners;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class SubstringTrimVsSliceTrimToStringHarness
{
    private readonly string _licenseText;

    public SubstringTrimVsSliceTrimToStringHarness()
    {
        _licenseText = "DDKMONKEY";

    }

    [Benchmark(Baseline = true)]
    public void SubstringTrim()
        => _licenseText.Substring(startIndex: 3).Trim();

    [Benchmark]
    public void SliceTrimToString()
        => _licenseText.AsSpan(start: 3).Trim().ToString();
}
