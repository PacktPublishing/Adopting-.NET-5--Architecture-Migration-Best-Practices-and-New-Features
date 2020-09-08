using BenchmarkDotNet.Running;
using System;

namespace IntroducingNet5.Chapter3.CS9Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SubstringVsSlice>();

        }
    }
}
