<Query Kind="Program" />

void Main()
{


    Player tennisPlayer1 = new TennisPlayer { age=39,FirstName="Roger",LastName="Federrer" };
    Console.WriteLine((tennisPlayer1 as TennisPlayer).age);	// 39

    Player tennisPlayer2 = tennisPlayer1 with { FirstName="Rafael", LastName = "Nadal" };
    Console.WriteLine((tennisPlayer2 as TennisPlayer).age);	// 39

}

public record Player {public string FirstName; public string LastName;};

public record TennisPlayer : Player { public int age; }
