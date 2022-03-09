﻿using System;
using System.Collections.Generic;

namespace AlgorithmMap
{
    internal class Program
    {
        static void GenerateCrossing()
        {
            //Find road river intersection
            int roadPositionX = 0;
            int riverPositionX;
            do
            {
                roadPositionX++;
                int roadPositionY = roadPositionsY[roadPositionX];
                riverPositionX = riverPositionsX[roadPositionY];
            }
            while (roadPositionX < riverPositionX);
            int intersectionX = roadPositionX;

            //Calculate bridge parameters
            int bridgeStartX = intersectionX - 3;
            int bridgeY = roadPositionsY[bridgeStartX];
            int bridgeEndX = bridgeStartX + 9;

            //Make road go straight across the bridge
            for (int bridgeX = bridgeStartX; bridgeX <= bridgeEndX && bridgeX < width; bridgeX++)
            {
                roadPositionsY[bridgeX] = bridgeY;
            }

            //Remake the road after the bridge

            GenerateRoad(bridgeEndX + 1, width, height, roadPositionsY);
        }
        static void GenerateRoad(int roadStartX, int width, int height, List<int> roadPositionsY)
        {
            var random = new Random();
            for (int x = roadStartX; x < width; x++)
            {
                int direction = random.Next(7);
                int newPositionY;
                int previousPositionY = roadPositionsY[x - 1];
                //Up
                if (direction == 0 && previousPositionY > 2)
                {
                    newPositionY = previousPositionY - 1;
                }
                //Down
                else if (direction == 1 && previousPositionY < height - 3)
                {
                    newPositionY = previousPositionY + 1;
                }
                //Straight
                else
                {
                    newPositionY = previousPositionY;
                }

                if (roadPositionsY.Count > x)
                {
                    roadPositionsY[x] = newPositionY;
                }
                else
                {
                    roadPositionsY.Add(newPositionY);
                }
            }
        }
        static void GenerateCurve(int startPositionX, int height, int curveChance, out List<int> positionsX, out List<int> directions)
        {
            var random = new Random();

            positionsX = new List<int>() { startPositionX };
            directions = new List<int>();

            for (int y = 1; y < height; y++)
            {
                int direction = random.Next(curveChance);
                int newPositionX;
                int previousPositionX = positionsX[y - 1];

                //Left
                if (direction == 0)
                {
                    newPositionX = previousPositionX - 1;
                }
                //Right
                else if (direction == 1)
                {
                    newPositionX = previousPositionX + 1;
                }
                //Straight
                else
                {
                    newPositionX = previousPositionX;
                }

                positionsX.Add(newPositionX);
                directions.Add(direction);
            }
        }
        static void DrawCurve(List<int> directions, int y)
        {
            int direction = directions[y];
            if (direction == 0)
            {
                Console.Write("/");
            }
            else if (direction == 1)
            {
                Console.Write(@"\");
            }
            else
            {
                Console.Write("|");
            }
        }
        static void DrawMap(int width, int height)
        {
            //1. Prepear the data
            var random = new Random();
            var treeSymbols = new string[] { "T", "@", "I" };
            string title = "Adventure Map";

            //    1.1 Create code for the river
            int riverStartPositionX = width * 3 / 4;
            List<int> riverPositionsX;
            List<int> riverDirections;

            GenerateCurve(riverStartPositionX, height, 3, out riverPositionsX, out riverDirections);

            // Create code for the wall
            int wallStartPositionX = width / 4;
            List<int> wallPositionsX;
            List<int> wallDirections;

            GenerateCurve(wallStartPositionX, height, 6, out wallPositionsX, out wallDirections);

            //    1.2 Create code for the road
            int roadStartPositionY = height / 2;
            var roadPositionsY = new List<int>() { roadStartPositionY };

            GenerateRoad(1, width, height, roadPositionsY);

            //    1.3 Create code for the bridge
            //Find road river intersection
            int roadPositionX = 0;
            int riverPositionX;
            do
            {
                roadPositionX++;
                int roadPositionY = roadPositionsY[roadPositionX];
                riverPositionX = riverPositionsX[roadPositionY];
            }
            while (roadPositionX < riverPositionX);
            int intersectionX = roadPositionX;

            //Calculate bridge parameters
            int bridgeStartX = intersectionX - 3;
            int bridgeY = roadPositionsY[bridgeStartX];
            int bridgeEndX = bridgeStartX + 9;

            //Make road go straight across the bridge
            for (int bridgeX = bridgeStartX; bridgeX <= bridgeEndX && bridgeX < width; bridgeX++)
            {
                roadPositionsY[bridgeX] = bridgeY;
            }

            //Remake the road after the bridge

            GenerateRoad(bridgeEndX + 1, width, height, roadPositionsY);


            //2. Draw the map
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    //    2.1 Draw the frame
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    if ((x == 0 && y == 0) || (x == width - 1 && y == 0) || (x == 0 && y == height - 1) || (x == width - 1 && y == height - 1))
                    {
                        Console.Write("+");
                        continue;
                    }

                    if ((x == 0) || (x == width - 1))
                    {
                        Console.Write("|");
                        continue;
                    }

                    if ((y == 0) || (y == height - 1))
                    {
                        Console.Write("-");
                        continue;
                    }

                    //Draw title
                    if (x == width / 2 - title.Length / 2 && y == 1)
                    {
                        Console.Write(title);
                        x += title.Length - 1;
                        continue;
                    }

                    //    2.2 Draw the road and side path
                    Console.ForegroundColor = ConsoleColor.White;
                    if (y == roadPositionsY[x])
                    {
                        Console.Write("#");
                        continue;
                    }

                    if (y >= bridgeY && x == riverPositionsX[y] - 5)
                    {
                        Console.Write("#");
                        continue;
                    }

                    //    2.3 Draw the bridge
                    if (y == bridgeY - 1 && x >= bridgeStartX + 1 && x <= bridgeEndX - 1)
                    {
                        Console.Write("=");
                        continue;
                    }

                    if (y == bridgeY + 1 && x >= bridgeStartX + 1 && x <= bridgeEndX - 1)
                    {
                        Console.Write("=");
                        continue;
                    }

                    //    2.4 Draw the river
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if ((x == riverPositionsX[y]) || (x == riverPositionsX[y] + 1) || (x == riverPositionsX[y] + 2))
                    {
                        DrawCurve(riverDirections, y);
                        continue;
                    }

                    //Drawing Wall
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if ((x == wallPositionsX[y]) || (x == wallPositionsX[y] + 1))
                    {
                        DrawCurve(wallDirections, y);
                        continue;
                    }

                    //Drawing towers


                    //    2.5 Draw the forest
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (x < width / 4)
                    {
                        int treeRoll = random.Next(100);
                        double treeChance = (Math.Sin((x / (width / 4.0) - 2) * Math.PI / 2) + 1) * 80;
                        if (treeChance > treeRoll)
                        {
                            int randomTreeSymbolIndex = random.Next(treeSymbols.Length);
                            Console.Write(treeSymbols[randomTreeSymbolIndex]);
                            continue;
                        }
                    }

                    //Nothing
                    Console.Write(" ");
                }

                Console.WriteLine();
            }

        }
        static void Main(string[] args)
        {
            DrawMap(60, 20);
        }
    }
}
