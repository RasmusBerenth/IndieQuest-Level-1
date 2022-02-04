using System;
using System.Collections.Generic;

namespace fundamentalFinalBossp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            int constitution = 5;

            var names = new List<string>();
            names.Add("Ken");
            names.Add("Barby");
            names.Add("Roland");
            names.Add("Melissa");

            Console.WriteLine($"A party of warriors {String.Join(", ", names)} descends into the dungeon.");

            int basiliskHP = 0;
            for (int i = 0; i < 8; i++)
            {
                int newRoll = random.Next(1, 9);
                basiliskHP += newRoll;
            }
            basiliskHP += 16;

            Console.WriteLine();
            Console.WriteLine($"A basilisk with {basiliskHP}HP appears");
            Console.WriteLine();

            do
            {
                foreach (var name in names)
                {
                    int damage = random.Next(1, 5);
                    basiliskHP -= damage;

                    if (basiliskHP <= 0)
                    {
                        Console.WriteLine($"{name} hits the basilisk for {damage} damage. Basilisk has 0 HP left.");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{name} hits the basilisk for {damage} damage. Basilisk has {basiliskHP} HP left.");
                        Console.WriteLine();
                    }
                }

                if (basiliskHP <= 0)
                {
                    break;
                }

                int tarrgetIndex = random.Next(names.Count);
                string targetName = names[tarrgetIndex];

                Console.WriteLine($"The basilisk uses petrifying gaze on {targetName}!");
                Console.WriteLine();

                int newRoll = random.Next(1, 21);

                if (constitution + newRoll < 12)
                {
                    Console.WriteLine($"{targetName} turned to stone");
                    names.Remove(targetName);
                }
                else
                {
                    Console.WriteLine($"{targetName} rolls a {newRoll} and is saved from the attack.");
                }

                Console.WriteLine();
            }
            while (names.Count > 0);

            if (basiliskHP > 0)
            {
                Console.WriteLine("Your party has died and the basilisk will ravish the lands!");
            }
            else
            {
                Console.WriteLine("The basilisk collapses and the heroes celebrate their victory!");
            }
        }
    }
}
