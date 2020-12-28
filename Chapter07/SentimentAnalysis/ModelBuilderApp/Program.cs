using System;

namespace ModelBuilderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelBuilder.CreateModel();
            Console.WriteLine("=============== Model created successfully, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
