# AsyncAwaitCosts

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17763.1577 (1809/October2018Update/Redstone5)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

Job=.NET Core 3.1  Runtime=.NET Core 3.1  

```
|                       Method |  N |        Mean |       Error |      StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |--- |------------:|------------:|------------:|-------:|--------:|-------:|------:|------:|----------:|
|      ReturnDoubleAwaitedTask | 20 | 71,350.3 ns | 1,405.54 ns | 1,503.91 ns | 311.01 |    7.95 | 1.9531 |     - |     - |    8325 B |
|            ReturnAwaitedTask | 20 | 50,544.7 ns |   996.51 ns |   932.14 ns | 219.61 |    7.84 | 1.4038 |     - |     - |    5887 B |
| ReturnDoubleTaskWithoutAwait | 20 | 37,880.2 ns |   686.51 ns |   608.58 ns | 165.05 |    5.24 | 0.7935 |     - |     - |    3490 B |
|       ReturnTaskWithoutAwait | 20 | 38,114.5 ns |   517.03 ns |   458.34 ns | 166.06 |    4.41 | 0.7935 |     - |     - |    3491 B |
|         ReturnTaskFromResult | 20 |    593.5 ns |    14.37 ns |    40.52 ns |   2.74 |    0.17 | 0.3614 |     - |     - |    1512 B |
|    ReturnResultWithAsyncWord | 20 |    926.8 ns |    20.65 ns |    57.22 ns |   4.16 |    0.29 | 0.3614 |     - |     - |    1512 B |
|                          For | 20 |    228.9 ns |     4.46 ns |     6.10 ns |   1.00 |    0.00 |      - |     - |     - |         - |
