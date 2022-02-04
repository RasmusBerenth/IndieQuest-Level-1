using System;
using System.Collections.Generic;

namespace fundamentals1Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Roll 4 (1,6) sub lowest then add tougheter
            var random = new Random();
            int newRoll;
            var abilityScores = new List<int>();

            for (int i = 0; i < 6; i++)
            {


                int counter = 0;
                int abilityScore = 0;
                var rolls = new List<int>();


                do
                {
                    newRoll = random.Next(1, 7);
                    rolls.Add(newRoll);
                    abilityScore += newRoll;
                    counter++;
                }
                while (counter < 4);


                rolls.Sort();
                int lowestRoll = rolls[0];
                abilityScore -= lowestRoll;
                abilityScores.Add(abilityScore);

                //Display rolls
                Console.WriteLine($"You roll {String.Join(", ", rolls)}.The ability score is {abilityScore}.");
            }



            //Sort the ability scores into a list
            abilityScores.Sort();
            Console.WriteLine($"Your available ability scores are {String.Join(", ", abilityScores)}.");
        }
    }
}
