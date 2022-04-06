using System;
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
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{DiceRoll(diceRoll)} ");
                }
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
            Throw("2d6");
            Throw("34");
            Throw("-12");
            Throw("ad6");
            Throw("-3d6");
            Throw("44d");
            Throw("0d6");
            Throw("d+");
            Throw("2d-4");
            Throw("2d4.5");
            Throw("2d$");
            Throw("2d-6.5");
            Throw("1d8*2");
            Throw("1d8+2");




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
