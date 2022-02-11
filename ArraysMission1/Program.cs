using System;

namespace ArraysMission1
{
    internal class Program
    {
        enum Seasons
        {
            Spring,
            Summer,
            Autumn,
            Winter,
        }
        static string OrdinalNumber(int number)
        {

            int lastDigit = number % 10;

            if (number > 10)
            {
                int secondToLastDigit = (number / 10) % 10;
                if (secondToLastDigit == 1)
                {
                    return number + "th";
                }
            }

            if (lastDigit == 1)
            {
                return number + "st";
            }

            if (lastDigit == 2)
            {
                return number + "nd";
            }

            if (lastDigit == 3)
            {
                return number + "rd";
            }
            return number + "th";



        }
        static string CreateDayDescription(int day, Seasons season, int year)
        {
            var specificSeasons = new string[4] { "Spring", "Summer", "Autumn", "Winter" };

            string specificSeason = specificSeasons[(int)season];

            string dayText = OrdinalNumber(day);
            return $"{dayText} day of {specificSeason} in the year {year}";

        }
        static void Main(string[] args)
        {
            Console.WriteLine(CreateDayDescription(25, Seasons.Summer, 2000));
            Console.WriteLine(CreateDayDescription(12, Seasons.Spring, 1790));
            Console.WriteLine(CreateDayDescription(27, Seasons.Winter, 497));
        }
    }
}
