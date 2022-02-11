using System;

namespace arraysMission2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            
            int[] monsters = new int[100];
            
            for (int i = 0; i < 100; i++)
            {
                int monstersPerLevel = random.Next(1, 51);
                monsters[i] = monstersPerLevel;
            }
            
            Array.Sort(monsters);
            
                Console.WriteLine($"Number of monsters in levels: {String.Join(" ", monsters)}");
            


        }
    }
}
