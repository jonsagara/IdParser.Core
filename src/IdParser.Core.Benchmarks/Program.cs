using BenchmarkDotNet.Running;
using IdParser.Core.Benchmarks.Runners;

//Console.WriteLine("Dummy code to fix compilation when harnesses are commented out.");

BenchmarkRunner.Run<SubstringVsSpanHarness>();
//BenchmarkRunner.Run<DriversLicenseHarness>();
