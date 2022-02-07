using System;
using System.Collections.Generic;

namespace Methods
{
    internal class Program
    {
        static void SimulateBattle(List<string> heroes, string monster, int monsterHP, int savingThrowDC)
        {
            var random = new Random();
            int constitution = 5;
            



            Console.WriteLine();
            Console.WriteLine($"A {monster} with {monsterHP}HP appears");
            Console.WriteLine();

            do
            {
                foreach (var name in heroes)
                {
                    int damage = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        int damageRoll = random.Next(1, 7);
                        damage += damageRoll;
                    }
                    monsterHP -= damage;
                    
                    if (monsterHP <= 0)
                    {
                        Console.WriteLine($"{name} hits the {monster} for {damage} damage. {monster} has 0 HP left.");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{name} hits the {monster} for {damage} damage. {monster} has {monsterHP} HP left.");
                        Console.WriteLine();
                    }
                }

                if (monsterHP <= 0)
                {
                    break;
                }

                int tarrgetIndex = random.Next(heroes.Count);
                string targetName = heroes[tarrgetIndex];

                Console.WriteLine($"The {monster} made a killing blow aginst {targetName}!");
                Console.WriteLine();

                int savingThrow = random.Next(1, 21);

                if (constitution + savingThrow < savingThrowDC)
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and was killed by the {monster}!");
                    heroes.Remove(targetName);
                }
                else
                {
                    Console.WriteLine($"{targetName} rolls a {savingThrow} and is saved from the attack.");
                }

                Console.WriteLine();
            }
            while (heroes.Count > 0);

            if (monsterHP > 0)
            {
                Console.WriteLine($"Your party has died and the {monster} will ravish the lands!");
            }
            else
            {
                Console.WriteLine($"The {monster} collapses and the heroes celebrate their victory!");
            }


        }
        static void Main(string[] args)
        {
            var names = new List<string>();
            names.Add("Ken");
            names.Add("Barby");
            names.Add("Roland");
            names.Add("Melissa");

            Console.WriteLine($"A party of warriors {String.Join(", ", names)} descends into the dungeon.");

            SimulateBattle(names, "orc", 15, 12);
            
            if (names.Count > 0)
            {
                SimulateBattle(names, "mage", 40, 20);

            }
            
            if (names.Count > 0)
            {
                SimulateBattle(names, "troll", 84, 18);
            }

            if (names.Count > 0)
            {
                Console.WriteLine($"{String.Join(", ", names)} survived the doungeon.");
            }
        }
    }
}
