using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StandardDiceNotation
{
    internal class Program
    {
        static string StandardDiceNotationRegex = "(?<=\\s|^)(\\d*)d(\\d+)([+-]\\d+)?(?=\\s|$|[.,!?])";
        static string ExtractionRegex = "^(.*)d(.+?)(([-+\\/*])(.+))?$";

        static void Throw(string diceRoll)
        {
            try
            {
                Console.Write($"Throwing {diceRoll}: ");
                //for (int i = 0; i < 10; i++)
                //{
                Console.Write($"{DiceRoll(diceRoll)} ");
                //}
            }
            catch (Exception e)
            {
                Console.Write($"Can't throw {diceRoll}...{e.Message}");
            }

            Console.WriteLine();
        }
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
            return Regex.IsMatch(text, StandardDiceNotationRegex);
        }
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus)
        {
            var random = new Random();
            var rolls = new List<int>();
            int result = 0;

            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
                rolls.Add(roll); //Output this list to Main.
            }
            result += fixedBonus;
            return result;
        }
        static int DiceRoll(string diceRoll)
        {
            Match diceNotation = Regex.Match(diceRoll, ExtractionRegex);
            if (!diceNotation.Success)
            {
                throw new ArgumentException("A standard dice notaion needs at least a d followed by a number of sides.");
            }

            int numbersOfRolls;
            int diceSides;
            int fixedBonus = 0;


            if (diceNotation.Groups[1].Value == "")
            {
                numbersOfRolls = 1;
            }
            else
            {
                try
                {
                    numbersOfRolls = Int32.Parse(diceNotation.Groups[1].Value);
                }
                catch
                {
                    throw new ArgumentException($"Number of rolls ({diceNotation.Groups[1].Value}) was incorrect");
                }

                if (numbersOfRolls < 1)
                {
                    throw new ArgumentException($"Number of rolls ({diceNotation.Groups[1].Value}) needs to be positive");
                }
            }

            try
            {
                diceSides = Int32.Parse(diceNotation.Groups[2].Value);
            }
            catch
            {
                throw new ArgumentException($"Number of dice sides ({diceNotation.Groups[2].Value}) must be a diget whitout decimals");
            }

            if (diceSides < 1)
            {
                throw new ArgumentException($"Number of dice sides ({diceNotation.Groups[2].Value}) must be a positiv didget");
            }

            if (diceNotation.Groups[3].Success)
            {
                try
                {
                    fixedBonus = Int32.Parse(diceNotation.Groups[3].Value);
                }
                catch
                {
                    string operation = diceNotation.Groups[4].Value;
                    if (operation == "+" || operation == "-")
                    {
                        throw new ArgumentException($"Bonus ({diceNotation.Groups[5].Value} needs an integer)");
                    }
                    else
                    {
                        throw new ArgumentException("Bonus can only be added or subtracted");
                    }
                }

            }

            return DiceRoll(numbersOfRolls, diceSides, fixedBonus);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("DICE SIMULATOR");
            Console.WriteLine("Enter desired dice roll in standard dice notation:");
            string userInput;

            do
            {
                userInput = Console.ReadLine();
                if (IsStandardDiceNotation(userInput) == true)
                {
                    Console.WriteLine("Simulating...");
                    Throw(userInput);
                    break;
                }
                else
                {
                    Throw(userInput);
                    Console.WriteLine("You didn't use standard dice notation. Try again!");
                }

            } while (true);


        }
    }
}
