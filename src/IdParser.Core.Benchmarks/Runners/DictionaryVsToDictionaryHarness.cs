using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

/* No meaningful difference. A for loop is slightly faster, but there's no memory difference, and readability suffers.

// * Summary *

BenchmarkDotNet v0.13.6, Windows 11 (10.0.22621.1992/22H2/2022Update/SunValley2)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 6.0.412
  [Host]   : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2

Job=.NET 6.0  Runtime=.NET 6.0

|                         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------------------- |---------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|              LINQ_ToDictionary | 1.303 us | 0.0207 us | 0.0303 us |  1.00 |    0.00 | 0.7877 |   3.22 KB |        1.00 |
|     DictionaryDefaultCount_For | 1.458 us | 0.0286 us | 0.0429 us |  1.12 |    0.04 | 1.0052 |   4.11 KB |        1.28 |
|        DictionaryWithCount_For | 1.208 us | 0.0233 us | 0.0286 us |  0.92 |    0.03 | 0.7877 |   3.22 KB |        1.00 |
| DictionaryDefaultCount_ForEach | 1.403 us | 0.0235 us | 0.0196 us |  1.07 |    0.04 | 1.0052 |   4.11 KB |        1.28 |
|    DictionaryWithCount_ForEach | 1.251 us | 0.0235 us | 0.0534 us |  0.96 |    0.05 | 0.7877 |   3.22 KB |        1.00 |

*/

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class DictionaryVsToDictionaryHarness
{
    private readonly string _licenseText = @"
@a
ANSI 636014040002DL00410278ZC03190034DLDCAC
DCBNONE
DCDNONE
DBA07052019
DCSHARPER
DACELIJAH
DADMASON
DBD02022016
DBB07051973
DBC1
DAYBLU
DAU068 IN
DAG671 BLUEBERRY HILL DR
DAIMILPITAS
DAJCA
DAK950350000  
DAQF1485768
DCF99?02?2916640L7?ZBFD?K9
DCGUSA
DDEU
DDFU
DDGU
DAW165
DAZBRN
DCK26933F14948489491
DDB04162010
DDD0
ZCZCAY
ZCB
ZCCBLU
ZCDBRN
ZCE
ZCF
";

    private readonly string[] _licenseLines;

    public DictionaryVsToDictionaryHarness()
    {
        _licenseLines = _licenseText
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(lt => lt.Trim())
            .Where(r => r.Length >= 3)
            .ToArray();
    }

    [Benchmark(Baseline = true)]
    public void LINQ_ToDictionary()
    {
        var dict = _licenseLines
            .ToDictionary(r => r.Substring(startIndex: 0, length: 3), r => r.Substring(startIndex: 3).Trim());
    }

    [Benchmark]
    public void DictionaryDefaultCount_For()
    {
        Dictionary<string, string> dict = new();

        for (var ixLine = 0; ixLine < _licenseLines.Length; ixLine++)
        {
            var line = _licenseLines[ixLine];

            dict.Add(line.Substring(startIndex: 0, length: 3), line.Substring(startIndex: 3).Trim());
        }
    }

    [Benchmark]
    public void DictionaryWithCount_For()
    {
        Dictionary<string, string> dict = new(_licenseLines.Length);

        for (var ixLine = 0; ixLine < _licenseLines.Length; ixLine++)
        {
            var line = _licenseLines[ixLine];

            dict.Add(line.Substring(startIndex: 0, length: 3), line.Substring(startIndex: 3).Trim());
        }
    }

    [Benchmark]
    public void DictionaryDefaultCount_ForEach()
    {
        Dictionary<string, string> dict = new();

        foreach (var line in _licenseLines)
        {
            dict.Add(line.Substring(startIndex: 0, length: 3), line.Substring(startIndex: 3).Trim());
        }
    }

    [Benchmark]
    public void DictionaryWithCount_ForEach()
    {
        Dictionary<string, string> dict = new(_licenseLines.Length);

        foreach (var line in _licenseLines)
        {
            dict.Add(line.Substring(startIndex: 0, length: 3), line.Substring(startIndex: 3).Trim());
        }
    }
}
