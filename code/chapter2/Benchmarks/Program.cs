using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;
using System.Text;

//Run with
//dotnet run -c Release -f net48 --runtimes net48 netcoreapp3.1 netcoreapp5.0 --filter *Program*

[MemoryDiagnoser]
public class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssemblies(new[] { typeof(Program).Assembly }).Run(args);

    private byte[] _arr = Encoding.UTF8.GetBytes("Test to see improvements to IndexOfAny.. how are they?");
    [Benchmark] public int IndexOf() => new Span<byte>(_arr).IndexOfAny((byte)'.', (byte)'?');
}
