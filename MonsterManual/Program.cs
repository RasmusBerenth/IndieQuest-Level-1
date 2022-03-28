using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class ArmorInformation
    {
        public int Class;
        public string Type;
    }
    class MonsterEntry
    {
        public string Name;
        public string Description;
        public string Alignment;
        public string HitPoint;
        public ArmorInformation Armor = new ArmorInformation();
    }
    internal class Program
    {
        static void DisplayMonster(MonsterEntry monster)
        {
            Console.Clear();
            Console.WriteLine(monster.Name);
            Console.WriteLine(monster.Description);
            Console.WriteLine(monster.Alignment);
            Console.WriteLine(monster.HitPoint);
            Console.WriteLine(monster.Armor.Class);
            Console.WriteLine(monster.Armor.Type);
        }
        static void Main(string[] args)
        {
            //Creating the list of monsters from the monster manual.
            var monsterEntries = new List<MonsterEntry>();
            string monsterManualPath = "MonsterManual.txt";
            string monsterManualText = File.ReadAllText(monsterManualPath);
            string[] monsterStatBlocks = monsterManualText.Split("\n\n");
            foreach (string monsterStatBlock in monsterStatBlocks)
            {
                var monster = new MonsterEntry();

                string[] monsterStatLines = monsterStatBlock.Split("\n");
                string monsterName = monsterStatLines[0];
                Match monsterDesciption = Regex.Match(monsterStatLines[1], "(.*),");
                Match monsterAlignment = Regex.Match(monsterStatLines[1], ", (.*)");
                Match monsterHitPont = Regex.Match(monsterStatLines[2], "\\d+d\\d+(\\+\\d+)?");
                Match monsterArmor = Regex.Match(monsterStatLines[3], "Armor Class: (\\d+) (\\((.*)\\))?");

                Convert.ToInt32(monsterArmor); //??? Parse, TryParse

                monster.Name = monsterName;
                monster.Description = monsterDesciption.Groups[1].Value;
                monster.Alignment = monsterAlignment.Groups[1].Value;
                monster.HitPoint = monsterHitPont.Value;
                monster.Armor.Type = monsterArmor.Groups[3].Value;
                monster.Armor.Class = monsterArmor.Groups[1].Value;
                monsterEntries.Add(monster);
            }

            //Prompt for user input.
            Console.WriteLine("MONSTER MANUAL");
            Console.WriteLine("Eneter a query to search monster by name:");

            do
            {
                string userInput = Console.ReadLine();

                //Output a list of numbered monster matching input.
                int counter = 0;
                var matchedMonsters = new List<MonsterEntry>();
                foreach (MonsterEntry monsterEntry in monsterEntries)
                {
                    Match result = Regex.Match(monsterEntry.Name, userInput, RegexOptions.IgnoreCase);
                    if (result.Success)
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {monsterEntry.Name}");
                        matchedMonsters.Add(monsterEntry);
                    }
                }

                //ask for specific number, user picks number.
                if (counter > 1)
                {
                    Console.WriteLine("Which monster would you like to look up?");
                    int chosenMonsterNumber = Convert.ToInt32(Console.ReadLine());
                    //Confirm monster.
                    MonsterEntry chosenMonster = matchedMonsters[chosenMonsterNumber - 1];
                    DisplayMonster(chosenMonster);
                    break;
                }
                else if (counter == 0)
                {
                    //If no monster found in original search ask for new input.
                    Console.WriteLine("No found monsters, try again");
                }
                else
                {
                    //If only one monster was found display info, skipping the confirmation of the monster.
                    DisplayMonster(matchedMonsters[0]);
                    break;
                }

            } while (true);
        }
    }
}
