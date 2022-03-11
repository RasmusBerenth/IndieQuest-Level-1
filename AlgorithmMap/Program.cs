using System;
using System.Collections.Generic;

namespace AlgorithmMap
{
    internal class Program
    {
        static void StraightenRoad(List<int> roadPositionsY, int startX, int endX)
        {
            for (int x = startX + 1; x <= endX && x < roadPositionsY.Count; x++)
            {
                roadPositionsY[x] = roadPositionsY[startX];
            }
        }
        static int FindCrossing(List<int> roadPositionsY, List<int> crossingPositionsX)
        {
            //Find intersection
            int roadPositionX = 0;
            int crossingPositionX;
            do
            {
                roadPositionX++;
                int roadPositionY = roadPositionsY[roadPositionX];
                crossingPositionX = crossingPositionsX[roadPositionY];
            }
            while (roadPositionX < crossingPositionX);
            return roadPositionX;
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
            //Prepear the data.
            var random = new Random();
            var treeSymbols = new string[] { "T", "@", "I" };
            string title = "Adventure Map";

            //Create the river.
            int riverStartPositionX = width * 3 / 4;
            List<int> riverPositionsX;
            List<int> riverDirections;

            GenerateCurve(riverStartPositionX, height, 3, out riverPositionsX, out riverDirections);

            //Create the wall.
            int wallStartPositionX = width / 4;
            List<int> wallPositionsX;
            List<int> wallDirections;

            GenerateCurve(wallStartPositionX, height, 6, out wallPositionsX, out wallDirections);

            //Create the road.
            int roadStartPositionY = height / 2;
            var roadPositionsY = new List<int>() { roadStartPositionY };

            GenerateRoad(1, width, height, roadPositionsY);

            //Find road wall intersect.
            int wallIntersectionX = FindCrossing(roadPositionsY, wallPositionsX);

            //Calculate gate perameters.
            int gateStartX = wallIntersectionX - 3;
            int gateY = roadPositionsY[gateStartX];
            int gateEndX = gateStartX + 6;

            //Make the road go straight through the gate.
            StraightenRoad(roadPositionsY, gateStartX, gateEndX);

            //Remake road after the gate.
            GenerateRoad(gateEndX + 1, width, height, roadPositionsY);

            //Find road river intersection.
            int riverIntersectionX = FindCrossing(roadPositionsY, riverPositionsX);

            //Calculate bridge parameters.
            int bridgeStartX = riverIntersectionX - 3;
            int bridgeY = roadPositionsY[bridgeStartX];
            int bridgeEndX = bridgeStartX + 9;

            //Make road go straight across the bridge.
            StraightenRoad(roadPositionsY, bridgeStartX, bridgeEndX);

            //Remake the road after the bridge.
            GenerateRoad(bridgeEndX + 1, width, height, roadPositionsY);


            //Draw the map.
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    //Draw the frame.
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

                    //Draw title.
                    if (x == width / 2 - title.Length / 2 && y == 1)
                    {
                        Console.Write(title);
                        x += title.Length - 1;
                        continue;
                    }

                    //Draw the road and side path.
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

                    //Draw the bridge.
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

                    //Draw the river.
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if ((x == riverPositionsX[y]) || (x == riverPositionsX[y] + 1) || (x == riverPositionsX[y] + 2))
                    {
                        DrawCurve(riverDirections, y);
                        continue;
                    }

                    //Draw the towers.
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (y == gateY + 1 && x == wallIntersectionX && x < gateEndX - 2)
                    {
                        Console.Write("[");
                        continue;
                    }
                    
                    if (y == gateY + 1 && x == wallIntersectionX + 1 && x < gateEndX - 1)
                    {
                        Console.Write("]");
                        continue;
                    }

                    if (y == gateY - 1 && x == wallIntersectionX && x < gateEndX - 2)
                    {
                        Console.Write("[");
                        continue;
                    }

                    if (y == gateY - 1 && x == wallIntersectionX + 1 && x < gateEndX - 1)
                    {
                        Console.Write("]");
                        continue;
                    }

                    //Draw the wall.
                    if ((x == wallPositionsX[y]) || (x == wallPositionsX[y] + 1))
                    {
                        DrawCurve(wallDirections, y);
                        continue;
                    }

                    //    2.5 Draw the forest.
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

                    //Draw nothing.
                    Console.Write(" ");
                }

                Console.WriteLine();
            }

        }
        static void Main(string[] args)
        {
            //Input the size of the map width and height respectivly.
            DrawMap(60, 20);
        }
    }
}
