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
            Match diceNotation = Regex.Match(diceRoll, "(\\d)d(\\d)([+-]\\d)?");
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
            Console.Write("Throwing 1d6+4: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("1d6+4")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing 1d8: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("1d8")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing 2d6+1: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("2d6+1")} ");
            }

            Console.WriteLine();

            Console.Write("Throwing 2d4+2: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{DiceRoll("2d4+2")} ");
            }
        }
    }
}
