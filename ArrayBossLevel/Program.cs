using System;
using System.Collections.Generic;

namespace ArrayBossLevel
{
    internal class Program
    {
        static void GenerateIntersection(bool[,] roads, int x, int y)
        {
            var random = new Random();

            for (int i = 0; i < 4; i++)
            {
                var newIntersection = random.Next(100);

                if (newIntersection < 70)
                {
                    GenerateRoad(roads, x, y, i);
                }

            }

        }
        static void GenerateRoad(bool[,] roads, int startX, int startY, int direction)
        {
            int width = roads.GetLength(0);
            int height = roads.GetLength(1);
            if (direction == 0)
            {
                for (int x = startX; x < width; x++)
                {
                    roads[x, startY] = true;
                }
            }
            else if (direction == 1)
            {
                for (int y = startY; y < height; y++)
                {
                    roads[startX, y] = true;
                }
            }
            else if (direction == 2)
            {
                for (int x = startX; x > -1; x--)
                {
                    roads[x, startY] = true;
                }
            }
            else
            {
                for (int y = startY; y > -1; y--)
                {
                    roads[startX, y] = true;
                }
            }

        }
        static void Main(string[] args)
        {
            //Prepar map (remember -1)
            int height = 25;
            int width = 48;
            var roads = new bool[width, height];


            //Generate roads
            roads[0, 0] = true;
            roads[width - 1, height - 1] = true;
            GenerateRoad(roads, 10, 5, 0);
            GenerateRoad(roads, 15, 10, 2);
            GenerateRoad(roads, 20, 21, 1);
            GenerateRoad(roads, 30, 15, 3);
            GenerateIntersection(roads, 15, 15);

            //Draw map
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    if (!roads[x, y])
                    {
                        Console.Write(".");
                        continue;
                    }

                    bool rightCheck = (x < width - 1 && roads[x + 1, y]);
                    bool leftCheck = (x > 0 && roads[x - 1, y]);
                    bool downCheck = (y < height - 1 && roads[x, y + 1]);
                    bool upCheck = (y > 0 && roads[x, y - 1]);

                    if (rightCheck && leftCheck)
                    {
                        Console.Write("═");
                    }
                    else if (upCheck && downCheck)
                    {
                        Console.Write("║");
                    }
                    else if (upCheck && downCheck && leftCheck && downCheck)
                    {
                        Console.Write("╬");
                    }
                    else if (upCheck && downCheck && leftCheck)
                    {
                        Console.Write("╣");
                    }
                    else if (upCheck && downCheck && rightCheck)
                    {
                        Console.Write("╠");
                    }
                    else if (rightCheck && leftCheck && downCheck)
                    {
                        Console.Write("╦");
                    }
                    else if (rightCheck && leftCheck && upCheck)
                    {
                        Console.Write("╩");
                    }
                    else if (leftCheck && downCheck)
                    {
                        Console.Write("╗");
                    }
                    else if (leftCheck && upCheck)
                    {
                        Console.Write("╝");
                    }
                    else if (rightCheck && upCheck)
                    {
                        Console.Write("╚");
                    }
                    else
                    {
                        Console.Write("╔");
                    }


                }
                Console.WriteLine();

            }


        }
    }
}
