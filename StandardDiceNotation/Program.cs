using System;
using System.Text.RegularExpressions;

namespace StandardDiceNotation
{
    internal class Program
    {
        static int NumberOfRolls(string text)
        {
            Match result = Regex.Match(text, "(?<=\\s|^)(\\d*)d(\\d+)([+-]\\d+)?(?=\\s|$|[.,!?])");
            int numberOfRolls;
            if (result.Groups[1].Value == "")
            {
                numberOfRolls = 1;
            }
            else
            {
                numberOfRolls = Int32.Parse(result.Groups[1].Value);
            }
            return numberOfRolls;
        }
        static bool IsStandardDiceNotation(string text)
        {
            if (Regex.IsMatch(text, "(?<=\\s|^)(\\d*)d(\\d+)([+-]\\d+)?(?=\\s|$|[.,!?])"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus)
        {
            var random = new Random();
            int result = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
            }
            result += fixedBonus;
            return result;
        }
        static int DiceRoll(string diceRoll)
        {
            Match diceNotation = Regex.Match(diceRoll, "(?<=\\s|^)(\\d*)d(\\d+)([+-]\\d+)?(?=\\s|$|[.,!?])");
            int numbersOfRolls;
            if (diceNotation.Groups[1].Value == "")
            {
                numbersOfRolls = 1;
            }
            else
            {
                numbersOfRolls = Int32.Parse(diceNotation.Groups[1].Value);
            }

            int diceSides = Int32.Parse(diceNotation.Groups[2].Value);

            int fixedBonus = 0;
            if (diceNotation.Groups[3].Success)
            {
                fixedBonus = Int32.Parse(diceNotation.Groups[3].Value);
            }

            return DiceRoll(numbersOfRolls, diceSides, fixedBonus);
        }
        static void Main(string[] args)
        {
            if (IsStandardDiceNotation("2d6") == true)
            {
                Console.Write("Throwing 2d6: ");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{DiceRoll("2d6")} ");
                }
            }
            else
            {
                Console.Write("Can't throw 2d6...");
            }

            Console.WriteLine();

            if (IsStandardDiceNotation("34") == true)
            {
                Console.Write("Throwing 34: ");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{DiceRoll("34")} ");
                }
            }
            else
            {
                Console.Write("Can't throw 34...");
            }



            //Standard dice notion counter
            //string textString = "To use the magic potion of Dragon Breath, first roll d8. If you roll 2 or higher, you manage to open the potion. Now roll 5d4+5 to see how many seconds the spell will last. Finally, the damage of the flames will be 2d6 per second.";
            //string[] words = textString.Split(" ");
            //int diceRolls = 0;

            //int counter = 0;
            //foreach (string word in words)
            //{
            //    if (IsStandardDiceNotation(word) == true)
            //    {
            //        counter++;
            //        diceRolls += NumberOfRolls(word);
            //    }
            //}
            //Console.WriteLine($"{counter} standard dice notations present.");
            //Console.WriteLine($"The player will have to preform {diceRolls} rolls.");
        }
    }
}
