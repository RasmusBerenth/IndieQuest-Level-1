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
                if (Regex.IsMatch(monsterStatBlock, "fly [1,2,3,4]"))
                {
                    Console.WriteLine(monsterName);
                }





                //string[] monsterHitPoint = monsterStatLines[2].Split("(");
                //if (monsterHitPoint.Length == 2)
                //{
                //    bool manualSearch = Regex.IsMatch(monsterHitPoint[1], "\\d{2}d");
                //    Console.WriteLine($"{monsterName} - 10+ dice rolls: {manualSearch}");
                //}
                //else
                //{
                //    Console.WriteLine($"{monsterName} - 10+ dice rolls: False");
                //}

            }


        }
    }
}
