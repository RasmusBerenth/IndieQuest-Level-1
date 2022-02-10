using System;

namespace SwitchMissions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set price:");
            string equation = Console.ReadLine();
            string[] equationParts = equation.Split(' '); //list first double[0] symbol[1] second double[2] --> first double[0] symbol[1] symbol [2] second double[3]
            double price;
            double firstNumber = Convert.ToDouble(equationParts[0]);

            if (equationParts.Length == 1)
            {
                price = firstNumber;
            }
            else
            {
                string symbol = equationParts[1];

                if (equationParts.Length == 4)
                {
                    symbol = $"{equationParts[1]} {equationParts[2]}";
                }

                double secondNumber = Convert.ToDouble(equationParts[equationParts.Length-1]);



                switch (symbol)
                {
                    //addition if price contains +, plus or add
                    case "+":
                    case "plus":
                    case "add":
                        price = firstNumber + secondNumber;
                        break;

                    //subtraction if price contains -, minus or subtract
                    case "-":
                    case "subtract":
                    case "minus":
                        price = firstNumber - secondNumber;
                        break;
                    
                    //multiplication if price contains *, times or multiply by
                    case "*":
                    case "times":
                    case "multiplied by":
                        price = firstNumber * secondNumber;
                        break;
                       
                    //division if price contains / or divided by
                    case "/":
                    case "divided by":
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
