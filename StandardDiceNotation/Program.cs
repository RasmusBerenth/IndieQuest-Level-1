using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace StandardDiceNotation
{
    internal class Program
    {
        static string StandardDiceNotationRegex = "(?<=\\s|^)(\\d*)d(\\d+)([+-]\\d+)?(?=\\s|$|[.,!?])";
        static string ExtractionRegex = "^(.*)d(.+?)(([-+\\/*])(.+))?$";
        static string OrdinalNumber(int number)
        {

            int lastDigit = number % 10;

            if (number > 10)
            {
                int secondToLastDigit = (number / 10) % 10;
                if (secondToLastDigit == 1)
                {
                    return number + "th";
                }
            }

            if (lastDigit == 1)
            {
                return number + "st";
            }

            if (lastDigit == 2)
            {
                return number + "nd";
            }

            if (lastDigit == 3)
            {
                return number + "rd";
            }
            return number + "th";



        }
        static void Throw(string diceRoll)
        {
            string diceTypePath = "dice types.txt";
            string diceTypeText = File.ReadAllText(diceTypePath);
            //Isolate each image
            string[] diceTypeImages = diceTypeText.Split("\n\n");

            try
            {
                List<int> rolls;
                int totalResult = DiceRoll(diceRoll, out rolls);

                //Match if the roll matches with any of the images
                string selectedDiceTypeImage = null;
                Match diceSidesMatch = Regex.Match(diceRoll, "d\\d+");
                string diceSidesText = diceSidesMatch.Value;

                foreach (string image in diceTypeImages)
                {
                    if (image.Contains(diceSidesText))
                    {
                        selectedDiceTypeImage = image;
                    }
                }

                //Display all rolls
                int number = 1;

                foreach (int roll in rolls)
                {
                    if (selectedDiceTypeImage == null)
                    {
                        string ordinalNumber = OrdinalNumber(number);
                        Console.WriteLine($"{ordinalNumber} roll is: {roll}");
                    }
                    else
                    {
                        //Determen console color
                        Match colorMatch = Regex.Match(selectedDiceTypeImage, "\\s(\\d+)");
                        int colorNumber = Convert.ToInt32(colorMatch.Groups[1].Value);
                        ConsoleColor consoleColor = (ConsoleColor)colorNumber;
                        Console.ForegroundColor = consoleColor;

                        //Determen replaced text
                        string rollText = roll.ToString();
                        string questionMarks;
                        if (Regex.IsMatch(diceSidesText, "\\d\\d") && diceSidesText != "d10")
                        {
                            questionMarks = "??";
                            if (!Regex.IsMatch(rollText, "\\d\\d"))
                            {
                                rollText = $" {roll}";
                            }
                        }
                        else
                        {
                            questionMarks = "?";
                            if (roll == 10)
                            {
                                rollText = "0";
                            }
                        }
                        string rollImage = selectedDiceTypeImage.Replace(questionMarks, rollText);
                        int newLineIndex = rollImage.IndexOf('\n');
                        rollImage = rollImage.Substring(newLineIndex + 1);
                        Console.WriteLine(rollImage);
                    }
                    number++;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"You rolled: {totalResult}");
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
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus, out List<int> rolls)
        {
            var random = new Random();
            rolls = new List<int>();
            int result = 0;

            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
                rolls.Add(roll);
            }
            result += fixedBonus;
            return result;
        }
        static int DiceRoll(string diceRoll, out List<int> rolls)
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

            return DiceRoll(numbersOfRolls, diceSides, fixedBonus, out rolls);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("DICE SIMULATOR");
            string userInput = "";
            string answer = "n";

            do
            {
                if (answer == "r")
                {
                    Console.WriteLine("Simulating...");
                    Throw(userInput);
                    Console.WriteLine("Do you want to continue? Press (r)epeat, (n)ew roll or (q)uit");
                }
                else if (answer == "n")
                {
                    Console.WriteLine("Enter desired dice roll in standard dice notation:");
                    userInput = Console.ReadLine();

                    if (IsStandardDiceNotation(userInput) == true)
                    {
                        Console.WriteLine("Simulating...");
                        Throw(userInput);
                        Console.WriteLine("Do you want to continue? Press (r)epeat, (n)ew roll or (q)uit");
                    }
                    else
                    {
                        Throw(userInput);
                        Console.WriteLine("You didn't use standard dice notation. Try again!");
                    }
                }
                else
                {
                    break;
                }

                if (IsStandardDiceNotation(userInput) == true)
                {
                    answer = Console.ReadLine();
                }

            } while (true);


        }
    }
}
