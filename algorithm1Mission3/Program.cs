using System;

namespace algorithm1Mission3
{
    internal class Program
    {
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
        static void Main(string[] args)
        {
            for (int i = 1; i < 250; i++)
            {
                string calledNumber = OrdinalNumber(i);
                Console.WriteLine(calledNumber);
            }

        }
    }
}
