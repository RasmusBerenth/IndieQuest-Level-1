using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Regex1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string monsterManualPath = "MonsterManual.txt";
            string monsterManualText = File.ReadAllText(monsterManualPath);
            string[] monsterStatBlocks = monsterManualText.Split("\n\n");

            Console.WriteLine("Can fly 10 - 40 feet per turn:");
            foreach (string monsterStatBlock in monsterStatBlocks)
            {
                string[] monsterStatLines = monsterStatBlock.Split("\n");
                string monsterName = monsterStatLines[0];
                //Solar, Planetar, Roc 100+(del)
                if (Regex.IsMatch(monsterStatBlock, "fly [1234]0 "))
                {
                    Console.WriteLine(monsterName);
                }

            }

        }
    }
}
