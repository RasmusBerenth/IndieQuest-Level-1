using System;
using System.IO;
using System.Linq;

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
            string playerName;

            if (File.Exists(playerNamePath))
            {
                playerName = File.ReadAllText(playerNamePath);
                //On the second run.
                Console.WriteLine($"Welcome back, {playerName}, lets continue!");

            }
            else
            {
                Console.WriteLine("What is your name, traveler?");
                playerName = Console.ReadLine();


                //Creating the file, player-name.txt, and storing the player name in it.
                File.WriteAllText(playerNamePath, playerName);

                //On the first run.
                Console.WriteLine($"Nice to meet you {playerName}");

            }

            //Put the backers into an array.
            string[] backers = File.ReadAllLines("backers.txt");


            //Display whether the player can enter special area or not.
            if (backers.Contains(playerName))
            {
                //If the player is on the list of backers.
                Console.WriteLine("You successfully enter Dr.Fred's secret laboratory and are greeted with a warm welcome for backing the game's Kickstarter!");
            }
            else
            {
                //If the player is not on the list of backers.
                Console.WriteLine("Unfortunately I cannot let you into Dr.Fred's secret laboratory.");
            }




        }
    }
}
