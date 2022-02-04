using System;

namespace BranchesAndLoops
{
    internal class Program
    {
        static void ExploreIf()
        {
            int sum = 0;
            for (int i = 1; i < 21; i++)
            {
                if (i % 3 == 0)
                {
                    sum = sum + i;
                }
            }
            Console.WriteLine($"The sum is {sum}.");
        }
        
        static void Main(string[] args)
        {
            ExploreIf();
        }
    }
}
