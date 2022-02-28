using System;

namespace arrayPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.
            string[] weekDays = new string[31] { "Mon", "Tue", "Wen", "Thu", "Fri", "Sat", "Sun" };
            Console.WriteLine($"The days of the week are: {String.Join(",", weekDays)}");

            //2.
            for (int i = 0; i < 31; i++)
            {
                Console.Write($"{i+1}: {weekDays[i]}, ");
            }

            //3.


            //4.
        }
    }
}
