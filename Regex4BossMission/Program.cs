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
            string htmlCode = httpClient.GetStringAsync(@"https://store.steampowered.com/app/814680/Unbound_Worlds_Apart/").Result;
            Match result = Regex.Match(htmlCode, "(<\\w+\\s\\w+=\"\\w+\\s\\w+\"\\s\\w+=\"\\w+\">(\\S+\\s\\w+)<\\/\\w+>)");
            if (result.Success)
            {
                Console.WriteLine($"The rating of the game Unbound: Worlds Apart is {result}");
            }

        }
    }
}
