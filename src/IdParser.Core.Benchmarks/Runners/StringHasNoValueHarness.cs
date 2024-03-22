using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace IdParser.Core.Benchmarks.Runners;

/*
// * Summary *

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3296/23H2/2023Update/SunValley3)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.202
  [Host]   : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 8.0 : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 8.0  Runtime=.NET 8.0

| Method                 | value   | Mean      | Error     | StdDev    | Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------- |-------- |----------:|----------:|----------:|------:|--------:|----------:|------------:|
| StringEquals_Operator  | ?       |  1.761 ns | 0.0454 ns | 0.0424 ns |  1.00 |    0.00 |         - |          NA |
| StringEquals_Equals    | ?       |  1.822 ns | 0.0461 ns | 0.0409 ns |  1.04 |    0.03 |         - |          NA |
| StringEquals_HashSet   | ?       |  1.775 ns | 0.0362 ns | 0.0321 ns |  1.01 |    0.03 |         - |          NA |
| StringEquals_FrozenSet | ?       |  1.785 ns | 0.0478 ns | 0.0447 ns |  1.01 |    0.04 |         - |          NA |
|                        |         |           |           |           |       |         |           |             |
| StringEquals_Operator  | NONE    |  2.781 ns | 0.0612 ns | 0.0543 ns |  1.00 |    0.00 |         - |          NA |
| StringEquals_Equals    | NONE    |  2.739 ns | 0.0739 ns | 0.0851 ns |  0.99 |    0.03 |         - |          NA |
| StringEquals_HashSet   | NONE    | 10.356 ns | 0.1464 ns | 0.1298 ns |  3.73 |    0.11 |         - |          NA |
| StringEquals_FrozenSet | NONE    |  4.406 ns | 0.0792 ns | 0.0741 ns |  1.59 |    0.04 |         - |          NA |
|                        |         |           |           |           |       |         |           |             |
| StringEquals_Operator  | unavail |  3.325 ns | 0.0912 ns | 0.0853 ns |  1.00 |    0.00 |         - |          NA |
| StringEquals_Equals    | unavail |  3.402 ns | 0.0823 ns | 0.0770 ns |  1.02 |    0.04 |         - |          NA |
| StringEquals_HashSet   | unavail | 11.893 ns | 0.1845 ns | 0.1636 ns |  3.58 |    0.10 |         - |          NA |
| StringEquals_FrozenSet | unavail |  4.813 ns | 0.0835 ns | 0.0781 ns |  1.45 |    0.04 |         - |          NA |
|                        |         |           |           |           |       |         |           |             |
| StringEquals_Operator  | unavl   |  3.074 ns | 0.0805 ns | 0.0753 ns |  1.00 |    0.00 |         - |          NA |
| StringEquals_Equals    | unavl   |  2.467 ns | 0.0637 ns | 0.0596 ns |  0.80 |    0.03 |         - |          NA |
| StringEquals_HashSet   | unavl   | 12.242 ns | 0.1807 ns | 0.1602 ns |  3.97 |    0.10 |         - |          NA |
| StringEquals_FrozenSet | unavl   |  4.350 ns | 0.0795 ns | 0.0705 ns |  1.41 |    0.03 |         - |          NA |
*/

[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class StringHasNoValueHarness
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(TestStrings))]
    public void StringEquals_Operator(string? value)
    {
        var _ = StringHasNoValue_Operator(value);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestStrings))]
    public void StringEquals_Equals(string? value)
    {
        var _ = StringHasNoValue_Equals(value);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestStrings))]
    public void StringEquals_HashSet(string? value)
    {
        var _ = StringHasNoValue_HashSet(value);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestStrings))]
    public void StringEquals_FrozenSet(string? value)
    {
        var _ = StringHasNoValue_FrozenSet(value);
    }


    private static readonly IEnumerable<string?> _testStrings;
    private static readonly HashSet<string> _testValuesHashSet;
    private static readonly FrozenSet<string> _testValuesFrozenSet;

    public IEnumerable<string?> TestStrings => _testStrings;

    static StringHasNoValueHarness()
    {
        _testStrings = [
            null,
            "NONE",
            "unavl",
            "unavail",
            ];

        _testValuesHashSet = _testStrings
            .Where(ts => ts is not null)
            .Select(ts => ts!)
            .ToHashSet();

        _testValuesFrozenSet = _testValuesHashSet.ToFrozenSet();
    }


    //
    // Private methods
    //

    private static bool StringHasNoValue_Operator([NotNullWhen(false)] string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input == "NONE"
            || input == "unavl"
            || input == "unavail";
    }

    private static bool StringHasNoValue_Equals([NotNullWhen(false)] string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || input.Equals("NONE")
            || input.Equals("unavl")
            || input.Equals("unavail");
    }

    private static bool StringHasNoValue_HashSet([NotNullWhen(false)] string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || _testValuesHashSet.Contains(input);
    }

    private static bool StringHasNoValue_FrozenSet([NotNullWhen(false)] string? input)
    {
        return string.IsNullOrWhiteSpace(input)
            || _testValuesFrozenSet.Contains(input);
    }
}
