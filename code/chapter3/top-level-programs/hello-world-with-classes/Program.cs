using System;

for (int i=0;i<10;i++)
    Console.WriteLine(GreetingsProvider.SayHello("John"));

internal class GreetingsProvider
{
    public static string SayHello(string name)
    {
        return $"Hello {name}";
    }
}

