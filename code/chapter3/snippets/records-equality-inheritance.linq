<Query Kind="Program" />

void Main()
{


    Player tennisPlayer1 = new TennisPlayer { age=39,FirstName="Roger",LastName="Federrer" };
    Player player2 = new Player { FirstName = "Roger", LastName = "Federrer" };

    Console.WriteLine(tennisPlayer1.Equals(player2) );	// false
    Console.WriteLine(player2.Equals(tennisPlayer1));	// false

}

public record Player {public string FirstName; public string LastName;};

public record TennisPlayer : Player { public int age; }
