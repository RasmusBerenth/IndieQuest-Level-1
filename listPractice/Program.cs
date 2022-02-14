using System;
using System.Collections.Generic;


namespace listPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part 1
            //1. Ask the user to enter a number. Store it into a variable called n. Examples below will be given for value n = 50.
            Console.WriteLine("Pleas enter a number here:");
            int n = 50;// Convert.ToInt32(Console.ReadLine());

            //2. Create a list with n random integers in the range 0–99. Output the list.
            var random = new Random();
            int randomInteger = 0;
            var listOfNumbers = new List<int>();

            for (int i = 0; i < n; i++)
            {
                randomInteger = random.Next(100);
                listOfNumbers.Add(randomInteger);
            }

            Console.WriteLine($"Number are: {String.Join(", ", listOfNumbers)}");
            Console.WriteLine();

            //3. Calculate the sum of all the numbers.
            int sumOfNumbers = 0; 
            foreach (int number in listOfNumbers)
            {
                sumOfNumbers += number;
            }
            //for (int i = 0;i < n; i++)
            //{
            //    sumOfList += listOfNumbers[i];
            //}
            Console.WriteLine($"The sum of the list is: {sumOfNumbers}");

            //4. Calculate the average of the numbers
            double avarageOfList = (double)sumOfNumbers / n;
            Console.WriteLine($"The avarage of the list is: {avarageOfList}");
            Console.WriteLine();

            //5. Calculate the product of the first 10 numbers.
            long productOfFirstTen = 1;
            for (int i = 0; i < 10; i++)
                {
                    productOfFirstTen *= listOfNumbers[i];
                }
                Console.WriteLine(productOfFirstTen);

            //6. Sort the numbers and output the sorted list.
            //7. Create a new list with just the even numbers from the sorted list.
            //8. Create a new list with just the 10 largest numbers from the sorted list.
            //9. Create a new list with the 10 largest unique numbers from the sorted list (numbers can't repeat themselves).
            //10. Write how many unique numbers are in the whole original list.
            //11. Write which numbers from 0–99 are missing in the list.
            //12. Draw a histogram of the numbers with 10 bins (each bin counts how many numbers fall into that bin).


        }
    }
}
