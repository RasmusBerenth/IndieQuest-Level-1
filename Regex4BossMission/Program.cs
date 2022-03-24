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
                Match title = Regex.Match(url, "https://store\\.steampowered\\.com/app/\\d+/(\\S+)/");
                if (title.Success)
                {
                    Console.WriteLine(title.Groups[1]);
                }

                Match result = results[0];
                Console.WriteLine($"{result.Groups[1]} {result.Groups[2]}");
                if (results.Count > 2)
                {
                    result = results[1];
                    Console.WriteLine($"{result.Groups[1]} {result.Groups[2]}");
                }

                Console.WriteLine();

            }
        }
    }
}
