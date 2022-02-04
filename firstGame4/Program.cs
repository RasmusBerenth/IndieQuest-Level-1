using System;

namespace firstGame4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Danger! A tank is approching our position. Your artilery unit is our only hope!");
            var random = new Random();
            int tankDistance = random.Next(40, 71);

            //Storing the players name
            Console.WriteLine("What is your name, commander?");
            Console.Write("Your name is: ");
            string playerName = Console.ReadLine();
            
            int playersChoosenDistance = 0;

            do
            {
                //Clear the screen before battlefield.
                Console.Clear();

                //Battlefield statment
                Console.WriteLine($"Here is the battlefield:");

                //Draw artillery
                Console.Write($"_/");

                //Draw underscores before tank
                for (int x = 1; x < tankDistance; x++)
                {
                    Console.Write("_");

                }

                //Draw tank
                Console.Write("T");

                //Draw underscores after tank
                for (int x = tankDistance + 1; x < 79; x++)
                {
                    Console.Write("_");
                }
                Console.WriteLine();

                //Asking for the distance to fire at (using player name)
                Console.WriteLine($"Aim your shot," + playerName);
                Console.Write("Enter distance: ");

                playersChoosenDistance = Convert.ToInt32(Console.ReadLine());

                //Draw marker where explosion hit
                for (int x = -1; x < playersChoosenDistance; x++)
                {
                    Console.Write(" ");

                }

                Console.WriteLine("*");

                //Three responses hit, too short, too long
                if (playersChoosenDistance == tankDistance)
                {
                    Console.WriteLine("Hit");
                    break;
                }
                else if (playersChoosenDistance < tankDistance)
                {
                    Console.WriteLine("Too short");
                }
                else
                {
                    Console.WriteLine("Too long");

                }
                //Move tank closer to artillery.
                tankDistance -= random.Next(1, 15);

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            while (tankDistance > 0);

            //Game over message
            if (playersChoosenDistance == tankDistance)
            {
                Console.WriteLine("Good shooting commander, the tank has been eliminated!");
            }
            else
            {
                Console.WriteLine("We failed to stop the tank in time, it is going to overrun us! GAME OVER");
            }

        }
    }
}
