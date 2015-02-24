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
            List<AerialObstacle> clouds = new List<AerialObstacle>();
            List<LandObstacle> traps = new List<LandObstacle>();
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
                    clouds.Add(new AerialObstacle(Console.WindowWidth - 1, secondChance));
                }

                if (chance < 30 && distanceBetweenObstacles > 10)
                {
                    traps.Add(new LandObstacle(Console.WindowWidth - 1, Console.WindowHeight - 3));
                    distanceBetweenObstacles = 0;
                }

                foreach (var cloud in clouds)
                {
                    cloud.MoveObstacle(gameSpeed);
                }

                foreach (var trap in traps)
                {
                    trap.MoveObstacle(gameSpeed);
                }

                Console.Clear();

                trollPath.DrawPath();
                for (int i = 0; i < clouds.Count; i++)
                {
                    clouds[i].DrawObstacle();

                    if (clouds[i].X == 0)
                    {
                        clouds.RemoveAt(i);
                    }
                }

                for (int i = 0; i < traps.Count; i++)
                {
                    traps[i].DrawObstacle();

                    if (traps[i].X == 0)
                    {
                        traps.RemoveAt(i);
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
