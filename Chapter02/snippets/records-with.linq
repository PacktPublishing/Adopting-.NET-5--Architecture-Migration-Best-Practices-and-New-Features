<Query Kind="Program" />

void Main()
{
	var player = new Player { PlayerId =10,FirstName = "John", LastName = "Citizen" };
	
	var player2= player with { FirstName = "Ron" };
	
	Console.WriteLine(player2.LastName);
}

public record Player
        {
            public int PlayerId { get; init; }
            public string FirstName { get; init; }
            public string LastName { get; init; }
        }
