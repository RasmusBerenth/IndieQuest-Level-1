using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Regex3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var namesByAlignment = new List<string>[3, 3];
            var namesOfUnaligned = new List<string>();
            var namesOfAnyAlignment = new List<string>();
            var namesOfSpecialCases = new List<string>();

            for (int axis1 = 0; axis1 < 3; axis1++)
            {
                for (int axis2 = 0; axis2 < 3; axis2++)
                {
                    namesByAlignment[axis1, axis2] = new List<string>();
                }
            }

            Dictionary<string, int> alignmentAxis1Indices = new Dictionary<string, int>
            {
                {"lawful",0 }, {"neutral", 1}, {"chaotic", 2}
            };
            Dictionary<string, int> alignementAxis2Indices = new Dictionary<string, int>
            {
                {"good",0 }, {"neutral", 1}, {"evil", 2}
            };

            string monsterManualPath = "MonsterManual.txt";
            string monsterManualText = File.ReadAllText(monsterManualPath);
            string[] monsterStatBlocks = monsterManualText.Split("\n\n");

            foreach (string monsterStatBlock in monsterStatBlocks)
            {
                string[] monsterStatLines = monsterStatBlock.Split("\n");
                string monsterName = monsterStatLines[0];

                string monsterStatBlockSecondLine = monsterStatLines[1];
                string[] monsterSizeTypeAndAlignment = monsterStatBlockSecondLine.Split(", ");
                string monsterAlignment = monsterSizeTypeAndAlignment[1];
                Match result = Regex.Match(monsterAlignment, "(lawful|chaotic|neutral) (good|evil|neutral)");
                if (result.Success)
                {
                    //Creature has typical alignment, sort it into alignment matrix

                    string axis1Text = result.Groups[1].Value;
                    string axis2Text = result.Groups[2].Value;

                    int axis1Index = alignmentAxis1Indices[axis1Text];
                    int axis2Index = alignementAxis2Indices[axis2Text];

                    namesByAlignment[axis1Index, axis2Index].Add(monsterName);

                }
                else
                {
                    //handle other cases
                    if (monsterAlignment == "neutral")
                    {
                        namesByAlignment[1, 1].Add(monsterName);
                    }
                    else if (monsterAlignment == "unaligned")
                    {
                        namesOfUnaligned.Add(monsterName);
                    }
                    else if (monsterAlignment == "any alignment")
                    {
                        namesOfAnyAlignment.Add(monsterName);
                    }
                    else
                    {
                        namesOfSpecialCases.Add(monsterName);
                    }
                }
            }

            List<string> outputList;
            string choosenAlignment = Console.ReadLine();
            Match result2 = Regex.Match(choosenAlignment, "(lawful|chaotic|neutral) (good|evil|neutral)");
            if (result2.Success)
            {
                //Creature has typical alignment, sort it into alignment matrix

                string axis1Text = result2.Groups[1].Value;
                string axis2Text = result2.Groups[2].Value;

                int axis1Index = alignmentAxis1Indices[axis1Text];
                int axis2Index = alignementAxis2Indices[axis2Text];
                outputList = namesByAlignment[axis1Index, axis2Index];

            }
            else
            {
                //handle other cases
                if (choosenAlignment == "neutral")
                {
                    outputList = namesByAlignment[1, 1];
                }
                else if (choosenAlignment == "unaligned")
                {
                    outputList = namesOfUnaligned;
                }
                else if (choosenAlignment == "any alignment")
                {
                    outputList = namesOfAnyAlignment;
                }
                else
                {
                    outputList = namesOfSpecialCases;
                }
            }


            foreach (string name in outputList)
            {
                Console.WriteLine(name);
            }




        }
    }
}
