using System;

namespace IntroducingNet5.Chapter3.CS9PlayerValueCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PlayerValueCalculatorService();

            var value1 = service.GetPlayerValue_1(
                new CricketStats {RunsScored=100 }
                );

            Console.WriteLine(value1);

            var value2 = service.GetPlayerValue_2(
                new SoccerStats { GoalsScored = 100,HasPlayedWorldCup=true }
                );

            Console.WriteLine(value2);

            var value3 = service.GetPlayerValue_3(
    new CricketStats { MatchesPlayed=110,RunsScored=200 }
    );

            Console.WriteLine(value3);

            var value4 = service.GetPlayerValue_4(
new TennisStats { SetsWon=200 },BrandSize.Large
);

            Console.WriteLine(value4);

        }
    }
}
