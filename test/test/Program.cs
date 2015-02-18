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
        static void Main(string[] args)
        {
            Random generator = new Random();
            List<AerialObstacle> clouds = new List<AerialObstacle>();
            Path trollPath = new Path();
            SetFieldSize();

            while (true)
            {
                int chance = generator.Next(1, 101);

                if (chance < 50)
                {
                    int secondChance = generator.Next(0,4);
                    clouds.Add(new AerialObstacle(Console.WindowWidth - 1, secondChance));
                }

                foreach (var cloud in clouds)
                {
                    cloud.MoveCloud();
                }

                Console.Clear();

                trollPath.DrawPath();
                foreach (var cloud in clouds)
                {
                    cloud.DrawCloud();
                }

                Thread.Sleep(200);
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
