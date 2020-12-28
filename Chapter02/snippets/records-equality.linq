<Query Kind="Program" />

void Main()
{

var p1 = new Player ("John", "Citizen");
var p2 = new Player ("John", "Citizen");

    Console.WriteLine(p1.Equals(p2)); // true, same values
    Console.WriteLine(p1 == p2);    // false, different objects
    Console.WriteLine(ReferenceEquals(p1,p2)); // false

}

public record Player(string FirstName, string LastName);


