<Query Kind="Program" />

void Main()
{
	var p1 = new Player { PlayerId = 10,FirstName= "John" };
   Console.WriteLine (p1.FirstName);
}

public class Player
        {
            public int PlayerId { get; init; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
