<Query Kind="Program" />

void Main()
{
	Animal animal=null; 
	Tiger tiger=null;
	Animal someAnimal = animal ?? tiger; // Both share Animal type

	var items = new List<string>{"Item1","Item2","Item3"};
	items.ForEach(_ => Console.WriteLine("Another item processed"));

}

public class Animal{};
public class Tiger:Animal{};
