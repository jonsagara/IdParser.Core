//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Jobs;

//namespace IdParser.Core.Benchmarks.Runners;

///* Results: the Static API is 3x faster and has fewer allocations per invocation.

//// * Summary *

//BenchmarkDotNet v0.13.6, Windows 11 (10.0.22621.1992/22H2/2022Update/SunValley2)
//Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
//.NET SDK 6.0.412
//  [Host]   : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
//  .NET 6.0 : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2

//Job=.NET 6.0  Runtime=.NET 6.0

//|                Method |     Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
//|---------------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
//| Instance_ParseLicense | 42.59 us | 0.647 us | 0.605 us |  1.00 | 7.6294 |  31.36 KB |        1.00 |
//|   Static_ParseLicense | 14.78 us | 0.289 us | 0.450 us |  0.34 | 4.3945 |  17.99 KB |        0.57 |

//// * Hints *
//Outliers
//  DriversLicenseHarness.Instance_ParseLicense: .NET 6.0 -> 1 outlier  was  detected (41.11 us)

//// * Legends *
//  Mean        : Arithmetic mean of all measurements
//  Error       : Half of 99.9% confidence interval
//  StdDev      : Standard deviation of all measurements
//  Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
//  Gen0        : GC Generation 0 collects per 1000 operations
//  Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
//  Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
//  1 us        : 1 Microsecond (0.000001 sec)

//// * Diagnostic Output - MemoryDiagnoser *


//// ***** BenchmarkRunner: End *****
//Run time: 00:00:39 (39.22 sec), executed benchmarks: 2

//Global total time: 00:00:45 (45.89 sec), executed benchmarks: 2

//*/

//[SimpleJob(RuntimeMoniker.Net60)]
//[MemoryDiagnoser]
//public class DriversLicenseHarness
//{
//    private readonly string _licenseText;

//    public DriversLicenseHarness()
//    {
//        _licenseText = File.ReadAllText(Path.Combine(@"data\Licenses\CA.txt"));
//    }

//    [Benchmark(Baseline = true)]
//    public void Instance_ParseLicense()
//        => Barcode.Parse(_licenseText, Validation.None);

//    [Benchmark]
//    public void Static_ParseLicense()
//        => Static.Barcode.Parse(_licenseText, Static.Validation.None);
//}
