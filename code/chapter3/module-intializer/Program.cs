using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Preview
{
    class Program
    {
        public static string ConnectionString;
        static void Main(string[] args)
        {
            Console.WriteLine($"ConnectionString = {ConnectionString}");
        }


        [ModuleInitializer]
        public static void Initialize()
        {
            ConnectionString = "DB Connection String";
        }

    }
}
