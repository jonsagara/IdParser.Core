using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using IdParser.Core;
using IdParser.Core.Benchmarks.Runners;

//Console.WriteLine("Dummy code to fix compilation when harnesses are commented out.");

//BenchmarkRunner.Run<DriversLicenseHarness>();
//BenchmarkRunner.Run<DictionaryVsToDictionaryHarness>();
//BenchmarkRunner.Run<SubstringTrimVsSliceTrimToStringHarness>();
BenchmarkRunner.Run<SubstringVsSpanHarness>();



//
// For dotMemory
//

//var sw = Stopwatch.StartNew();
//var licenseText = File.ReadAllText(Path.Combine(@"data\Licenses\CA.txt"));

//for (var ix = 0; ix < 1_000_000; ix++)
//{
//    if (ix % 1_000 == 0)
//    {
//        Console.WriteLine($"{ix}...");
//    }
//    Barcode.Parse(licenseText, Validation.None);
//}

//sw.Stop();
//Console.WriteLine($"Done. Elapsed: {sw.Elapsed}");
