using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
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

    private Task _incomplete = new TaskCompletionSource<bool>().Task;

    [Benchmark]
    public Task OneAlreadyCompleted() => Task.WhenAny(Task.CompletedTask, _incomplete);

    [Benchmark]
    public Task AsyncCompletion()
    {
        AsyncTaskMethodBuilder atmb = default;
        Task result = Task.WhenAny(atmb.Task, _incomplete);
        atmb.SetResult();
        return result;
    }
}
