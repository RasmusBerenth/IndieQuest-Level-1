using System;
using System.Collections.Generic;

namespace SwitchMissions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set price:");
            string equation = Console.ReadLine();
            string[] equationParts = equation.Split(' '); //list first double[0] symbol[1] second double[2]
            double price;
            double firstNumber = Convert.ToDouble(equationParts[0]);

            if (equationParts.Length == 1)
            {
                price = firstNumber;
            }
            else
            {
                string symbol = equationParts[1];
                double secondNumber = Convert.ToDouble(equationParts[2]);


                switch (symbol)
                {
                    //addition if price contains +
                    case "+":
                        price = firstNumber + secondNumber;
                        break;

                    //subtraction if price contains -
                    case "-":
                        price = firstNumber - secondNumber;
                        break;

                    //multiplication if price contains *
                    case "*":
                        price = firstNumber * secondNumber;
                        break;

                    //division if price contains /
                    case "/":
                        price = firstNumber / secondNumber;
                        break;

                    default:
                        Console.WriteLine("I don't know what you mean.");
                        return;
                }
            }
                Console.WriteLine($"The price was set to: {price}");



        }
    }
}
