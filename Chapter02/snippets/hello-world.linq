<Query Kind="Program" />

public class Program
{
  public static void Main()
  {
  	List<string> greetings= new() {"Hello","World!"};	// C#9 Feature
    Console.WriteLine(String.Join(" ",greetings));
  }
}