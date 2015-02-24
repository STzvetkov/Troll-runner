using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main()
        {
            Random generator = new Random();
            List<AerialObstacle> cloudsContainer = new List<AerialObstacle>();
            List<LandObstacle> trapsContainer = new List<LandObstacle>();
            List<Pickup> pickupContainer = new List<Pickup>();
            Path trollPath = new Path();
            SetFieldSize();
            int distanceBetweenObstacles = 0;
            int gameSpeed = 1;


            while (true)
            {
                int chance = generator.Next(1, 101);

                if (chance < 30)
                {
                    int secondChance = generator.Next(0, 4);
                    cloudsContainer.Add(new AerialObstacle(Console.WindowWidth - 1, secondChance));
                }

                if (chance < 30 && distanceBetweenObstacles > 10)
                {
                    trapsContainer.Add(new LandObstacle(Console.WindowWidth - 1, Console.WindowHeight - 3));
                    distanceBetweenObstacles = 0;
                }

                foreach (var cloud in cloudsContainer)
                {
                    cloud.MoveObstacle(gameSpeed);
                }

                foreach (var trap in trapsContainer)
                {
                    trap.MoveObstacle(gameSpeed);
                }

                Console.Clear();

                trollPath.DrawPath();
                for (int i = 0; i < cloudsContainer.Count; i++)
                {
                    cloudsContainer[i].DrawObstacle();

                    if (cloudsContainer[i].X == 0)
                    {
                        cloudsContainer.RemoveAt(i);
                    }
                }

                for (int i = 0; i < trapsContainer.Count; i++)
                {
                    trapsContainer[i].DrawObstacle();

                    if (trapsContainer[i].X == 0)
                    {
                        trapsContainer.RemoveAt(i);
                    }
                }

                distanceBetweenObstacles++;
                Thread.Sleep(50);
            }
        }

        static void SetFieldSize()
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 120;
            Console.BufferWidth = 120;
            Console.BufferHeight = 20;
        }
    }
}
