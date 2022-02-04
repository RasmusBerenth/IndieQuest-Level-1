using System;

namespace firstGame3
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var random = new Random();
            int diceResult;
            int strenght = 0;
            int rollCount = 0;
            do
            {
                diceResult = random.Next(1, 7);
                strenght += diceResult;
                rollCount++;
            }
            while (rollCount < 3); //roll 3 times
            Console.WriteLine($"A character with strenght {strenght} was created");

            int cubeHP = 0;
            for (int i = 0; i < 8; i++)
            {
                diceResult = random.Next(1, 11);
                cubeHP += diceResult;
            }
            cubeHP += 40;
            Console.WriteLine($"A gelatinous cube with {cubeHP} HP apperad!");

            int cubeHoardHP = 0;
            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    diceResult = random.Next(1, 11);
                    cubeHoardHP += diceResult;
                }
                cubeHoardHP += 40;
            }

            Console.WriteLine($"Dear gods, an army of 100 cubes decends upon us with a total of {cubeHoardHP} HP. We are doomed!");
        }
    }
}
