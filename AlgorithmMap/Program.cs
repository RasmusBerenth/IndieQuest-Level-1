using System;
using System.Collections.Generic;

namespace AlgorithmMap
{
    internal class Program
    {
        static void DrawMap(int width, int height)
        {
            //1. Prepear the data
            var random = new Random();
            int direction;

            //    1.1 Create code for the river
            int riverStartPositionX = width * 3 / 4;
            var riverPositionsX = new List<int>() { riverStartPositionX };

            for (int y = 1; y < height; y++)
            {
                direction = random.Next(3);
                int newPositionX;
                int previousPositionX = riverPositionsX[y - 1];

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

                riverPositionsX.Add(newPositionX);
            }

            //    1.2 Create code for the road
            int roadStartPositionY = height / 2;
            var roadPositionsY = new List<int>() {roadStartPositionY};

            for (int x = 1; x < width; x++)
            {
                direction = random.Next(3);
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

                roadPositionsY.Add(newPositionY);
            }

            //    1.3 Create code for the bridge


            //2. Draw the map

            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    //    2.1 Draw the frame and title
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


                    //    2.2 Draw the road and side path
                    if (y == roadPositionsY[x])
                    {
                        Console.Write("#");
                        continue;
                    }

                    //    2.3 Draw the bridge


                    //    2.4 Draw the river
                    if ((x == riverPositionsX[y]) || (x == riverPositionsX[y] + 1) || x == riverPositionsX[y] + 2)
                    {
                        Console.Write("R");
                        continue;
                    }

                    //    2.5 Draw the forest
                    if (x < width / 4)
                    {
                        int treeRoll = random.Next(100);
                        double treeChance = (Math.Sin((x / (width / 4.0) - 2) * Math.PI / 2) + 1) * 80;
                        if (treeChance > treeRoll)
                        {
                            Console.Write("T");
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
            DrawMap(40, 10);
        }
    }
}
