using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace IdParser.Core.Benchmarks.Runners;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class SubstringVsSpanHarness
{
    private readonly string _licenseText;

    public SubstringVsSpanHarness()
    {
        _licenseText = File.ReadAllText(Path.Combine(@"data\Licenses\CA.txt"));
    }

    [Benchmark(Baseline = true)]
    public void Substring_ParseLicense()
        => Barcode.Parse(_licenseText, Validation.None);


/* Substring results

// * Summary *

BenchmarkDotNet v0.13.6, Windows 11 (10.0.22621.1992/22H2/2022Update/SunValley2)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 6.0.412
    [Host]   : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
    .NET 6.0 : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2

Job=.NET 6.0  Runtime=.NET 6.0

|                 Method |     Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|----------------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
| Substring_ParseLicense | 14.17 us | 0.185 us | 0.164 us |  1.00 | 4.3945 |  17.99 KB |        1.00 |

// * Hints *
Outliers
    SubstringVsSpanHarness.Substring_ParseLicense: .NET 6.0 -> 1 outlier  was  removed (14.85 us)

// * Legends *
    Mean        : Arithmetic mean of all measurements
    Error       : Half of 99.9% confidence interval
    StdDev      : Standard deviation of all measurements
    Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
    Gen0        : GC Generation 0 collects per 1000 operations
    Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
    Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
    1 us        : 1 Microsecond (0.000001 sec)

// * Diagnostic Output - MemoryDiagnoser *


// ***** BenchmarkRunner: End *****
Run time: 00:00:14 (14.18 sec), executed benchmarks: 1

Global total time: 00:00:18 (18.67 sec), executed benchmarks: 1

*/

/* Using Span with x.Parse instead of Convert.Tox:

// * Summary *

BenchmarkDotNet v0.13.6, Windows 11 (10.0.22621.1992/22H2/2022Update/SunValley2)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 6.0.412
    [Host]   : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
    .NET 6.0 : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2

Job=.NET 6.0  Runtime=.NET 6.0

|                 Method |     Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|----------------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
| Substring_ParseLicense | 14.86 us | 0.276 us | 0.538 us |  1.00 | 4.3640 |  17.86 KB |        1.00 |

// * Hints *
Outliers
    SubstringVsSpanHarness.Substring_ParseLicense: .NET 6.0 -> 4 outliers were removed (16.59 us..16.96 us)

// * Legends *
    Mean        : Arithmetic mean of all measurements
    Error       : Half of 99.9% confidence interval
    StdDev      : Standard deviation of all measurements
    Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
    Gen0        : GC Generation 0 collects per 1000 operations
    Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
    Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
    1 us        : 1 Microsecond (0.000001 sec)

// * Diagnostic Output - MemoryDiagnoser *


// ***** BenchmarkRunner: End *****
Run time: 00:00:31 (31.53 sec), executed benchmarks: 1

Global total time: 00:00:35 (35.6 sec), executed benchmarks: 1

*/
}
