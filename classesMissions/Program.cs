using System;
using System.Collections.Generic;

namespace classesMissions
{
    class Location
    {
        public string Name;
        public string Description;
        public List<Location> Neighbors = new List<Location>();
    }
    internal class Program
    {
        static void ConnectLocations(Location a, Location b)
        {
            a.Neighbors.Add(b);
            b.Neighbors.Add(a);
        }
        static void Main(string[] args)
        {
            var locations = new List<Location>();

            var winterfell = new Location();
            winterfell.Name = "Winterfell";
            winterfell.Description = "the capital of the Kingdom of the North.";
            locations.Add(winterfell);

            var pyke = new Location();
            pyke.Name = "Pyke";
            pyke.Description = "the stronghold and seat of House Greyjoy.";
            locations.Add(pyke);

            var riverrun = new Location();
            riverrun.Name = "Riverrun";
            riverrun.Description = "a large castle located in the central-western part of the Riverlands.";
            locations.Add(riverrun);

            var trident = new Location();
            trident.Name = "The Trident";
            trident.Description = "one of the largest and most well-known rivers on the continent of Westeros.";
            locations.Add(trident);

            var kingsLanding = new Location();
            kingsLanding.Name = "King's Landing";
            kingsLanding.Description = "the capital, and largest city, of the Seven Kingdoms.";
            locations.Add(kingsLanding);

            var highgarden = new Location();
            highgarden.Name = "Highgarden";
            highgarden.Description = "the seat of House Tyrell and the regional capital of the Reach.";
            locations.Add(highgarden);

            ConnectLocations(trident, winterfell);
            ConnectLocations(winterfell, pyke);
            ConnectLocations(pyke, riverrun);
            ConnectLocations(pyke, highgarden);
            ConnectLocations(highgarden, riverrun);
            ConnectLocations(highgarden, kingsLanding);
            ConnectLocations(kingsLanding, trident);
            ConnectLocations(kingsLanding, riverrun);
            ConnectLocations(riverrun, trident);

            Location currentLocation = winterfell;
            while (true)
            {
                Console.WriteLine($"Welcome to {currentLocation.Name}, {currentLocation.Description}");
                Console.WriteLine($"Possible destinations are:");
                for (int i = 0; i < currentLocation.Neighbors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentLocation.Neighbors[i].Name}");
                }
                Console.WriteLine("Where do you want to travel?");
                string enteredNumberText = Console.ReadLine();
                int enteredNumber = Convert.ToInt32(enteredNumberText);
                int chosenLocationIndex = enteredNumber - 1;
                Location chosenLocation = currentLocation.Neighbors[chosenLocationIndex];
                currentLocation = chosenLocation;
            }


        }
    }
}
