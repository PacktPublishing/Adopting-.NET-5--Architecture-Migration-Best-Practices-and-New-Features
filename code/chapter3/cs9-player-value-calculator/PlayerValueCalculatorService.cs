using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroducingNet5.Chapter3.CS9PlayerValueCalculator
{
    public class PlayerValueCalculatorService
    {
        public decimal GetPlayerValue_1(object stats)
        {

            return stats switch
            {
                CricketStats cr => cr.RunsScored * 100,
                TennisStats t => t.SetsWon * 100,
                SoccerStats s => s.GoalsScored * 100,
                { } => throw new ArgumentException(message: "Not a known sports statistics", paramName: nameof(stats)),
                null => throw new ArgumentNullException(nameof(stats))
            };
        }

        public decimal GetPlayerValue_2(object stats)
        {
            return stats switch
            {
                CricketStats cr => cr.RunsScored * 100,
                TennisStats t => t.SetsWon * 100,
                SoccerStats { HasPlayedWorldCup: true } s => s.GoalsScored * 150,
                SoccerStats { HasPlayedWorldCup: false } s => s.GoalsScored * 100,
                { } => throw new ArgumentException(message: "Not a known sports statistics", paramName: nameof(stats)),
                null => throw new ArgumentNullException(nameof(stats))
            };
        }

        public decimal GetPlayerValue_3(object stats)
        {
            return stats switch
            {
                CricketStats cr => cr.MatchesPlayed switch
                {
                    >= 100 => cr.RunsScored * 150,
                    >= 50 and < 100 => cr.RunsScored * 100,
                    < 50 => cr.RunsScored * 80
                },
                TennisStats t => t.SetsWon * 100,
                SoccerStats { HasPlayedWorldCup: true } s => s.GoalsScored * 150,
                SoccerStats { HasPlayedWorldCup: false } s => s.GoalsScored * 100,
                { } => throw new ArgumentException(message: "Not a known sports statistics", paramName: nameof(stats)),
                null => throw new ArgumentNullException(nameof(stats))
            };
        }

        public decimal GetPlayerValue_4(object stats, BrandSize brandSize)
        {
            return (stats, brandSize) switch
            {
                (CricketStats cr, BrandSize.Large) => cr.RunsScored * 150,
                (CricketStats cr, BrandSize.Medium) => cr.RunsScored * 100,
                (CricketStats cr, BrandSize.Small) => cr.RunsScored * 80,
                (TennisStats t, BrandSize.Large) => t.SetsWon * 150,
                (TennisStats t, _) => t.SetsWon * 100,
                (SoccerStats s, _) => s.HasPlayedWorldCup switch
                {
                    true => s.GoalsScored * 150,
                    false => s.GoalsScored * 100
                },
                ({ }, _) => throw new ArgumentException(message: "Not a known sports statistics", paramName: nameof(stats)),
                (null, _) => throw new ArgumentNullException(nameof(stats))
            };
        }
    }
}
