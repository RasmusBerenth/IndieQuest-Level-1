using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MonsterManual
{
    class MonsterEntry
    {
        public string Name;
        public string Description;
        public string Alignment;
        public string HitPoint;
        public List<MonsterEntry> MonsterEntries = new List<MonsterEntry>();
    }
    internal class Program
    {
        static void DisplayMonster(MonsterEntry monster)
        {
            Console.WriteLine(monster.Name);
            Console.WriteLine(monster.Description);
            Console.WriteLine(monster.Alignment);
            Console.WriteLine(monster.HitPoint);
        }
        static void Main(string[] args)
        {
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


                monster.Name = monsterName;
                monster.Description = monsterDesciption.Groups[1].Value;
                monster.Alignment = monsterAlignment.Groups[1].Value;
                monster.HitPoint = monsterHitPont.Value;
                monsterEntries.Add(monster);

            }

            DisplayMonster(monsterEntries[8]);


        }
    }
}
