<Query Kind="Program" />

void Main()
{
	Person CloneThisPerson(Person person)
	{
		return new Person(person.FirstName,person.LastName);
	}
	
	
	Console.WriteLine( CloneThisPerson(new ("FirstName","LastName")));
}

public record Person(string FirstName, string LastName);
