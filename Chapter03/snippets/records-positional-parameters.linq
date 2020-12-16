<Query Kind="Program" />

void Main()
{
var player = new Player("John", "Citizen"); // both properties will initialize

var (name1,name2) = player;  // name1 = John, name2= Citizen 

	Console.WriteLine(name1);
	Console.WriteLine(name2);
}

public record Player(string FirstName, string LastName);

