using System;

namespace AlgorithmMap
{
    internal class Program
    {
        static void DrawMap(int width, int height)
        {
            //1. Prepear the data
            //    1.1 Create code for the river

            //    1.2 Create code for the road

            //    1.3 Create code for the bridge
            
            
            //2. Draw the map
            var random = new Random();

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
                    //    2.3 Draw the bridge
                    //    2.4 Draw the river
                    //    2.5 Draw the forest
                    if (x < width/4)
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

            
            
            DrawMap(100, 20);
        }
    }
}
