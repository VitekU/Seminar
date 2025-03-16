```

BenchmarkDotNet v0.14.0, macOS Sequoia 15.3.2 (24D81) [Darwin 24.3.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK 7.0.400
  [Host]     : .NET 7.0.14 (7.0.1423.51910), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.14 (7.0.1423.51910), Arm64 RyuJIT AdvSIMD


```
| Method                      | Mean          | Error        | StdDev       | Gen0     | Gen1   | Allocated |
|---------------------------- |--------------:|-------------:|-------------:|---------:|-------:|----------:|
| Knapsack_Backtracking       | 537,467.35 ns | 1,293.515 ns | 1,080.144 ns | 248.0469 | 1.9531 | 1560241 B |
| Knapsack_DynamicProgramming |      79.27 ns |     0.153 ns |     0.136 ns |   0.0254 |      - |     160 B |
