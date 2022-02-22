using System;
using System.Collections.Generic;

namespace DictionariesMissions
{
    internal class Program
    {
        static void Main(string[] args)
        {
             Console.WriteLine("Question 1:");
            //Mission 1
            //Dictonary with years(Keys) and countries(Values).
            Dictionary <int,string> hostCountrey = new Dictionary<int, string>();
            hostCountrey.Add(2000, "Australia");
            hostCountrey.Add(2002, "United States"); 
            hostCountrey.Add(2004, "Greece"); 
            hostCountrey.Add(2006, "Italy"); 
            hostCountrey.Add(2008, "China"); 
            hostCountrey.Add(2010, "Canada"); 
            hostCountrey.Add(2012, "United Kingdom"); 
            hostCountrey.Add(2014, "Russia"); 
            hostCountrey.Add(2016, "Brazil"); 
            hostCountrey.Add(2018, "South Korea"); 
            hostCountrey.Add(2020, "Japan"); 
            hostCountrey.Add(2022, "China"); 

            //Creating random years between 2000 and 2020 and making each year corespond to a entry in the dictonary.
            var random = new Random();
            int randomYearIndex = random.Next(0, hostCountrey.Count);
            var years = new List<int>(hostCountrey.Keys);
            int questionYear = years[randomYearIndex];
            string correctAnswer = hostCountrey[questionYear];
            
            //Asking questinons and cheacking wheater given answers where correct or not.    
            Console.WriteLine($"Who was the host country of the olympics in the year {questionYear}?");
                string answer = Console.ReadLine();

                if (answer == correctAnswer)
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Incorrect, the answer was {correctAnswer}.");
                }
                Console.WriteLine();


            Console.WriteLine("Question 2:");
            //Mission 2
            //Dictonary with counries(Keys) and capitals(Values).
            Dictionary<string, string> countryCapitals = new Dictionary<string, string>();
            countryCapitals.Add("Sweden", "Stockholm");
            countryCapitals.Add("Denmark", "Copenhagen");
            countryCapitals.Add("Norway", "Oslo");
            countryCapitals.Add("Finland", "Helsinki");
            countryCapitals.Add("Iceland", "Rejkavík");
            countryCapitals.Add("Greenland", "Nuuk");
            countryCapitals.Add("United Kingdom", "London");
            countryCapitals.Add("Italy", "Rome");
            countryCapitals.Add("Andorra", "Andorra la Vella");
            countryCapitals.Add("Nepal", "Kathmandu");

            
            int randomCountryIndex = random.Next(0, countryCapitals.Count);
            var country = new List<string>(countryCapitals.Keys);
            string questionCountry = country[randomCountryIndex];
            string correctAnswer2 = countryCapitals[questionCountry];
            
            Console.WriteLine($"What is the capital of {questionCountry}?");
            string answer2 = Console.ReadLine();

            if (answer2 == correctAnswer2)
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Incorrect, the answer was {correctAnswer2}.");
            }


        }
    }
}
