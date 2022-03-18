using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Regex2
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
                string monsterStatBlockSecondLine = monsterStatLines[1];
                string[] monsterSizeAndAlignment = monsterStatBlockSecondLine.Split(", ");
                string monsterAlignment = monsterSizeAndAlignment[1];
                if (Regex.IsMatch(monsterAlignment, "(lawful|chaotic|neutral|good|evil)"))
                {
                    Console.WriteLine($"{monsterName} ({monsterAlignment})");
                }

            }
        }
    }
}
