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


            foreach (string monsterStatBlock in monsterStatBlocks)
            {
                string[] monsterStatLines = monsterStatBlock.Split("\n");
                string monsterName = monsterStatLines[0];
                bool manualSearch = Regex.IsMatch(monsterStatBlock, "fly");
                Console.WriteLine($"{monsterName} - Can fly: {manualSearch}");
            }


        }
    }
}
