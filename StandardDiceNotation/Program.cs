using System;
using System.Text.RegularExpressions;

namespace StandardDiceNotation
{
    internal class Program
    {
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
            Match diceNotation = Regex.Match(diceRoll, "(\\d*)?d(\\d+)([+-]\\d+)?");

            if (diceNotation.Groups[1].Value == "")
            {

            }

            int numbersOfRolls = Int32.Parse(diceNotation.Groups[1].Value);
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
            Console.Write("Throwing d6: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("d6")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing 2d4: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("2d4")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing d8+12: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("d8+12")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing 2d4-1: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("2d4-1")} ");
            }
        }
    }
}
