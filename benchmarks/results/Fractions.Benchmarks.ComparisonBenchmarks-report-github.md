```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.4170/22H2/2022Update)
AMD Ryzen 9 7900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.202
  [Host]   : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  ShortRun : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method         | a                    | b                    | Mean      | Error      | StdDev    | Gen0   | Allocated |
|--------------- |--------------------- |--------------------- |----------:|-----------:|----------:|-------:|----------:|
| **Equals**         | **-1024**                | **-1/1024**              |  **1.629 ns** |  **0.3568 ns** | **0.0196 ns** |      **-** |         **-** |
| IsEquivalentTo | -1024                | -1/1024              |  8.172 ns |  0.3270 ns | 0.0179 ns |      - |         - |
| GetHashCode    | -1024                | -1/1024              |  4.963 ns |  0.0130 ns | 0.0007 ns |      - |         - |
| CompareTo      | -1024                | -1/1024              | 25.306 ns |  4.2008 ns | 0.2303 ns |      - |         - |
| **Equals**         | **-45**                  | **1/6**                  |  **1.580 ns** |  **0.6161 ns** | **0.0338 ns** |      **-** |         **-** |
| IsEquivalentTo | -45                  | 1/6                  |  8.305 ns |  0.8940 ns | 0.0490 ns |      - |         - |
| GetHashCode    | -45                  | 1/6                  |  5.229 ns |  0.1668 ns | 0.0091 ns |      - |         - |
| CompareTo      | -45                  | 1/6                  | 25.430 ns |  1.6655 ns | 0.0913 ns |      - |         - |
| **Equals**         | **0**                    | **1**                    |  **1.384 ns** |  **0.4631 ns** | **0.0254 ns** |      **-** |         **-** |
| IsEquivalentTo | 0                    | 1                    |  8.159 ns |  1.1496 ns | 0.0630 ns |      - |         - |
| GetHashCode    | 0                    | 1                    |  4.970 ns |  0.0733 ns | 0.0040 ns |      - |         - |
| CompareTo      | 0                    | 1                    |  2.926 ns |  0.6964 ns | 0.0382 ns |      - |         - |
| **Equals**         | **88427(...)10656 [31]** | **47161(...)70496 [33]** |  **2.535 ns** |  **0.0865 ns** | **0.0047 ns** |      **-** |         **-** |
| IsEquivalentTo | 88427(...)10656 [31] | 47161(...)70496 [33] | 10.184 ns |  0.5757 ns | 0.0316 ns |      - |         - |
| GetHashCode    | 88427(...)10656 [31] | 47161(...)70496 [33] | 29.811 ns |  5.3768 ns | 0.2947 ns |      - |         - |
| CompareTo      | 88427(...)10656 [31] | 47161(...)70496 [33] | 76.287 ns | 18.5379 ns | 1.0161 ns | 0.0057 |      96 B |
| **Equals**         | **88427(...)10656 [31]** | **88427(...)10656 [31]** |  **6.672 ns** |  **0.9378 ns** | **0.0514 ns** |      **-** |         **-** |
| IsEquivalentTo | 88427(...)10656 [31] | 88427(...)10656 [31] | 11.977 ns |  0.8160 ns | 0.0447 ns |      - |         - |
| GetHashCode    | 88427(...)10656 [31] | 88427(...)10656 [31] | 29.864 ns |  5.4908 ns | 0.3010 ns |      - |         - |
| CompareTo      | 88427(...)10656 [31] | 88427(...)10656 [31] |  5.273 ns |  0.0892 ns | 0.0049 ns |      - |         - |
| **Equals**         | **245850922/78256779**   | **26714619/25510582**    |  **1.651 ns** |  **0.0788 ns** | **0.0043 ns** |      **-** |         **-** |
| IsEquivalentTo | 245850922/78256779   | 26714619/25510582    |  8.288 ns |  0.6151 ns | 0.0337 ns |      - |         - |
| GetHashCode    | 245850922/78256779   | 26714619/25510582    |  5.266 ns |  1.9945 ns | 0.1093 ns |      - |         - |
| CompareTo      | 245850922/78256779   | 26714619/25510582    | 57.468 ns | 46.6248 ns | 2.5557 ns | 0.0038 |      64 B |
| **Equals**         | **245850922/78256779**   | **245850922/78256779**   |  **2.502 ns** |  **0.2267 ns** | **0.0124 ns** |      **-** |         **-** |
| IsEquivalentTo | 245850922/78256779   | 245850922/78256779   |  9.196 ns |  1.1008 ns | 0.0603 ns |      - |         - |
| GetHashCode    | 245850922/78256779   | 245850922/78256779   |  4.973 ns |  0.0618 ns | 0.0034 ns |      - |         - |
| CompareTo      | 245850922/78256779   | 245850922/78256779   |  2.717 ns |  0.7859 ns | 0.0431 ns |      - |         - |