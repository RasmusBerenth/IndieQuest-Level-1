using System;
using System.Collections.Generic;

namespace Enums
{
    enum Suits
    {
        Heart,
        Spades,
        Diamonds,
        Clover,
    }
    internal class Program
    {
        static void DrawAce(Suits suit)
        {
            //Other alternativs
            //var suitSymbols = new List<string> { "♥", "♠", "♦", "♣" };
            //var suitSymbolsArray = new string[] { "♥", "♠", "♦", "♣" };
            //var suitSymbolsArrayChar = new char[] { '♥', '♠', '♦', '♣' };
            string suitSymbols = "♥♠♦♣";

            char suitSymbol = suitSymbols[(int)suit];
            string ace = @"
╭───────╮
│A      │
│♠      │
│   ♠   │
│      ♠│
│      A│
╰───────╯
";

            Console.Write(ace.Replace('♠', suitSymbol));

        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < 4; i++)
            {
                DrawAce((Suits)i);
            }
            
            //Other alternativs
            //for (Suits i = Suits.Heart; i <= Suits.Clover; i++)
            //{
            //    DrawAce(i);
            //}

        }
    }
}
