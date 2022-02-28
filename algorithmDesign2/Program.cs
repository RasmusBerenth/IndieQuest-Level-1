using System;
using System.Collections.Generic;

namespace algorithmDesign2
{
    internal class Program
    {
        static int Factorial(int n)
        {
            // function factorial is:

            //input: integer n such that n >= 0

            //output:[n × (n - 1) × (n - 2) × ... × 1]

            // 1. if n is 0, return 1
            // 2.otherwise, return [n × factorial(n - 1)]

            //end factorial
            
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }


        }
        static void ShuffleList<T>(List<T> items)
        {
            //-- To shuffle an array a of n elements (indices 0..n-1):
            //for i from n−1 downto 1 do
            //        j ← random integer such that 0 ≤ j ≤ i
            //        exchange a[j] and a[i]


            var random = new Random();

            for (int i = items.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                //T itemJ = items[j];
                //items[j] = items[i];
                //items[i] = itemJ;

                (items[j], items[i]) = (items[i], items[j]);
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Mission 1");
            var names = new List<string> { "Allie", "Ben", "Claire", "Dan", "Eleanor" };
            Console.WriteLine($"Signed participants: { String.Join(", ", names)}");
            Console.WriteLine("Generating starting order...");

            ShuffleList(names);
            Console.WriteLine($"Shuffled list: { String.Join(", ", names)}");

            Console.WriteLine();
            Console.WriteLine("Mission 2");

            int factorial = Factorial(10);
            Console.WriteLine(factorial);
        }
    }
}
