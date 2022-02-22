using System;
using System.Collections.Generic;

namespace dictionariesBossLevel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary <string, int> score = new Dictionary<string, int>();

            while (true)
            {
                Console.WriteLine("Who won the round?");
                string name = Console.ReadLine();
                if (name == "")
                {
                    return;
                }

                if (score.ContainsKey(name))
                {
                    score[name]++;
                }
                else
                {
                    score.Add(name, 1);
                }
                Console.WriteLine();

                foreach (KeyValuePair <string, int> kvp in score)
                {
                    Console.WriteLine($"{kvp.Key} {kvp.Value}");
                }
                Console.WriteLine();
            }

        }
    }
}
