using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

//Run with
//dotnet run -c Release -f net48 --runtimes net48 netcoreapp3.1 netcoreapp5.0 --filter *Program*

[MemoryDiagnoser]
public class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssemblies(new[] { typeof(Program).Assembly }).Run(args);

    public class DoubleSorting : Sorting<double> { protected override double GetNext() => _random.Next(); }
    public class Int32Sorting : Sorting<int> { protected override int GetNext() => _random.Next(); }
    public class StringSorting : Sorting<string>
    {
        protected override string GetNext()
        {
            var dest = new char[_random.Next(1, 5)];
            for (int i = 0; i < dest.Length; i++) dest[i] = (char)('a' + _random.Next(26));
            return new string(dest);
        }
    }

    public abstract class Sorting<T>
    {
        protected Random _random;
        private T[] _orig, _array;

        [Params(10)]
        public int Size { get; set; }

        protected abstract T GetNext();

        [GlobalSetup]
        public void Setup()
        {
            _random = new Random(42);
            _orig = Enumerable.Range(0, Size).Select(_ => GetNext()).ToArray();
            _array = (T[])_orig.Clone();
            Array.Sort(_array);
        }

        [Benchmark]
        public void Random()
        {
            _orig.AsSpan().CopyTo(_array);
            Array.Sort(_array);
        }
    }
}
