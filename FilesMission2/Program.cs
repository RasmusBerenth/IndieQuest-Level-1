using System;
using System.IO;

namespace FilesMission2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Starting text.
            Console.WriteLine("Welcome to your biggest adventure yet!");
            
            //If the file dosen't exist previously go to "first run" and if it does got to "second run".
            string playerNamePath = "player-name.txt";
            if (File.Exists(playerNamePath))
            {
                string playerName = File.ReadAllText(playerNamePath);
                //On the second run.
                Console.WriteLine($"Welcome back, {playerName}, lets continue!");
            }
            else
            {
                Console.WriteLine("What is your name, traveler?");
                string playerName = Console.ReadLine();
                //Creating the file, player-name.txt, and storing the player name in it.

                File.WriteAllText(playerNamePath, playerName);

                //On the first run.
                Console.WriteLine($"Nice to meet you {playerName}");

            }

            //Put the backers into an array.
            string[] backers = File.ReadAllLines("backers.txt");
            
            //Display whether they can enter special area or not.


        }
    }
}
