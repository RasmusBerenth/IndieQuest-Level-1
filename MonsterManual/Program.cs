using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    enum ArmorType
    {
        Unspecified,
        Natural,
        Leather,
        StuddedLeather,
        Hide,
        ChainShirt,
        ChainMail,
        ScaleMail,
        Plate,
        Other
    }
    class ArmorInformation
    {
        public int Class;
        public ArmorType Type;
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
                Match monsterAlignment = Regex.Match(monsterStatLines[1], "(, shapechanger\\))?, (.*)");
                Match monsterHitPont = Regex.Match(monsterStatLines[2], "\\d+d\\d+(\\+\\d+)?");
                Match monsterArmor = Regex.Match(monsterStatLines[3], "Armor Class: (\\d+) (?:\\((.*)\\))?");

                monster.Name = monsterName;
                monster.Description = monsterDesciption.Groups[1].Value;
                monster.Alignment = monsterAlignment.Groups[2].Value;
                monster.HitPoint = monsterHitPont.Value;
                if (monsterArmor.Groups[2].Success)
                {
                    string armorTypeText = monsterArmor.Groups[2].Value;

                    if (Regex.IsMatch(armorTypeText, "natural", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.Natural;
                    }
                    else if (Regex.IsMatch(armorTypeText, "leather armor", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.Leather;
                    }
                    else if (Regex.IsMatch(armorTypeText, "studded leather", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.StuddedLeather;
                    }
                    else if (Regex.IsMatch(armorTypeText, "hide", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.Hide;
                    }
                    else if (Regex.IsMatch(armorTypeText, "chain shirt", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.ChainShirt;
                    }
                    else if (Regex.IsMatch(armorTypeText, "chain mail", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.ChainMail;
                    }
                    else if (Regex.IsMatch(armorTypeText, "scale mail", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.ScaleMail;
                    }
                    else if (Regex.IsMatch(armorTypeText, "plate", RegexOptions.IgnoreCase))
                    {
                        monster.Armor.Type = ArmorType.Plate;
                    }
                    else
                    {
                        monster.Armor.Type = ArmorType.Other;
                    }
                }
                else
                {
                    monster.Armor.Type = ArmorType.Unspecified;
                }
                monster.Armor.Class = Convert.ToInt32(monsterArmor.Groups[1].Value);
                monsterEntries.Add(monster);
            }

            //Prompt for user input and let user chose between name and armor. (n or a)
            Console.WriteLine("MONSTER MANUAL");
            Console.WriteLine("Would you like to search by (n)ame or (a)rmor");
            string searchMethod = Console.ReadLine();
            var matchedMonsters = new List<MonsterEntry>();
            if (searchMethod == "n")
            {
                //Output chosen search method.
                Console.WriteLine("Eneter a query to search monster by name:");
                do
                {
                    string userInput = Console.ReadLine();

                    //Output a list of numbered monster matching input.
                    int counter = 0;
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
                        //If no monster found in the search ask for new input.
                        Console.WriteLine("No found monsters, try again");
                    }
                    else
                    {
                        //If only one monster was found display info.
                        DisplayMonster(matchedMonsters[0]);
                        break;
                    }

                } while (true);
            }
            else if (searchMethod == "a")
            {
                //Output chosen search method.
                Console.WriteLine("Enter an armor type to search by:");

                //Display list of numbered armor types.
                string[] armorTypeNames = Enum.GetNames<ArmorType>();
                int counter = 0;
                foreach (string armorTypeName in armorTypeNames)
                {
                    counter++;
                    Console.WriteLine($"{counter}. {armorTypeName}");
                }

                //Select armor type by number.
                int selectedArmorTypeNumber = Convert.ToInt32(Console.ReadLine());
                ArmorType[] armorTypes = Enum.GetValues<ArmorType>();
                ArmorType selectedArmorType = armorTypes[selectedArmorTypeNumber - 1];

                //Display monsters with selected armor type.
                foreach (MonsterEntry monsterEntry in monsterEntries)
                {
                    if (monsterEntry.Armor.Type == selectedArmorType)
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {monsterEntry.Name}");
                        matchedMonsters.Add(monsterEntry);
                    }

                }

                //Select monster.
                if (counter > 1)
                {
                    Console.Clear();
                    Console.WriteLine("Which monster would you like to look up?");
                    counter = 0;
                    foreach (MonsterEntry monsterEntry in matchedMonsters)
                    {
                        counter++;
                        Console.WriteLine($"{counter}. {monsterEntry.Name}");
                    }

                    int chosenMonsterNumber = Convert.ToInt32(Console.ReadLine());
                    //Confirm monster.
                    MonsterEntry chosenMonster = matchedMonsters[chosenMonsterNumber - 1];
                    DisplayMonster(chosenMonster);
                }
                else if (counter == 0)
                {
                    //If no monster found in the search ask for new input.
                    Console.WriteLine("No found monsters, try again");
                }
                else
                {
                    //If only one monster was found display info.
                    DisplayMonster(matchedMonsters[0]);
                }

            }
            else
            {
                //If user tries to search by method other than name or armor.
                Console.WriteLine("This wasen't an option...");
            }
        }
    }
}
