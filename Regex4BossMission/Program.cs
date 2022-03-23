using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Regex4BossMission
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            string[] gameUrls = new string[]
            {
             "https://store.steampowered.com/app/205100/Dishonored/", //overvelming positiv
             "https://store.steampowered.com/app/814680/Unbound_Worlds_Apart/", //very positiv
             "https://store.steampowered.com/app/1592110/Spirit_of_the_Island/", //mostly positiv
             "https://store.steampowered.com/app/1063730/New_World/", //mixed
             "https://store.steampowered.com/app/1517290/Battlefield_2042/" // mostly negative
            };

            foreach (string url in gameUrls)
            {
                string htmlCode = httpClient.GetStringAsync(url).Result;
                MatchCollection results = Regex.Matches(htmlCode, @"<div class="".*?subtitle.*?"">([^<]*?)</div>[\n\s\r]*?<div class=""summary column"">[\n\s\r]*?<span.*?class="".*?game_review_summary.*?"".*?>([^<]*?)</span>", RegexOptions.Singleline);
                Console.WriteLine(url);
                foreach (Match result in results)
                {
                    if (results.Count == 4)
                    {


                        if (result.Success)
                        {
                            Console.WriteLine($"{result.Groups[1]} {result.Groups[2]}");
                        }
                        else
                        {
                            Console.WriteLine("Could not find review.");
                        }
                    }
                    else if (result.Equals("All"))
                    {

                    }
                    else
                    {

                    }
                }
                Console.WriteLine();

            }
        }
    }
}
