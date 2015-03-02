using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace test
{
    class Game  
    {
        public const int MinSpeed = 1;
        public const int InitialLifes = 3;
        public const int SleepTime = 50;

        public static int gameSpeed = MinSpeed;
        public static int currentLifes = InitialLifes;
        public static bool ableToFire = false;

        public static Random generator = new Random();
       
        static void Main()
        {
            Console.Title = ("Troll runner");

            GraphicsManagement.InitializeGraphics();  // Load graphics from the corresponding files
            
            List<AerialObstacle> cloudsContainer = new List<AerialObstacle>();
            List<LandObstacle> trapsContainer = new List<LandObstacle>();
            List<Pickup> pickupContainer = new List<Pickup>();
            Path trollPath = new Path();
            SetFieldSize();
            int distanceBetweenObstacles = 0;
                       
            int startResult = 0;
            string fileName = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\HighScores.txt");        
            string[] scores = System.IO.File.ReadAllLines(fileName);
            string highscore = scores[0];
            bool playing = true;

            while (playing)
            {
                int chance = generator.Next(1, 101);

                if (chance >= 30 && chance <= 35)
                {
                    switch (chance)
                    {
                        case 30: case 31:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Fire));
                            break;
                        case 32:
                        case 33:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Life));
                            break;
                        case 34:
                        case 35:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Slow));
                            break;
                    }
                }

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

                foreach (var pickup in pickupContainer)
                {
                    pickup.MoveObstacle(gameSpeed);
                }
               
                Console.Clear();
               
                Score(startResult, highscore);
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

                // handles pickups
                for (int i = 0; i < pickupContainer.Count; i++)
                {
                    if (!pickupContainer[i].IsActive)
                    { 
                        if (pickupContainer[i].X > 0)
                        {
                            pickupContainer[i].DrawObstacle();
                        }
                        else if (pickupContainer[i].X <= 0)
                        {
                            pickupContainer.RemoveAt(i);
                        }
                        /*else if (DetectCollision(pickupContainer[i], runner))
                        {
                        pickupContainer[i].Activate();
                        }*/
                    }
                    else
                    {
                        pickupContainer[i].Expire();
                    }
                }

                PauseResume(playing);
                distanceBetweenObstacles++;
                startResult += 10;
                Thread.Sleep(SleepTime);             
            }
<<<<<<< HEAD

            //Console.CursorVisible = false;
=======
>>>>>>> 39d1b2777b8a1ebfea46ea8287be5441c57d4b86
        }

        static void Score(int result, string high)
        {
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write("Current Score: {0}", result);

            Console.SetCursorPosition((62 - high.Length - 1), Console.BufferHeight - 1);
            Console.Write("Press ESC to pause");

            Console.SetCursorPosition((108 - high.Length - 1), Console.BufferHeight - 1);
            Console.Write("High Score: {0}", high);
        }

        static void PauseResume(bool play)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo press = Console.ReadKey(true);
                if (press.Key == ConsoleKey.Escape)
                {
                    play = false;
                    Console.SetCursorPosition(51, 7);
                    Console.Write("Game is now paused");
                    Console.SetCursorPosition(50, 8);
                    Console.Write("Press ESC to continue");
                    ConsoleKeyInfo pressAgain = Console.ReadKey(true);
                    while (pressAgain.Key != ConsoleKey.Escape)
                    {
                        pressAgain = Console.ReadKey(true);
                    }
                    if (pressAgain.Key == ConsoleKey.M)
                    {
                        play = true;
                    }
                }
            }
        }

        static void SetFieldSize()
        {
            Console.WindowHeight = 22;
            Console.WindowWidth = 120;
            Console.BufferWidth = 120;
            Console.BufferHeight = 22;
        }
    }
}
