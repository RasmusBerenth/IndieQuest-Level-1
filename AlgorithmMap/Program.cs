using System;
using System.Collections.Generic;

namespace AlgorithmMap
{
    internal class Program
    {
        static void StraightenPositions(List<int> positions, int start, int end)
        {
            //Making the road go straight across the bridge and through the gate.
            for (int i = start + 1; i <= end && i < positions.Count; i++)
            {
                positions[i] = positions[start];
            }
        }
        static int FindCrossing(List<int> roadPositionsY, List<int> crossingPositionsX)
        {
            //Find intersection when the wall or river crosses the road.
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
            //Creating the roads direction.
            var random = new Random();
            for (int x = roadStartX; x < width; x++)
            {
                int direction = random.Next(7);
                int newPositionY;
                int previousPositionY = roadPositionsY[x - 1];
                
                //Road goes up and prevents it from going to low.
                if (direction == 0 && previousPositionY > 2)
                {
                    newPositionY = previousPositionY - 1;
                }
                //Road goes down and prevents it from going to high.
                else if (direction == 1 && previousPositionY < height - 3)
                {
                    newPositionY = previousPositionY + 1;
                }
                //Road goes straight.
                else
                {
                    newPositionY = previousPositionY;
                }

                //After a direction has been choosen add or replace new piece to the list of the road that has already been completed.
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
        static void GenerateCurve(int startY, int height, int curveChance, List<int> positionsX, List<int> directions)
        {
            //Choosing a random direction for the wall and river.
            var random = new Random();

            for (int y = startY; y < height; y++)
            {
                int direction = random.Next(curveChance);
                int newPositionX;
                int previousPositionX = positionsX[y - 1];

                //Turns to the left.
                if (direction == 0)
                {
                    newPositionX = previousPositionX - 1;
                }
                //Turns to the right.
                else if (direction == 1)
                {
                    newPositionX = previousPositionX + 1;
                }
                //Moves in a straight line from it's previous position.
                else
                {
                    newPositionX = previousPositionX;
                }

                //After a direction has been choosen add or replace new piece to the list of the river or wall that has already been completed.
                if (positionsX.Count > y)
                {
                    positionsX[y] = newPositionX;
                    directions[y - 1] = direction;
                }
                else
                {
                    positionsX.Add(newPositionX);
                    directions.Add(direction);
                }
            }
        }
        static void DrawCurve(List<int> directions, int y)
        {
            //Drawing the curves generated in GenerateCurve for the wall and the river.
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
            //Prepear data for the drawing stage.
            var random = new Random();
            var treeSymbols = new string[] { "T", "@", "l" };
            string title = "Adventure Map";

            //Create the river.
            int riverStartPositionX = width * 3 / 4;
            var riverPositionsX = new List<int>() {riverStartPositionX};
            var riverDirections = new List<int>();

            GenerateCurve(1, height, 3, riverPositionsX, riverDirections);

            //Create the wall.
            int wallStartPositionX = width / 4;
            var wallPositionsX = new List<int>() {wallStartPositionX};
            var wallDirections = new List<int>();

            GenerateCurve(1, height, 6, wallPositionsX, wallDirections);

            //Create the road .
            int roadStartPositionY = height / 2;
            var roadPositionsY = new List<int>() { roadStartPositionY };

            GenerateRoad(1, width, height, roadPositionsY);

            //Find road wall intersect.
            int wallIntersectionX = FindCrossing(roadPositionsY, wallPositionsX);
            
            //Calculate gate perameters.
            int gateRoadStartX = wallIntersectionX - 3;
            int gateY = roadPositionsY[gateRoadStartX];
            int gateRoadEndX = gateRoadStartX + 6;
            int gateWallStartY = gateY - 2;
            int gateWallEndY = gateY + 2;
            
            //Straighten the wall.
            StraightenPositions(wallPositionsX, gateWallStartY, gateWallEndY);
            
            //Remake wall after gate
            GenerateCurve(gateWallEndY + 1, height, 6, wallPositionsX, wallDirections);
            
            //Straighten road.
            StraightenPositions(roadPositionsY, gateRoadStartX, gateRoadEndX);

            //Remake road after the gate.
            GenerateRoad(gateRoadEndX + 1, width, height, roadPositionsY);

            //Find road river intersection.
            int riverIntersectionX = FindCrossing(roadPositionsY, riverPositionsX);

            //Calculate bridge parameters.
            int bridgeStartX = riverIntersectionX - 3;
            int bridgeY = roadPositionsY[bridgeStartX];
            int bridgeEndX = bridgeStartX + 9;

            StraightenPositions(roadPositionsY, bridgeStartX, bridgeEndX);

            //Remake the road after the bridge.
            GenerateRoad(bridgeEndX + 1, width, height, roadPositionsY);

            //Drawing the map.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //Drawing the frame.
                    Console.ForegroundColor = ConsoleColor.White;
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

                    //Drawing title.
                    if (x == width / 2 - title.Length / 2 && y == 1)
                    {
                        Console.Write(title);
                        x += title.Length - 1;
                        continue;
                    }

                    //Drawing the main road.
                    Console.ForegroundColor = ConsoleColor.White;
                    if (y == roadPositionsY[x])
                    {
                        Console.Write("#");
                        continue;
                    }
                    //Drawing the side path by the river.
                    if (y >= bridgeY && x == riverPositionsX[y] - 5)
                    {
                        Console.Write("#");
                        continue;
                    }

                    //Drawing the bridge.
                    Console.ForegroundColor = ConsoleColor.Gray;
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

                    //Drawing the river.
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if ((x == riverPositionsX[y]) || (x == riverPositionsX[y] + 1) || (x == riverPositionsX[y] + 2))
                    {
                        DrawCurve(riverDirections, y);
                        continue;
                    }

                    //Drawing the towers.
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (y == gateY + 1 && x == wallPositionsX[y])
                    {
                        Console.Write("[]");
                        x++;
                        continue;
                    }

                    if (y == gateY - 1 && x == wallPositionsX[y])
                    {
                        Console.Write("[]");
                        x++;
                        continue;
                    }

                    //Drawing the wall.
                    if ((x == wallPositionsX[y]) || (x == wallPositionsX[y] + 1))
                    {
                        DrawCurve(wallDirections, y);
                        continue;
                    }

                    //2.5 Drawing the forest.
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (x < width / 4)
                    {
                        //Generating the liklyhood of there being a tree at any location.
                        int treeRoll = random.Next(100);
                        double treeChance = (Math.Sin((x / (width / 4.0) - 2) * Math.PI / 2) + 1) * 80;
                        if (treeChance > treeRoll)
                        {
                            int randomTreeSymbolIndex = random.Next(treeSymbols.Length);
                            Console.Write(treeSymbols[randomTreeSymbolIndex]);
                            continue;
                        }
                    }

                    //Drawing nothing.
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
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
