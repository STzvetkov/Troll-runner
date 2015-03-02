﻿using System;
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

            Start();

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

            Console.CursorVisible = false;
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

        static void Start()
        {
            Console.WindowHeight = 22;
            Console.WindowWidth = 120;
            Console.BufferWidth = 120;
            Console.BufferHeight = 22;
            char[,] trollRUnnerLetters;
            int rows = 8;
            int cols = 92;
            trollRUnnerLetters = new char[rows, cols];

            trollRUnnerLetters[0, 0] = '_';
            trollRUnnerLetters[0, 1] = '_';
            trollRUnnerLetters[0, 2] = '_';
            trollRUnnerLetters[0, 3] = '_';
            trollRUnnerLetters[0, 4] = '_';
            trollRUnnerLetters[0, 5] = '_';
            trollRUnnerLetters[0, 6] = '_';
            trollRUnnerLetters[0, 7] = '_';
            trollRUnnerLetters[0, 8] = '_';
            trollRUnnerLetters[0, 9] = '_';
            trollRUnnerLetters[0, 10] = '_';
            trollRUnnerLetters[0, 11] = '_';
            trollRUnnerLetters[0, 12] = '_';
            trollRUnnerLetters[0, 13] = '_';
            trollRUnnerLetters[0, 14] = '_';
            trollRUnnerLetters[0, 15] = '_';
            trollRUnnerLetters[0, 16] = ' ';
            trollRUnnerLetters[0, 17] = '_';
            trollRUnnerLetters[0, 18] = '_';
            trollRUnnerLetters[0, 19] = '_';
            trollRUnnerLetters[0, 20] = '_';
            trollRUnnerLetters[0, 21] = '_';
            trollRUnnerLetters[0, 22] = '_';
            trollRUnnerLetters[0, 23] = '_';
            trollRUnnerLetters[0, 24] = ' ';
            trollRUnnerLetters[0, 25] = '_';
            trollRUnnerLetters[0, 26] = ' ';
            trollRUnnerLetters[0, 27] = ' ';
            trollRUnnerLetters[0, 28] = ' ';
            trollRUnnerLetters[0, 29] = ' ';
            trollRUnnerLetters[0, 30] = ' ';
            trollRUnnerLetters[0, 31] = ' ';
            trollRUnnerLetters[0, 32] = ' ';
            trollRUnnerLetters[0, 33] = '_';
            trollRUnnerLetters[0, 34] = ' ';
            trollRUnnerLetters[0, 35] = ' ';
            trollRUnnerLetters[0, 36] = ' ';
            trollRUnnerLetters[0, 37] = ' ';
            trollRUnnerLetters[0, 38] = ' ';
            trollRUnnerLetters[0, 39] = ' ';
            trollRUnnerLetters[0, 40] = ' ';
            trollRUnnerLetters[0, 41] = ' ';
            trollRUnnerLetters[0, 42] = ' ';
            trollRUnnerLetters[0, 43] = ' ';
            trollRUnnerLetters[0, 44] = '_';
            trollRUnnerLetters[0, 45] = '_';
            trollRUnnerLetters[0, 46] = '_';
            trollRUnnerLetters[0, 47] = '_';
            trollRUnnerLetters[0, 48] = '_';
            trollRUnnerLetters[0, 49] = '_';
            trollRUnnerLetters[0, 50] = '_';
            trollRUnnerLetters[0, 51] = ' ';
            trollRUnnerLetters[0, 52] = ' ';
            trollRUnnerLetters[0, 53] = ' ';
            trollRUnnerLetters[0, 54] = ' ';
            trollRUnnerLetters[0, 55] = ' ';
            trollRUnnerLetters[0, 56] = ' ';
            trollRUnnerLetters[0, 57] = ' ';
            trollRUnnerLetters[0, 58] = ' ';
            trollRUnnerLetters[0, 59] = ' ';
            trollRUnnerLetters[0, 60] = '_';
            trollRUnnerLetters[0, 61] = ' ';
            trollRUnnerLetters[0, 62] = ' ';
            trollRUnnerLetters[0, 63] = ' ';
            trollRUnnerLetters[0, 64] = ' ';
            trollRUnnerLetters[0, 65] = ' ';
            trollRUnnerLetters[0, 66] = ' ';
            trollRUnnerLetters[0, 67] = ' ';
            trollRUnnerLetters[0, 68] = '_';
            trollRUnnerLetters[0, 69] = ' ';
            trollRUnnerLetters[0, 70] = ' ';
            trollRUnnerLetters[0, 71] = ' ';
            trollRUnnerLetters[0, 72] = ' ';
            trollRUnnerLetters[0, 73] = ' ';
            trollRUnnerLetters[0, 74] = ' ';
            trollRUnnerLetters[0, 75] = ' ';
            trollRUnnerLetters[0, 76] = '_';
            trollRUnnerLetters[0, 77] = '_';
            trollRUnnerLetters[0, 78] = '_';
            trollRUnnerLetters[0, 79] = '_';
            trollRUnnerLetters[0, 80] = '_';
            trollRUnnerLetters[0, 81] = '_';
            trollRUnnerLetters[0, 82] = '_';
            trollRUnnerLetters[0, 83] = ' ';
            trollRUnnerLetters[0, 84] = '_';
            trollRUnnerLetters[0, 85] = '_';
            trollRUnnerLetters[0, 86] = '_';
            trollRUnnerLetters[0, 87] = '_';
            trollRUnnerLetters[0, 88] = '_';
            trollRUnnerLetters[0, 89] = '_';
            trollRUnnerLetters[0, 90] = '_';
            trollRUnnerLetters[0, 91] = ' ';


            trollRUnnerLetters[1, 0] = '\\';
            trollRUnnerLetters[1, 1] = '_';
            trollRUnnerLetters[1, 2] = '_';
            trollRUnnerLetters[1, 3] = ' ';
            trollRUnnerLetters[1, 4] = ' ';
            trollRUnnerLetters[1, 5] = ' ';
            trollRUnnerLetters[1, 6] = '_';
            trollRUnnerLetters[1, 7] = '_';
            trollRUnnerLetters[1, 8] = '(';
            trollRUnnerLetters[1, 9] = ' ';
            trollRUnnerLetters[1, 10] = ' ';
            trollRUnnerLetters[1, 11] = '_';
            trollRUnnerLetters[1, 12] = '_';
            trollRUnnerLetters[1, 13] = '_';
            trollRUnnerLetters[1, 14] = '_';
            trollRUnnerLetters[1, 15] = ' ';
            trollRUnnerLetters[1, 16] = '(';
            trollRUnnerLetters[1, 17] = ' ';
            trollRUnnerLetters[1, 18] = ' ';
            trollRUnnerLetters[1, 19] = '_';
            trollRUnnerLetters[1, 20] = '_';
            trollRUnnerLetters[1, 21] = '_';
            trollRUnnerLetters[1, 22] = ' ';
            trollRUnnerLetters[1, 23] = ' ';
            trollRUnnerLetters[1, 24] = '(';
            trollRUnnerLetters[1, 25] = ' ';
            trollRUnnerLetters[1, 26] = '\\';
            trollRUnnerLetters[1, 27] = ' ';
            trollRUnnerLetters[1, 28] = ' ';
            trollRUnnerLetters[1, 29] = ' ';
            trollRUnnerLetters[1, 30] = ' ';
            trollRUnnerLetters[1, 31] = ' ';
            trollRUnnerLetters[1, 32] = '(';
            trollRUnnerLetters[1, 33] = ' ';
            trollRUnnerLetters[1, 34] = '\\';
            trollRUnnerLetters[1, 35] = ' ';
            trollRUnnerLetters[1, 36] = ' ';
            trollRUnnerLetters[1, 37] = ' ';
            trollRUnnerLetters[1, 38] = ' ';
            trollRUnnerLetters[1, 39] = ' ';
            trollRUnnerLetters[1, 40] = ' ';
            trollRUnnerLetters[1, 41] = ' ';
            trollRUnnerLetters[1, 42] = ' ';
            trollRUnnerLetters[1, 43] = '(';
            trollRUnnerLetters[1, 44] = ' ';
            trollRUnnerLetters[1, 45] = ' ';
            trollRUnnerLetters[1, 46] = '_';
            trollRUnnerLetters[1, 47] = '_';
            trollRUnnerLetters[1, 48] = '_';
            trollRUnnerLetters[1, 49] = '_';
            trollRUnnerLetters[1, 50] = ' ';
            trollRUnnerLetters[1, 51] = '|';
            trollRUnnerLetters[1, 52] = '\\';
            trollRUnnerLetters[1, 53] = ' ';
            trollRUnnerLetters[1, 54] = ' ';
            trollRUnnerLetters[1, 55] = ' ';
            trollRUnnerLetters[1, 56] = ' ';
            trollRUnnerLetters[1, 57] = ' ';
            trollRUnnerLetters[1, 58] = '/';
            trollRUnnerLetters[1, 59] = '(';
            trollRUnnerLetters[1, 60] = ' ';
            trollRUnnerLetters[1, 61] = '(';
            trollRUnnerLetters[1, 62] = ' ';
            trollRUnnerLetters[1, 63] = ' ';
            trollRUnnerLetters[1, 64] = ' ';
            trollRUnnerLetters[1, 65] = ' ';
            trollRUnnerLetters[1, 66] = '/';
            trollRUnnerLetters[1, 67] = '(';
            trollRUnnerLetters[1, 68] = ' ';
            trollRUnnerLetters[1, 69] = '(';
            trollRUnnerLetters[1, 70] = ' ';
            trollRUnnerLetters[1, 71] = ' ';
            trollRUnnerLetters[1, 72] = ' ';
            trollRUnnerLetters[1, 73] = ' ';
            trollRUnnerLetters[1, 74] = '/';
            trollRUnnerLetters[1, 75] = '(';
            trollRUnnerLetters[1, 76] = ' ';
            trollRUnnerLetters[1, 77] = ' ';
            trollRUnnerLetters[1, 78] = '_';
            trollRUnnerLetters[1, 79] = '_';
            trollRUnnerLetters[1, 80] = '_';
            trollRUnnerLetters[1, 81] = '_';
            trollRUnnerLetters[1, 82] = ' ';
            trollRUnnerLetters[1, 83] = '(';
            trollRUnnerLetters[1, 84] = ' ';
            trollRUnnerLetters[1, 85] = ' ';
            trollRUnnerLetters[1, 86] = '_';
            trollRUnnerLetters[1, 87] = '_';
            trollRUnnerLetters[1, 88] = '_';
            trollRUnnerLetters[1, 89] = ' ';
            trollRUnnerLetters[1, 90] = ' ';
            trollRUnnerLetters[1, 91] = ')';


            trollRUnnerLetters[2, 0] = ' ';
            trollRUnnerLetters[2, 1] = ' ';
            trollRUnnerLetters[2, 2] = ' ';
            trollRUnnerLetters[2, 3] = ')';
            trollRUnnerLetters[2, 4] = ' ';
            trollRUnnerLetters[2, 5] = '(';
            trollRUnnerLetters[2, 6] = ' ';
            trollRUnnerLetters[2, 7] = ' ';
            trollRUnnerLetters[2, 8] = '|';
            trollRUnnerLetters[2, 9] = ' ';
            trollRUnnerLetters[2, 10] = '(';
            trollRUnnerLetters[2, 11] = ' ';
            trollRUnnerLetters[2, 12] = ' ';
            trollRUnnerLetters[2, 13] = ' ';
            trollRUnnerLetters[2, 14] = ')';
            trollRUnnerLetters[2, 15] = ' ';
            trollRUnnerLetters[2, 16] = '|';
            trollRUnnerLetters[2, 17] = ' ';
            trollRUnnerLetters[2, 18] = '(';
            trollRUnnerLetters[2, 19] = ' ';
            trollRUnnerLetters[2, 20] = ' ';
            trollRUnnerLetters[2, 21] = ' ';
            trollRUnnerLetters[2, 22] = ')';
            trollRUnnerLetters[2, 23] = ' ';
            trollRUnnerLetters[2, 24] = '|';
            trollRUnnerLetters[2, 25] = ' ';
            trollRUnnerLetters[2, 26] = '(';
            trollRUnnerLetters[2, 27] = ' ';
            trollRUnnerLetters[2, 28] = ' ';
            trollRUnnerLetters[2, 29] = ' ';
            trollRUnnerLetters[2, 30] = ' ';
            trollRUnnerLetters[2, 31] = ' ';
            trollRUnnerLetters[2, 32] = '|';
            trollRUnnerLetters[2, 33] = ' ';
            trollRUnnerLetters[2, 34] = '(';
            trollRUnnerLetters[2, 35] = ' ';
            trollRUnnerLetters[2, 36] = ' ';
            trollRUnnerLetters[2, 37] = ' ';
            trollRUnnerLetters[2, 38] = ' ';
            trollRUnnerLetters[2, 39] = ' ';
            trollRUnnerLetters[2, 40] = ' ';
            trollRUnnerLetters[2, 41] = ' ';
            trollRUnnerLetters[2, 42] = ' ';
            trollRUnnerLetters[2, 43] = '|';
            trollRUnnerLetters[2, 44] = ' ';
            trollRUnnerLetters[2, 45] = '(';
            trollRUnnerLetters[2, 46] = ' ';
            trollRUnnerLetters[2, 47] = ' ';
            trollRUnnerLetters[2, 48] = ' ';
            trollRUnnerLetters[2, 49] = ' ';
            trollRUnnerLetters[2, 50] = ')';
            trollRUnnerLetters[2, 51] = '|';
            trollRUnnerLetters[2, 52] = ' ';
            trollRUnnerLetters[2, 53] = ')';
            trollRUnnerLetters[2, 54] = ' ';
            trollRUnnerLetters[2, 55] = ' ';
            trollRUnnerLetters[2, 56] = ' ';
            trollRUnnerLetters[2, 57] = '(';
            trollRUnnerLetters[2, 58] = ' ';
            trollRUnnerLetters[2, 59] = '|';
            trollRUnnerLetters[2, 60] = ' ';
            trollRUnnerLetters[2, 61] = ' ';
            trollRUnnerLetters[2, 62] = '\\';
            trollRUnnerLetters[2, 63] = ' ';
            trollRUnnerLetters[2, 64] = ' ';
            trollRUnnerLetters[2, 65] = '(';
            trollRUnnerLetters[2, 66] = ' ';
            trollRUnnerLetters[2, 67] = '|';
            trollRUnnerLetters[2, 68] = ' ';
            trollRUnnerLetters[2, 69] = ' ';
            trollRUnnerLetters[2, 70] = '\\';
            trollRUnnerLetters[2, 71] = ' ';
            trollRUnnerLetters[2, 72] = ' ';
            trollRUnnerLetters[2, 73] = '(';
            trollRUnnerLetters[2, 74] = ' ';
            trollRUnnerLetters[2, 75] = '|';
            trollRUnnerLetters[2, 76] = ' ';
            trollRUnnerLetters[2, 77] = '(';
            trollRUnnerLetters[2, 78] = ' ';
            trollRUnnerLetters[2, 79] = ' ';
            trollRUnnerLetters[2, 80] = ' ';
            trollRUnnerLetters[2, 81] = ' ';
            trollRUnnerLetters[2, 82] = '\\';
            trollRUnnerLetters[2, 83] = '|';
            trollRUnnerLetters[2, 84] = ' ';
            trollRUnnerLetters[2, 85] = '(';
            trollRUnnerLetters[2, 86] = ' ';
            trollRUnnerLetters[2, 87] = ' ';
            trollRUnnerLetters[2, 88] = ' ';
            trollRUnnerLetters[2, 89] = ')';
            trollRUnnerLetters[2, 90] = ' ';
            trollRUnnerLetters[2, 91] = '|';


            trollRUnnerLetters[3, 0] = ' ';
            trollRUnnerLetters[3, 1] = ' ';
            trollRUnnerLetters[3, 2] = ' ';
            trollRUnnerLetters[3, 3] = '|';
            trollRUnnerLetters[3, 4] = ' ';
            trollRUnnerLetters[3, 5] = '|';
            trollRUnnerLetters[3, 6] = ' ';
            trollRUnnerLetters[3, 7] = ' ';
            trollRUnnerLetters[3, 8] = '|';
            trollRUnnerLetters[3, 9] = ' ';
            trollRUnnerLetters[3, 10] = '(';
            trollRUnnerLetters[3, 11] = '_';
            trollRUnnerLetters[3, 12] = '_';
            trollRUnnerLetters[3, 13] = '_';
            trollRUnnerLetters[3, 14] = ')';
            trollRUnnerLetters[3, 15] = ' ';
            trollRUnnerLetters[3, 16] = '|';
            trollRUnnerLetters[3, 17] = ' ';
            trollRUnnerLetters[3, 18] = '|';
            trollRUnnerLetters[3, 19] = ' ';
            trollRUnnerLetters[3, 20] = ' ';
            trollRUnnerLetters[3, 21] = ' ';
            trollRUnnerLetters[3, 22] = '|';
            trollRUnnerLetters[3, 23] = ' ';
            trollRUnnerLetters[3, 24] = '|';
            trollRUnnerLetters[3, 25] = ' ';
            trollRUnnerLetters[3, 26] = '|';
            trollRUnnerLetters[3, 27] = ' ';
            trollRUnnerLetters[3, 28] = ' ';
            trollRUnnerLetters[3, 29] = ' ';
            trollRUnnerLetters[3, 30] = ' ';
            trollRUnnerLetters[3, 31] = ' ';
            trollRUnnerLetters[3, 32] = '|';
            trollRUnnerLetters[3, 33] = ' ';
            trollRUnnerLetters[3, 34] = '|';
            trollRUnnerLetters[3, 35] = ' ';
            trollRUnnerLetters[3, 36] = ' ';
            trollRUnnerLetters[3, 37] = ' ';
            trollRUnnerLetters[3, 38] = ' ';
            trollRUnnerLetters[3, 39] = ' ';
            trollRUnnerLetters[3, 40] = ' ';
            trollRUnnerLetters[3, 41] = ' ';
            trollRUnnerLetters[3, 42] = ' ';
            trollRUnnerLetters[3, 43] = '|';
            trollRUnnerLetters[3, 44] = ' ';
            trollRUnnerLetters[3, 45] = '(';
            trollRUnnerLetters[3, 46] = '_';
            trollRUnnerLetters[3, 47] = '_';
            trollRUnnerLetters[3, 48] = '_';
            trollRUnnerLetters[3, 49] = '_';
            trollRUnnerLetters[3, 50] = ')';
            trollRUnnerLetters[3, 51] = '|';
            trollRUnnerLetters[3, 52] = ' ';
            trollRUnnerLetters[3, 53] = '|';
            trollRUnnerLetters[3, 54] = ' ';
            trollRUnnerLetters[3, 55] = ' ';
            trollRUnnerLetters[3, 56] = ' ';
            trollRUnnerLetters[3, 57] = '|';
            trollRUnnerLetters[3, 58] = ' ';
            trollRUnnerLetters[3, 59] = '|';
            trollRUnnerLetters[3, 60] = ' ';
            trollRUnnerLetters[3, 61] = ' ';
            trollRUnnerLetters[3, 62] = ' ';
            trollRUnnerLetters[3, 63] = '\\';
            trollRUnnerLetters[3, 64] = ' ';
            trollRUnnerLetters[3, 65] = '|';
            trollRUnnerLetters[3, 66] = ' ';
            trollRUnnerLetters[3, 67] = '|';
            trollRUnnerLetters[3, 68] = ' ';
            trollRUnnerLetters[3, 69] = ' ';
            trollRUnnerLetters[3, 70] = ' ';
            trollRUnnerLetters[3, 71] = '\\';
            trollRUnnerLetters[3, 72] = ' ';
            trollRUnnerLetters[3, 73] = '|';
            trollRUnnerLetters[3, 74] = ' ';
            trollRUnnerLetters[3, 75] = '|';
            trollRUnnerLetters[3, 76] = ' ';
            trollRUnnerLetters[3, 77] = '(';
            trollRUnnerLetters[3, 78] = '_';
            trollRUnnerLetters[3, 79] = '_';
            trollRUnnerLetters[3, 80] = ' ';
            trollRUnnerLetters[3, 81] = ' ';
            trollRUnnerLetters[3, 82] = ' ';
            trollRUnnerLetters[3, 83] = '|';
            trollRUnnerLetters[3, 84] = ' ';
            trollRUnnerLetters[3, 85] = '(';
            trollRUnnerLetters[3, 86] = '_';
            trollRUnnerLetters[3, 87] = '_';
            trollRUnnerLetters[3, 88] = '_';
            trollRUnnerLetters[3, 89] = ')';
            trollRUnnerLetters[3, 90] = ' ';
            trollRUnnerLetters[3, 91] = '|';


            trollRUnnerLetters[4, 0] = ' ';
            trollRUnnerLetters[4, 1] = ' ';
            trollRUnnerLetters[4, 2] = ' ';
            trollRUnnerLetters[4, 3] = '|';
            trollRUnnerLetters[4, 4] = ' ';
            trollRUnnerLetters[4, 5] = '|';
            trollRUnnerLetters[4, 6] = ' ';
            trollRUnnerLetters[4, 7] = ' ';
            trollRUnnerLetters[4, 8] = '|';
            trollRUnnerLetters[4, 9] = ' ';
            trollRUnnerLetters[4, 10] = ' ';
            trollRUnnerLetters[4, 11] = ' ';
            trollRUnnerLetters[4, 12] = ' ';
            trollRUnnerLetters[4, 13] = '_';
            trollRUnnerLetters[4, 14] = '_';
            trollRUnnerLetters[4, 15] = '_';
            trollRUnnerLetters[4, 16] = '|';
            trollRUnnerLetters[4, 17] = ' ';
            trollRUnnerLetters[4, 18] = '|';
            trollRUnnerLetters[4, 19] = ' ';
            trollRUnnerLetters[4, 20] = ' ';
            trollRUnnerLetters[4, 21] = ' ';
            trollRUnnerLetters[4, 22] = '|';
            trollRUnnerLetters[4, 23] = ' ';
            trollRUnnerLetters[4, 24] = '|';
            trollRUnnerLetters[4, 25] = ' ';
            trollRUnnerLetters[4, 26] = '|';
            trollRUnnerLetters[4, 27] = ' ';
            trollRUnnerLetters[4, 28] = ' ';
            trollRUnnerLetters[4, 29] = ' ';
            trollRUnnerLetters[4, 30] = ' ';
            trollRUnnerLetters[4, 31] = ' ';
            trollRUnnerLetters[4, 32] = '|';
            trollRUnnerLetters[4, 33] = ' ';
            trollRUnnerLetters[4, 34] = '|';
            trollRUnnerLetters[4, 35] = ' ';
            trollRUnnerLetters[4, 36] = ' ';
            trollRUnnerLetters[4, 37] = ' ';
            trollRUnnerLetters[4, 38] = ' ';
            trollRUnnerLetters[4, 39] = ' ';
            trollRUnnerLetters[4, 40] = ' ';
            trollRUnnerLetters[4, 41] = ' ';
            trollRUnnerLetters[4, 42] = ' ';
            trollRUnnerLetters[4, 43] = '|';
            trollRUnnerLetters[4, 44] = ' ';
            trollRUnnerLetters[4, 45] = ' ';
            trollRUnnerLetters[4, 46] = ' ';
            trollRUnnerLetters[4, 47] = ' ';
            trollRUnnerLetters[4, 48] = ' ';
            trollRUnnerLetters[4, 49] = '_';
            trollRUnnerLetters[4, 50] = '_';
            trollRUnnerLetters[4, 51] = '|';
            trollRUnnerLetters[4, 52] = ' ';
            trollRUnnerLetters[4, 53] = '|';
            trollRUnnerLetters[4, 54] = ' ';
            trollRUnnerLetters[4, 55] = ' ';
            trollRUnnerLetters[4, 56] = ' ';
            trollRUnnerLetters[4, 57] = '|';
            trollRUnnerLetters[4, 58] = ' ';
            trollRUnnerLetters[4, 59] = '|';
            trollRUnnerLetters[4, 60] = ' ';
            trollRUnnerLetters[4, 61] = '(';
            trollRUnnerLetters[4, 62] = '\\';
            trollRUnnerLetters[4, 63] = ' ';
            trollRUnnerLetters[4, 64] = '\\';
            trollRUnnerLetters[4, 65] = ')';
            trollRUnnerLetters[4, 66] = ' ';
            trollRUnnerLetters[4, 67] = '|';
            trollRUnnerLetters[4, 68] = ' ';
            trollRUnnerLetters[4, 69] = '(';
            trollRUnnerLetters[4, 70] = '\\';
            trollRUnnerLetters[4, 71] = ' ';
            trollRUnnerLetters[4, 72] = '\\';
            trollRUnnerLetters[4, 73] = ')';
            trollRUnnerLetters[4, 74] = ' ';
            trollRUnnerLetters[4, 75] = '|';
            trollRUnnerLetters[4, 76] = ' ';
            trollRUnnerLetters[4, 77] = ' ';
            trollRUnnerLetters[4, 78] = '_';
            trollRUnnerLetters[4, 79] = '_';
            trollRUnnerLetters[4, 80] = ')';
            trollRUnnerLetters[4, 81] = ' ';
            trollRUnnerLetters[4, 82] = ' ';
            trollRUnnerLetters[4, 83] = '|';
            trollRUnnerLetters[4, 84] = ' ';
            trollRUnnerLetters[4, 85] = ' ';
            trollRUnnerLetters[4, 86] = ' ';
            trollRUnnerLetters[4, 87] = ' ';
            trollRUnnerLetters[4, 88] = ' ';
            trollRUnnerLetters[4, 89] = '_';
            trollRUnnerLetters[4, 90] = '_';
            trollRUnnerLetters[4, 91] = ')';


            trollRUnnerLetters[5, 0] = ' ';
            trollRUnnerLetters[5, 1] = ' ';
            trollRUnnerLetters[5, 2] = ' ';
            trollRUnnerLetters[5, 3] = '|';
            trollRUnnerLetters[5, 4] = ' ';
            trollRUnnerLetters[5, 5] = '|';
            trollRUnnerLetters[5, 6] = ' ';
            trollRUnnerLetters[5, 7] = ' ';
            trollRUnnerLetters[5, 8] = '|';
            trollRUnnerLetters[5, 9] = ' ';
            trollRUnnerLetters[5, 10] = '(';
            trollRUnnerLetters[5, 11] = '\\';
            trollRUnnerLetters[5, 12] = ' ';
            trollRUnnerLetters[5, 13] = '(';
            trollRUnnerLetters[5, 14] = ' ';
            trollRUnnerLetters[5, 15] = ' ';
            trollRUnnerLetters[5, 16] = '|';
            trollRUnnerLetters[5, 17] = ' ';
            trollRUnnerLetters[5, 18] = '|';
            trollRUnnerLetters[5, 19] = ' ';
            trollRUnnerLetters[5, 20] = ' ';
            trollRUnnerLetters[5, 21] = ' ';
            trollRUnnerLetters[5, 22] = '|';
            trollRUnnerLetters[5, 23] = ' ';
            trollRUnnerLetters[5, 24] = '|';
            trollRUnnerLetters[5, 25] = ' ';
            trollRUnnerLetters[5, 26] = '|';
            trollRUnnerLetters[5, 27] = ' ';
            trollRUnnerLetters[5, 28] = ' ';
            trollRUnnerLetters[5, 29] = ' ';
            trollRUnnerLetters[5, 30] = ' ';
            trollRUnnerLetters[5, 31] = ' ';
            trollRUnnerLetters[5, 32] = '|';
            trollRUnnerLetters[5, 33] = ' ';
            trollRUnnerLetters[5, 34] = '|';
            trollRUnnerLetters[5, 35] = ' ';
            trollRUnnerLetters[5, 36] = ' ';
            trollRUnnerLetters[5, 37] = ' ';
            trollRUnnerLetters[5, 38] = ' ';
            trollRUnnerLetters[5, 39] = ' ';
            trollRUnnerLetters[5, 40] = ' ';
            trollRUnnerLetters[5, 41] = ' ';
            trollRUnnerLetters[5, 42] = ' ';
            trollRUnnerLetters[5, 43] = '|';
            trollRUnnerLetters[5, 44] = ' ';
            trollRUnnerLetters[5, 45] = '(';
            trollRUnnerLetters[5, 46] = '\\';
            trollRUnnerLetters[5, 47] = ' ';
            trollRUnnerLetters[5, 48] = '(';
            trollRUnnerLetters[5, 49] = ' ';
            trollRUnnerLetters[5, 50] = ' ';
            trollRUnnerLetters[5, 51] = '|';
            trollRUnnerLetters[5, 52] = ' ';
            trollRUnnerLetters[5, 53] = '|';
            trollRUnnerLetters[5, 54] = ' ';
            trollRUnnerLetters[5, 55] = ' ';
            trollRUnnerLetters[5, 56] = ' ';
            trollRUnnerLetters[5, 57] = '|';
            trollRUnnerLetters[5, 58] = ' ';
            trollRUnnerLetters[5, 59] = '|';
            trollRUnnerLetters[5, 60] = ' ';
            trollRUnnerLetters[5, 61] = '|';
            trollRUnnerLetters[5, 62] = ' ';
            trollRUnnerLetters[5, 63] = '\\';
            trollRUnnerLetters[5, 64] = ' ';
            trollRUnnerLetters[5, 65] = ' ';
            trollRUnnerLetters[5, 66] = ' ';
            trollRUnnerLetters[5, 67] = '|';
            trollRUnnerLetters[5, 68] = ' ';
            trollRUnnerLetters[5, 69] = '|';
            trollRUnnerLetters[5, 70] = ' ';
            trollRUnnerLetters[5, 71] = '\\';
            trollRUnnerLetters[5, 72] = ' ';
            trollRUnnerLetters[5, 73] = ' ';
            trollRUnnerLetters[5, 74] = ' ';
            trollRUnnerLetters[5, 75] = '|';
            trollRUnnerLetters[5, 76] = ' ';
            trollRUnnerLetters[5, 77] = '(';
            trollRUnnerLetters[5, 78] = ' ';
            trollRUnnerLetters[5, 79] = ' ';
            trollRUnnerLetters[5, 80] = ' ';
            trollRUnnerLetters[5, 81] = ' ';
            trollRUnnerLetters[5, 82] = ' ';
            trollRUnnerLetters[5, 83] = '|';
            trollRUnnerLetters[5, 84] = ' ';
            trollRUnnerLetters[5, 85] = '(';
            trollRUnnerLetters[5, 86] = '\\';
            trollRUnnerLetters[5, 87] = ' ';
            trollRUnnerLetters[5, 88] = '(';
            trollRUnnerLetters[5, 89] = ' ';
            trollRUnnerLetters[5, 90] = ' ';
            trollRUnnerLetters[5, 91] = ' ';


            trollRUnnerLetters[6, 0] = ' ';
            trollRUnnerLetters[6, 1] = ' ';
            trollRUnnerLetters[6, 2] = ' ';
            trollRUnnerLetters[6, 3] = '|';
            trollRUnnerLetters[6, 4] = ' ';
            trollRUnnerLetters[6, 5] = '|';
            trollRUnnerLetters[6, 6] = ' ';
            trollRUnnerLetters[6, 7] = ' ';
            trollRUnnerLetters[6, 8] = '|';
            trollRUnnerLetters[6, 9] = ' ';
            trollRUnnerLetters[6, 10] = ')';
            trollRUnnerLetters[6, 11] = ' ';
            trollRUnnerLetters[6, 12] = '\\';
            trollRUnnerLetters[6, 13] = ' ';
            trollRUnnerLetters[6, 14] = '\\';
            trollRUnnerLetters[6, 15] = '_';
            trollRUnnerLetters[6, 16] = '|';
            trollRUnnerLetters[6, 17] = ' ';
            trollRUnnerLetters[6, 18] = '(';
            trollRUnnerLetters[6, 19] = '_';
            trollRUnnerLetters[6, 20] = '_';
            trollRUnnerLetters[6, 21] = '_';
            trollRUnnerLetters[6, 22] = ')';
            trollRUnnerLetters[6, 23] = ' ';
            trollRUnnerLetters[6, 24] = '|';
            trollRUnnerLetters[6, 25] = ' ';
            trollRUnnerLetters[6, 26] = '(';
            trollRUnnerLetters[6, 27] = '_';
            trollRUnnerLetters[6, 28] = '_';
            trollRUnnerLetters[6, 29] = '_';
            trollRUnnerLetters[6, 30] = '_';
            trollRUnnerLetters[6, 31] = '/';
            trollRUnnerLetters[6, 32] = '|';
            trollRUnnerLetters[6, 33] = ' ';
            trollRUnnerLetters[6, 34] = '(';
            trollRUnnerLetters[6, 35] = '_';
            trollRUnnerLetters[6, 36] = '_';
            trollRUnnerLetters[6, 37] = '_';
            trollRUnnerLetters[6, 38] = '_';
            trollRUnnerLetters[6, 39] = '/';
            trollRUnnerLetters[6, 40] = '\\';
            trollRUnnerLetters[6, 41] = ' ';
            trollRUnnerLetters[6, 42] = ' ';
            trollRUnnerLetters[6, 43] = '|';
            trollRUnnerLetters[6, 44] = ' ';
            trollRUnnerLetters[6, 45] = ')';
            trollRUnnerLetters[6, 46] = ' ';
            trollRUnnerLetters[6, 47] = '\\';
            trollRUnnerLetters[6, 48] = ' ';
            trollRUnnerLetters[6, 49] = '\\';
            trollRUnnerLetters[6, 50] = '_';
            trollRUnnerLetters[6, 51] = '|';
            trollRUnnerLetters[6, 52] = ' ';
            trollRUnnerLetters[6, 53] = '(';
            trollRUnnerLetters[6, 54] = '_';
            trollRUnnerLetters[6, 55] = '_';
            trollRUnnerLetters[6, 56] = '_';
            trollRUnnerLetters[6, 57] = ')';
            trollRUnnerLetters[6, 58] = ' ';
            trollRUnnerLetters[6, 59] = '|';
            trollRUnnerLetters[6, 60] = ' ';
            trollRUnnerLetters[6, 61] = ')';
            trollRUnnerLetters[6, 62] = ' ';
            trollRUnnerLetters[6, 63] = ' ';
            trollRUnnerLetters[6, 64] = '\\';
            trollRUnnerLetters[6, 65] = ' ';
            trollRUnnerLetters[6, 66] = ' ';
            trollRUnnerLetters[6, 67] = '|';
            trollRUnnerLetters[6, 68] = ' ';
            trollRUnnerLetters[6, 69] = ')';
            trollRUnnerLetters[6, 70] = ' ';
            trollRUnnerLetters[6, 71] = ' ';
            trollRUnnerLetters[6, 72] = '\\';
            trollRUnnerLetters[6, 73] = ' ';
            trollRUnnerLetters[6, 74] = ' ';
            trollRUnnerLetters[6, 75] = '|';
            trollRUnnerLetters[6, 76] = ' ';
            trollRUnnerLetters[6, 77] = '(';
            trollRUnnerLetters[6, 78] = '_';
            trollRUnnerLetters[6, 79] = '_';
            trollRUnnerLetters[6, 80] = '_';
            trollRUnnerLetters[6, 81] = '_';
            trollRUnnerLetters[6, 82] = '/';
            trollRUnnerLetters[6, 83] = '|';
            trollRUnnerLetters[6, 84] = ' ';
            trollRUnnerLetters[6, 85] = ')';
            trollRUnnerLetters[6, 86] = ' ';
            trollRUnnerLetters[6, 87] = '\\';
            trollRUnnerLetters[6, 88] = ' ';
            trollRUnnerLetters[6, 89] = '\\';
            trollRUnnerLetters[6, 90] = '_';
            trollRUnnerLetters[6, 91] = '_';

            trollRUnnerLetters[7, 0] = ' ';
            trollRUnnerLetters[7, 1] = ' ';
            trollRUnnerLetters[7, 2] = ' ';
            trollRUnnerLetters[7, 3] = ')';
            trollRUnnerLetters[7, 4] = '_';
            trollRUnnerLetters[7, 5] = '(';
            trollRUnnerLetters[7, 6] = ' ';
            trollRUnnerLetters[7, 7] = ' ';
            trollRUnnerLetters[7, 8] = '|';
            trollRUnnerLetters[7, 9] = '/';
            trollRUnnerLetters[7, 10] = ' ';
            trollRUnnerLetters[7, 11] = ' ';
            trollRUnnerLetters[7, 12] = ' ';
            trollRUnnerLetters[7, 13] = '\\';
            trollRUnnerLetters[7, 14] = '_';
            trollRUnnerLetters[7, 15] = '_';
            trollRUnnerLetters[7, 16] = '(';
            trollRUnnerLetters[7, 17] = '_';
            trollRUnnerLetters[7, 18] = '_';
            trollRUnnerLetters[7, 19] = '_';
            trollRUnnerLetters[7, 20] = '_';
            trollRUnnerLetters[7, 21] = '_';
            trollRUnnerLetters[7, 22] = '_';
            trollRUnnerLetters[7, 23] = '_';
            trollRUnnerLetters[7, 24] = '(';
            trollRUnnerLetters[7, 25] = '_';
            trollRUnnerLetters[7, 26] = '_';
            trollRUnnerLetters[7, 27] = '_';
            trollRUnnerLetters[7, 28] = '_';
            trollRUnnerLetters[7, 29] = '_';
            trollRUnnerLetters[7, 30] = '_';
            trollRUnnerLetters[7, 31] = '_';
            trollRUnnerLetters[7, 32] = '(';
            trollRUnnerLetters[7, 33] = '_';
            trollRUnnerLetters[7, 34] = '_';
            trollRUnnerLetters[7, 35] = '_';
            trollRUnnerLetters[7, 36] = '_';
            trollRUnnerLetters[7, 37] = '_';
            trollRUnnerLetters[7, 38] = '_';
            trollRUnnerLetters[7, 39] = '_';
            trollRUnnerLetters[7, 40] = '/';
            trollRUnnerLetters[7, 41] = ' ';
            trollRUnnerLetters[7, 42] = ' ';
            trollRUnnerLetters[7, 43] = '|';
            trollRUnnerLetters[7, 44] = '/';
            trollRUnnerLetters[7, 45] = ' ';
            trollRUnnerLetters[7, 46] = ' ';
            trollRUnnerLetters[7, 47] = ' ';
            trollRUnnerLetters[7, 48] = '\\';
            trollRUnnerLetters[7, 49] = '_';
            trollRUnnerLetters[7, 50] = '_';
            trollRUnnerLetters[7, 51] = '(';
            trollRUnnerLetters[7, 52] = '_';
            trollRUnnerLetters[7, 53] = '_';
            trollRUnnerLetters[7, 54] = '_';
            trollRUnnerLetters[7, 55] = '_';
            trollRUnnerLetters[7, 56] = '_';
            trollRUnnerLetters[7, 57] = '_';
            trollRUnnerLetters[7, 58] = '_';
            trollRUnnerLetters[7, 59] = '|';
            trollRUnnerLetters[7, 60] = '/';
            trollRUnnerLetters[7, 61] = ' ';
            trollRUnnerLetters[7, 62] = ' ';
            trollRUnnerLetters[7, 63] = ' ';
            trollRUnnerLetters[7, 64] = ' ';
            trollRUnnerLetters[7, 65] = ')';
            trollRUnnerLetters[7, 66] = '_';
            trollRUnnerLetters[7, 67] = '|';
            trollRUnnerLetters[7, 68] = '/';
            trollRUnnerLetters[7, 69] = ' ';
            trollRUnnerLetters[7, 70] = ' ';
            trollRUnnerLetters[7, 71] = ' ';
            trollRUnnerLetters[7, 72] = ' ';
            trollRUnnerLetters[7, 73] = ')';
            trollRUnnerLetters[7, 74] = '_';
            trollRUnnerLetters[7, 75] = '(';
            trollRUnnerLetters[7, 76] = '_';
            trollRUnnerLetters[7, 77] = '_';
            trollRUnnerLetters[7, 78] = '_';
            trollRUnnerLetters[7, 79] = '_';
            trollRUnnerLetters[7, 80] = '_';
            trollRUnnerLetters[7, 81] = '_';
            trollRUnnerLetters[7, 82] = '_';
            trollRUnnerLetters[7, 83] = '|';
            trollRUnnerLetters[7, 84] = '/';
            trollRUnnerLetters[7, 85] = ' ';
            trollRUnnerLetters[7, 86] = ' ';
            trollRUnnerLetters[7, 87] = ' ';
            trollRUnnerLetters[7, 88] = '\\';
            trollRUnnerLetters[7, 89] = '_';
            trollRUnnerLetters[7, 90] = '_';
            trollRUnnerLetters[7, 91] = '/';


            Console.Write(trollRUnnerLetters[0, 0]); Console.Write(trollRUnnerLetters[0, 1]);
            Console.Write(trollRUnnerLetters[0, 2]); Console.Write(trollRUnnerLetters[0, 3]);
            Console.Write(trollRUnnerLetters[0, 4]); Console.Write(trollRUnnerLetters[0, 5]);
            Console.Write(trollRUnnerLetters[0, 6]); Console.Write(trollRUnnerLetters[0, 7]);
            Console.Write(trollRUnnerLetters[0, 8]); Console.Write(trollRUnnerLetters[0, 9]);
            Console.Write(trollRUnnerLetters[0, 10]); Console.Write(trollRUnnerLetters[0, 11]);
            Console.Write(trollRUnnerLetters[0, 12]); Console.Write(trollRUnnerLetters[0, 13]);
            Console.Write(trollRUnnerLetters[0, 14]); Console.Write(trollRUnnerLetters[0, 15]);
            Console.Write(trollRUnnerLetters[0, 16]); Console.Write(trollRUnnerLetters[0, 17]);
            Console.Write(trollRUnnerLetters[0, 18]); Console.Write(trollRUnnerLetters[0, 19]);
            Console.Write(trollRUnnerLetters[0, 20]); Console.Write(trollRUnnerLetters[0, 21]);
            Console.Write(trollRUnnerLetters[0, 22]); Console.Write(trollRUnnerLetters[0, 23]);
            Console.Write(trollRUnnerLetters[0, 24]); Console.Write(trollRUnnerLetters[0, 25]);
            Console.Write(trollRUnnerLetters[0, 26]); Console.Write(trollRUnnerLetters[0, 27]);
            Console.Write(trollRUnnerLetters[0, 28]); Console.Write(trollRUnnerLetters[0, 29]);
            Console.Write(trollRUnnerLetters[0, 30]); Console.Write(trollRUnnerLetters[0, 31]);
            Console.Write(trollRUnnerLetters[0, 32]); Console.Write(trollRUnnerLetters[0, 33]);
            Console.Write(trollRUnnerLetters[0, 34]); Console.Write(trollRUnnerLetters[0, 35]);
            Console.Write(trollRUnnerLetters[0, 36]); Console.Write(trollRUnnerLetters[0, 37]);
            Console.Write(trollRUnnerLetters[0, 38]); Console.Write(trollRUnnerLetters[0, 39]);
            Console.Write(trollRUnnerLetters[0, 40]); Console.Write(trollRUnnerLetters[0, 41]);
            Console.Write(trollRUnnerLetters[0, 42]); Console.Write(trollRUnnerLetters[0, 43]);
            Console.Write(trollRUnnerLetters[0, 44]); Console.Write(trollRUnnerLetters[0, 45]);
            Console.Write(trollRUnnerLetters[0, 46]); Console.Write(trollRUnnerLetters[0, 47]);
            Console.Write(trollRUnnerLetters[0, 48]); Console.Write(trollRUnnerLetters[0, 49]);
            Console.Write(trollRUnnerLetters[0, 50]); Console.Write(trollRUnnerLetters[0, 51]);
            Console.Write(trollRUnnerLetters[0, 52]); Console.Write(trollRUnnerLetters[0, 53]);
            Console.Write(trollRUnnerLetters[0, 54]); Console.Write(trollRUnnerLetters[0, 55]);
            Console.Write(trollRUnnerLetters[0, 56]); Console.Write(trollRUnnerLetters[0, 57]);
            Console.Write(trollRUnnerLetters[0, 58]); Console.Write(trollRUnnerLetters[0, 59]);
            Console.Write(trollRUnnerLetters[0, 60]); Console.Write(trollRUnnerLetters[0, 61]);
            Console.Write(trollRUnnerLetters[0, 62]); Console.Write(trollRUnnerLetters[0, 63]);
            Console.Write(trollRUnnerLetters[0, 64]); Console.Write(trollRUnnerLetters[0, 65]);
            Console.Write(trollRUnnerLetters[0, 66]); Console.Write(trollRUnnerLetters[0, 67]);
            Console.Write(trollRUnnerLetters[0, 68]); Console.Write(trollRUnnerLetters[0, 69]);
            Console.Write(trollRUnnerLetters[0, 70]); Console.Write(trollRUnnerLetters[0, 71]);
            Console.Write(trollRUnnerLetters[0, 72]); Console.Write(trollRUnnerLetters[0, 73]);
            Console.Write(trollRUnnerLetters[0, 74]); Console.Write(trollRUnnerLetters[0, 75]);
            Console.Write(trollRUnnerLetters[0, 76]); Console.Write(trollRUnnerLetters[0, 77]);
            Console.Write(trollRUnnerLetters[0, 78]); Console.Write(trollRUnnerLetters[0, 79]);
            Console.Write(trollRUnnerLetters[0, 80]); Console.Write(trollRUnnerLetters[0, 81]);
            Console.Write(trollRUnnerLetters[0, 82]); Console.Write(trollRUnnerLetters[0, 83]);
            Console.Write(trollRUnnerLetters[0, 84]); Console.Write(trollRUnnerLetters[0, 85]);
            Console.Write(trollRUnnerLetters[0, 86]); Console.Write(trollRUnnerLetters[0, 87]);
            Console.Write(trollRUnnerLetters[0, 88]); Console.Write(trollRUnnerLetters[0, 89]);
            Console.Write(trollRUnnerLetters[0, 90]); Console.WriteLine(trollRUnnerLetters[0, 91]);


            Console.Write(trollRUnnerLetters[1, 0]); Console.Write(trollRUnnerLetters[1, 1]);
            Console.Write(trollRUnnerLetters[1, 2]); Console.Write(trollRUnnerLetters[1, 3]);
            Console.Write(trollRUnnerLetters[1, 4]); Console.Write(trollRUnnerLetters[1, 5]);
            Console.Write(trollRUnnerLetters[1, 6]); Console.Write(trollRUnnerLetters[1, 7]);
            Console.Write(trollRUnnerLetters[1, 8]); Console.Write(trollRUnnerLetters[1, 9]);
            Console.Write(trollRUnnerLetters[1, 10]); Console.Write(trollRUnnerLetters[1, 11]);
            Console.Write(trollRUnnerLetters[1, 12]); Console.Write(trollRUnnerLetters[1, 13]);
            Console.Write(trollRUnnerLetters[1, 14]); Console.Write(trollRUnnerLetters[1, 15]);
            Console.Write(trollRUnnerLetters[1, 16]); Console.Write(trollRUnnerLetters[1, 17]);
            Console.Write(trollRUnnerLetters[1, 18]); Console.Write(trollRUnnerLetters[1, 19]);
            Console.Write(trollRUnnerLetters[1, 20]); Console.Write(trollRUnnerLetters[1, 21]);
            Console.Write(trollRUnnerLetters[1, 22]); Console.Write(trollRUnnerLetters[1, 23]);
            Console.Write(trollRUnnerLetters[1, 24]); Console.Write(trollRUnnerLetters[1, 25]);
            Console.Write(trollRUnnerLetters[1, 26]); Console.Write(trollRUnnerLetters[1, 27]);
            Console.Write(trollRUnnerLetters[1, 28]); Console.Write(trollRUnnerLetters[1, 29]);
            Console.Write(trollRUnnerLetters[1, 30]); Console.Write(trollRUnnerLetters[1, 31]);
            Console.Write(trollRUnnerLetters[1, 32]); Console.Write(trollRUnnerLetters[1, 33]);
            Console.Write(trollRUnnerLetters[1, 34]); Console.Write(trollRUnnerLetters[1, 35]);
            Console.Write(trollRUnnerLetters[1, 36]); Console.Write(trollRUnnerLetters[1, 37]);
            Console.Write(trollRUnnerLetters[1, 38]); Console.Write(trollRUnnerLetters[1, 39]);
            Console.Write(trollRUnnerLetters[1, 40]); Console.Write(trollRUnnerLetters[1, 41]);
            Console.Write(trollRUnnerLetters[1, 42]); Console.Write(trollRUnnerLetters[1, 43]);
            Console.Write(trollRUnnerLetters[1, 44]); Console.Write(trollRUnnerLetters[1, 45]);
            Console.Write(trollRUnnerLetters[1, 46]); Console.Write(trollRUnnerLetters[1, 47]);
            Console.Write(trollRUnnerLetters[1, 48]); Console.Write(trollRUnnerLetters[1, 49]);
            Console.Write(trollRUnnerLetters[1, 50]); Console.Write(trollRUnnerLetters[1, 51]);
            Console.Write(trollRUnnerLetters[1, 52]); Console.Write(trollRUnnerLetters[1, 53]);
            Console.Write(trollRUnnerLetters[1, 54]); Console.Write(trollRUnnerLetters[1, 55]);
            Console.Write(trollRUnnerLetters[1, 56]); Console.Write(trollRUnnerLetters[1, 57]);
            Console.Write(trollRUnnerLetters[1, 58]); Console.Write(trollRUnnerLetters[1, 59]);
            Console.Write(trollRUnnerLetters[1, 60]); Console.Write(trollRUnnerLetters[1, 61]);
            Console.Write(trollRUnnerLetters[1, 62]); Console.Write(trollRUnnerLetters[1, 63]);
            Console.Write(trollRUnnerLetters[1, 64]); Console.Write(trollRUnnerLetters[1, 65]);
            Console.Write(trollRUnnerLetters[1, 66]); Console.Write(trollRUnnerLetters[1, 67]);
            Console.Write(trollRUnnerLetters[1, 68]); Console.Write(trollRUnnerLetters[1, 69]);
            Console.Write(trollRUnnerLetters[1, 70]); Console.Write(trollRUnnerLetters[1, 71]);
            Console.Write(trollRUnnerLetters[1, 72]); Console.Write(trollRUnnerLetters[1, 73]);
            Console.Write(trollRUnnerLetters[1, 74]); Console.Write(trollRUnnerLetters[1, 75]);
            Console.Write(trollRUnnerLetters[1, 76]); Console.Write(trollRUnnerLetters[1, 77]);
            Console.Write(trollRUnnerLetters[1, 78]); Console.Write(trollRUnnerLetters[1, 79]);
            Console.Write(trollRUnnerLetters[1, 80]); Console.Write(trollRUnnerLetters[1, 81]);
            Console.Write(trollRUnnerLetters[1, 82]); Console.Write(trollRUnnerLetters[1, 83]);
            Console.Write(trollRUnnerLetters[1, 84]); Console.Write(trollRUnnerLetters[1, 85]);
            Console.Write(trollRUnnerLetters[1, 86]); Console.Write(trollRUnnerLetters[1, 87]);
            Console.Write(trollRUnnerLetters[1, 88]); Console.Write(trollRUnnerLetters[1, 89]);
            Console.Write(trollRUnnerLetters[1, 90]); Console.WriteLine(trollRUnnerLetters[1, 91]);

            Console.Write(trollRUnnerLetters[2, 0]); Console.Write(trollRUnnerLetters[2, 1]);
            Console.Write(trollRUnnerLetters[2, 2]); Console.Write(trollRUnnerLetters[2, 3]);
            Console.Write(trollRUnnerLetters[2, 4]); Console.Write(trollRUnnerLetters[2, 5]);
            Console.Write(trollRUnnerLetters[2, 6]); Console.Write(trollRUnnerLetters[2, 7]);
            Console.Write(trollRUnnerLetters[2, 8]); Console.Write(trollRUnnerLetters[2, 9]);
            Console.Write(trollRUnnerLetters[2, 10]); Console.Write(trollRUnnerLetters[2, 11]);
            Console.Write(trollRUnnerLetters[2, 12]); Console.Write(trollRUnnerLetters[2, 13]);
            Console.Write(trollRUnnerLetters[2, 14]); Console.Write(trollRUnnerLetters[2, 15]);
            Console.Write(trollRUnnerLetters[2, 16]); Console.Write(trollRUnnerLetters[2, 17]);
            Console.Write(trollRUnnerLetters[2, 18]); Console.Write(trollRUnnerLetters[2, 19]);
            Console.Write(trollRUnnerLetters[2, 20]); Console.Write(trollRUnnerLetters[2, 21]);
            Console.Write(trollRUnnerLetters[2, 22]); Console.Write(trollRUnnerLetters[2, 23]);
            Console.Write(trollRUnnerLetters[2, 24]); Console.Write(trollRUnnerLetters[2, 25]);
            Console.Write(trollRUnnerLetters[2, 26]); Console.Write(trollRUnnerLetters[2, 27]);
            Console.Write(trollRUnnerLetters[2, 28]); Console.Write(trollRUnnerLetters[2, 29]);
            Console.Write(trollRUnnerLetters[2, 30]); Console.Write(trollRUnnerLetters[2, 31]);
            Console.Write(trollRUnnerLetters[2, 32]); Console.Write(trollRUnnerLetters[2, 33]);
            Console.Write(trollRUnnerLetters[2, 34]); Console.Write(trollRUnnerLetters[2, 35]);
            Console.Write(trollRUnnerLetters[2, 36]); Console.Write(trollRUnnerLetters[2, 37]);
            Console.Write(trollRUnnerLetters[2, 38]); Console.Write(trollRUnnerLetters[2, 39]);
            Console.Write(trollRUnnerLetters[2, 40]); Console.Write(trollRUnnerLetters[2, 41]);
            Console.Write(trollRUnnerLetters[2, 42]); Console.Write(trollRUnnerLetters[2, 43]);
            Console.Write(trollRUnnerLetters[2, 44]); Console.Write(trollRUnnerLetters[2, 45]);
            Console.Write(trollRUnnerLetters[2, 46]); Console.Write(trollRUnnerLetters[2, 47]);
            Console.Write(trollRUnnerLetters[2, 48]); Console.Write(trollRUnnerLetters[2, 49]);
            Console.Write(trollRUnnerLetters[2, 50]); Console.Write(trollRUnnerLetters[2, 51]);
            Console.Write(trollRUnnerLetters[2, 52]); Console.Write(trollRUnnerLetters[2, 53]);
            Console.Write(trollRUnnerLetters[2, 54]); Console.Write(trollRUnnerLetters[2, 55]);
            Console.Write(trollRUnnerLetters[2, 56]); Console.Write(trollRUnnerLetters[2, 57]);
            Console.Write(trollRUnnerLetters[2, 58]); Console.Write(trollRUnnerLetters[2, 59]);
            Console.Write(trollRUnnerLetters[2, 60]); Console.Write(trollRUnnerLetters[2, 61]);
            Console.Write(trollRUnnerLetters[2, 62]); Console.Write(trollRUnnerLetters[2, 63]);
            Console.Write(trollRUnnerLetters[2, 64]); Console.Write(trollRUnnerLetters[2, 65]);
            Console.Write(trollRUnnerLetters[2, 66]); Console.Write(trollRUnnerLetters[2, 67]);
            Console.Write(trollRUnnerLetters[2, 68]); Console.Write(trollRUnnerLetters[2, 69]);
            Console.Write(trollRUnnerLetters[2, 70]); Console.Write(trollRUnnerLetters[2, 71]);
            Console.Write(trollRUnnerLetters[2, 72]); Console.Write(trollRUnnerLetters[2, 73]);
            Console.Write(trollRUnnerLetters[2, 74]); Console.Write(trollRUnnerLetters[2, 75]);
            Console.Write(trollRUnnerLetters[2, 76]); Console.Write(trollRUnnerLetters[2, 77]);
            Console.Write(trollRUnnerLetters[2, 78]); Console.Write(trollRUnnerLetters[2, 79]);
            Console.Write(trollRUnnerLetters[2, 80]); Console.Write(trollRUnnerLetters[2, 81]);
            Console.Write(trollRUnnerLetters[2, 82]); Console.Write(trollRUnnerLetters[2, 83]);
            Console.Write(trollRUnnerLetters[2, 84]); Console.Write(trollRUnnerLetters[2, 85]);
            Console.Write(trollRUnnerLetters[2, 86]); Console.Write(trollRUnnerLetters[2, 87]);
            Console.Write(trollRUnnerLetters[2, 88]); Console.Write(trollRUnnerLetters[2, 89]);
            Console.Write(trollRUnnerLetters[2, 90]); Console.WriteLine(trollRUnnerLetters[2, 91]);

            Console.Write(trollRUnnerLetters[3, 0]); Console.Write(trollRUnnerLetters[3, 1]);
            Console.Write(trollRUnnerLetters[3, 2]); Console.Write(trollRUnnerLetters[3, 3]);
            Console.Write(trollRUnnerLetters[3, 4]); Console.Write(trollRUnnerLetters[3, 5]);
            Console.Write(trollRUnnerLetters[3, 6]); Console.Write(trollRUnnerLetters[3, 7]);
            Console.Write(trollRUnnerLetters[3, 8]); Console.Write(trollRUnnerLetters[3, 9]);
            Console.Write(trollRUnnerLetters[3, 10]); Console.Write(trollRUnnerLetters[3, 11]);
            Console.Write(trollRUnnerLetters[3, 12]); Console.Write(trollRUnnerLetters[3, 13]);
            Console.Write(trollRUnnerLetters[3, 14]); Console.Write(trollRUnnerLetters[3, 15]);
            Console.Write(trollRUnnerLetters[3, 16]); Console.Write(trollRUnnerLetters[3, 17]);
            Console.Write(trollRUnnerLetters[3, 18]); Console.Write(trollRUnnerLetters[3, 19]);
            Console.Write(trollRUnnerLetters[3, 20]); Console.Write(trollRUnnerLetters[3, 21]);
            Console.Write(trollRUnnerLetters[3, 22]); Console.Write(trollRUnnerLetters[3, 23]);
            Console.Write(trollRUnnerLetters[3, 24]); Console.Write(trollRUnnerLetters[3, 25]);
            Console.Write(trollRUnnerLetters[3, 26]); Console.Write(trollRUnnerLetters[3, 27]);
            Console.Write(trollRUnnerLetters[3, 28]); Console.Write(trollRUnnerLetters[3, 29]);
            Console.Write(trollRUnnerLetters[3, 30]); Console.Write(trollRUnnerLetters[3, 31]);
            Console.Write(trollRUnnerLetters[3, 32]); Console.Write(trollRUnnerLetters[3, 33]);
            Console.Write(trollRUnnerLetters[3, 34]); Console.Write(trollRUnnerLetters[3, 35]);
            Console.Write(trollRUnnerLetters[3, 36]); Console.Write(trollRUnnerLetters[3, 37]);
            Console.Write(trollRUnnerLetters[3, 38]); Console.Write(trollRUnnerLetters[3, 39]);
            Console.Write(trollRUnnerLetters[3, 40]); Console.Write(trollRUnnerLetters[3, 41]);
            Console.Write(trollRUnnerLetters[3, 42]); Console.Write(trollRUnnerLetters[3, 43]);
            Console.Write(trollRUnnerLetters[3, 44]); Console.Write(trollRUnnerLetters[3, 45]);
            Console.Write(trollRUnnerLetters[3, 46]); Console.Write(trollRUnnerLetters[3, 47]);
            Console.Write(trollRUnnerLetters[3, 48]); Console.Write(trollRUnnerLetters[3, 49]);
            Console.Write(trollRUnnerLetters[3, 50]); Console.Write(trollRUnnerLetters[3, 51]);
            Console.Write(trollRUnnerLetters[3, 52]); Console.Write(trollRUnnerLetters[3, 53]);
            Console.Write(trollRUnnerLetters[3, 54]); Console.Write(trollRUnnerLetters[3, 55]);
            Console.Write(trollRUnnerLetters[3, 56]); Console.Write(trollRUnnerLetters[3, 57]);
            Console.Write(trollRUnnerLetters[3, 58]); Console.Write(trollRUnnerLetters[3, 59]);
            Console.Write(trollRUnnerLetters[3, 60]); Console.Write(trollRUnnerLetters[3, 61]);
            Console.Write(trollRUnnerLetters[3, 62]); Console.Write(trollRUnnerLetters[3, 63]);
            Console.Write(trollRUnnerLetters[3, 64]); Console.Write(trollRUnnerLetters[3, 65]);
            Console.Write(trollRUnnerLetters[3, 66]); Console.Write(trollRUnnerLetters[3, 67]);
            Console.Write(trollRUnnerLetters[3, 68]); Console.Write(trollRUnnerLetters[3, 69]);
            Console.Write(trollRUnnerLetters[3, 70]); Console.Write(trollRUnnerLetters[3, 71]);
            Console.Write(trollRUnnerLetters[3, 72]); Console.Write(trollRUnnerLetters[3, 73]);
            Console.Write(trollRUnnerLetters[3, 74]); Console.Write(trollRUnnerLetters[3, 75]);
            Console.Write(trollRUnnerLetters[3, 76]); Console.Write(trollRUnnerLetters[3, 77]);
            Console.Write(trollRUnnerLetters[3, 78]); Console.Write(trollRUnnerLetters[3, 79]);
            Console.Write(trollRUnnerLetters[3, 80]); Console.Write(trollRUnnerLetters[3, 81]);
            Console.Write(trollRUnnerLetters[3, 82]); Console.Write(trollRUnnerLetters[3, 83]);
            Console.Write(trollRUnnerLetters[3, 84]); Console.Write(trollRUnnerLetters[3, 85]);
            Console.Write(trollRUnnerLetters[3, 86]); Console.Write(trollRUnnerLetters[3, 87]);
            Console.Write(trollRUnnerLetters[3, 88]); Console.Write(trollRUnnerLetters[3, 89]);
            Console.Write(trollRUnnerLetters[3, 90]); Console.WriteLine(trollRUnnerLetters[3, 91]);

            Console.Write(trollRUnnerLetters[4, 0]); Console.Write(trollRUnnerLetters[4, 1]);
            Console.Write(trollRUnnerLetters[4, 2]); Console.Write(trollRUnnerLetters[4, 3]);
            Console.Write(trollRUnnerLetters[4, 4]); Console.Write(trollRUnnerLetters[4, 5]);
            Console.Write(trollRUnnerLetters[4, 6]); Console.Write(trollRUnnerLetters[4, 7]);
            Console.Write(trollRUnnerLetters[4, 8]); Console.Write(trollRUnnerLetters[4, 9]);
            Console.Write(trollRUnnerLetters[4, 10]); Console.Write(trollRUnnerLetters[4, 11]);
            Console.Write(trollRUnnerLetters[4, 12]); Console.Write(trollRUnnerLetters[4, 13]);
            Console.Write(trollRUnnerLetters[4, 14]); Console.Write(trollRUnnerLetters[4, 15]);
            Console.Write(trollRUnnerLetters[4, 16]); Console.Write(trollRUnnerLetters[4, 17]);
            Console.Write(trollRUnnerLetters[4, 18]); Console.Write(trollRUnnerLetters[4, 19]);
            Console.Write(trollRUnnerLetters[4, 20]); Console.Write(trollRUnnerLetters[4, 21]);
            Console.Write(trollRUnnerLetters[4, 22]); Console.Write(trollRUnnerLetters[4, 23]);
            Console.Write(trollRUnnerLetters[4, 24]); Console.Write(trollRUnnerLetters[4, 25]);
            Console.Write(trollRUnnerLetters[4, 26]); Console.Write(trollRUnnerLetters[4, 27]);
            Console.Write(trollRUnnerLetters[4, 28]); Console.Write(trollRUnnerLetters[4, 29]);
            Console.Write(trollRUnnerLetters[4, 30]); Console.Write(trollRUnnerLetters[4, 31]);
            Console.Write(trollRUnnerLetters[4, 32]); Console.Write(trollRUnnerLetters[4, 33]);
            Console.Write(trollRUnnerLetters[4, 34]); Console.Write(trollRUnnerLetters[4, 35]);
            Console.Write(trollRUnnerLetters[4, 36]); Console.Write(trollRUnnerLetters[4, 37]);
            Console.Write(trollRUnnerLetters[4, 38]); Console.Write(trollRUnnerLetters[4, 39]);
            Console.Write(trollRUnnerLetters[4, 40]); Console.Write(trollRUnnerLetters[4, 41]);
            Console.Write(trollRUnnerLetters[4, 42]); Console.Write(trollRUnnerLetters[4, 43]);
            Console.Write(trollRUnnerLetters[4, 44]); Console.Write(trollRUnnerLetters[4, 45]);
            Console.Write(trollRUnnerLetters[4, 46]); Console.Write(trollRUnnerLetters[4, 47]);
            Console.Write(trollRUnnerLetters[4, 48]); Console.Write(trollRUnnerLetters[4, 49]);
            Console.Write(trollRUnnerLetters[4, 50]); Console.Write(trollRUnnerLetters[4, 51]);
            Console.Write(trollRUnnerLetters[4, 52]); Console.Write(trollRUnnerLetters[4, 53]);
            Console.Write(trollRUnnerLetters[4, 54]); Console.Write(trollRUnnerLetters[4, 55]);
            Console.Write(trollRUnnerLetters[4, 56]); Console.Write(trollRUnnerLetters[4, 57]);
            Console.Write(trollRUnnerLetters[4, 58]); Console.Write(trollRUnnerLetters[4, 59]);
            Console.Write(trollRUnnerLetters[4, 60]); Console.Write(trollRUnnerLetters[4, 61]);
            Console.Write(trollRUnnerLetters[4, 62]); Console.Write(trollRUnnerLetters[4, 63]);
            Console.Write(trollRUnnerLetters[4, 64]); Console.Write(trollRUnnerLetters[4, 65]);
            Console.Write(trollRUnnerLetters[4, 66]); Console.Write(trollRUnnerLetters[4, 67]);
            Console.Write(trollRUnnerLetters[4, 68]); Console.Write(trollRUnnerLetters[4, 69]);
            Console.Write(trollRUnnerLetters[4, 70]); Console.Write(trollRUnnerLetters[4, 71]);
            Console.Write(trollRUnnerLetters[4, 72]); Console.Write(trollRUnnerLetters[4, 73]);
            Console.Write(trollRUnnerLetters[4, 74]); Console.Write(trollRUnnerLetters[4, 75]);
            Console.Write(trollRUnnerLetters[4, 76]); Console.Write(trollRUnnerLetters[4, 77]);
            Console.Write(trollRUnnerLetters[4, 78]); Console.Write(trollRUnnerLetters[4, 79]);
            Console.Write(trollRUnnerLetters[4, 80]); Console.Write(trollRUnnerLetters[4, 81]);
            Console.Write(trollRUnnerLetters[4, 82]); Console.Write(trollRUnnerLetters[4, 83]);
            Console.Write(trollRUnnerLetters[4, 84]); Console.Write(trollRUnnerLetters[4, 85]);
            Console.Write(trollRUnnerLetters[4, 86]); Console.Write(trollRUnnerLetters[4, 87]);
            Console.Write(trollRUnnerLetters[4, 88]); Console.Write(trollRUnnerLetters[4, 89]);
            Console.Write(trollRUnnerLetters[4, 90]); Console.WriteLine(trollRUnnerLetters[4, 91]);

            Console.Write(trollRUnnerLetters[5, 0]); Console.Write(trollRUnnerLetters[5, 1]);
            Console.Write(trollRUnnerLetters[5, 2]); Console.Write(trollRUnnerLetters[5, 3]);
            Console.Write(trollRUnnerLetters[5, 4]); Console.Write(trollRUnnerLetters[5, 5]);
            Console.Write(trollRUnnerLetters[5, 6]); Console.Write(trollRUnnerLetters[5, 7]);
            Console.Write(trollRUnnerLetters[5, 8]); Console.Write(trollRUnnerLetters[5, 9]);
            Console.Write(trollRUnnerLetters[5, 10]); Console.Write(trollRUnnerLetters[5, 11]);
            Console.Write(trollRUnnerLetters[5, 12]); Console.Write(trollRUnnerLetters[5, 13]);
            Console.Write(trollRUnnerLetters[5, 14]); Console.Write(trollRUnnerLetters[5, 15]);
            Console.Write(trollRUnnerLetters[5, 16]); Console.Write(trollRUnnerLetters[5, 17]);
            Console.Write(trollRUnnerLetters[5, 18]); Console.Write(trollRUnnerLetters[5, 19]);
            Console.Write(trollRUnnerLetters[5, 20]); Console.Write(trollRUnnerLetters[5, 21]);
            Console.Write(trollRUnnerLetters[5, 22]); Console.Write(trollRUnnerLetters[5, 23]);
            Console.Write(trollRUnnerLetters[5, 24]); Console.Write(trollRUnnerLetters[5, 25]);
            Console.Write(trollRUnnerLetters[5, 26]); Console.Write(trollRUnnerLetters[5, 27]);
            Console.Write(trollRUnnerLetters[5, 28]); Console.Write(trollRUnnerLetters[5, 29]);
            Console.Write(trollRUnnerLetters[5, 30]); Console.Write(trollRUnnerLetters[5, 31]);
            Console.Write(trollRUnnerLetters[5, 32]); Console.Write(trollRUnnerLetters[5, 33]);
            Console.Write(trollRUnnerLetters[5, 34]); Console.Write(trollRUnnerLetters[5, 35]);
            Console.Write(trollRUnnerLetters[5, 36]); Console.Write(trollRUnnerLetters[5, 37]);
            Console.Write(trollRUnnerLetters[5, 38]); Console.Write(trollRUnnerLetters[5, 39]);
            Console.Write(trollRUnnerLetters[5, 40]); Console.Write(trollRUnnerLetters[5, 41]);
            Console.Write(trollRUnnerLetters[5, 42]); Console.Write(trollRUnnerLetters[5, 43]);
            Console.Write(trollRUnnerLetters[5, 44]); Console.Write(trollRUnnerLetters[5, 45]);
            Console.Write(trollRUnnerLetters[5, 46]); Console.Write(trollRUnnerLetters[5, 47]);
            Console.Write(trollRUnnerLetters[5, 48]); Console.Write(trollRUnnerLetters[5, 49]);
            Console.Write(trollRUnnerLetters[5, 50]); Console.Write(trollRUnnerLetters[5, 51]);
            Console.Write(trollRUnnerLetters[5, 52]); Console.Write(trollRUnnerLetters[5, 53]);
            Console.Write(trollRUnnerLetters[5, 54]); Console.Write(trollRUnnerLetters[5, 55]);
            Console.Write(trollRUnnerLetters[5, 56]); Console.Write(trollRUnnerLetters[5, 57]);
            Console.Write(trollRUnnerLetters[5, 58]); Console.Write(trollRUnnerLetters[5, 59]);
            Console.Write(trollRUnnerLetters[5, 60]); Console.Write(trollRUnnerLetters[5, 61]);
            Console.Write(trollRUnnerLetters[5, 62]); Console.Write(trollRUnnerLetters[5, 63]);
            Console.Write(trollRUnnerLetters[5, 64]); Console.Write(trollRUnnerLetters[5, 65]);
            Console.Write(trollRUnnerLetters[5, 66]); Console.Write(trollRUnnerLetters[5, 67]);
            Console.Write(trollRUnnerLetters[5, 68]); Console.Write(trollRUnnerLetters[5, 69]);
            Console.Write(trollRUnnerLetters[5, 70]); Console.Write(trollRUnnerLetters[5, 71]);
            Console.Write(trollRUnnerLetters[5, 72]); Console.Write(trollRUnnerLetters[5, 73]);
            Console.Write(trollRUnnerLetters[5, 74]); Console.Write(trollRUnnerLetters[5, 75]);
            Console.Write(trollRUnnerLetters[5, 76]); Console.Write(trollRUnnerLetters[5, 77]);
            Console.Write(trollRUnnerLetters[5, 78]); Console.Write(trollRUnnerLetters[5, 79]);
            Console.Write(trollRUnnerLetters[5, 80]); Console.Write(trollRUnnerLetters[5, 81]);
            Console.Write(trollRUnnerLetters[5, 82]); Console.Write(trollRUnnerLetters[5, 83]);
            Console.Write(trollRUnnerLetters[5, 84]); Console.Write(trollRUnnerLetters[5, 85]);
            Console.Write(trollRUnnerLetters[5, 86]); Console.Write(trollRUnnerLetters[5, 87]);
            Console.Write(trollRUnnerLetters[5, 88]); Console.Write(trollRUnnerLetters[5, 89]);
            Console.Write(trollRUnnerLetters[5, 90]); Console.WriteLine(trollRUnnerLetters[5, 91]);

            Console.Write(trollRUnnerLetters[6, 0]); Console.Write(trollRUnnerLetters[6, 1]);
            Console.Write(trollRUnnerLetters[6, 2]); Console.Write(trollRUnnerLetters[6, 3]);
            Console.Write(trollRUnnerLetters[6, 4]); Console.Write(trollRUnnerLetters[6, 5]);
            Console.Write(trollRUnnerLetters[6, 6]); Console.Write(trollRUnnerLetters[6, 7]);
            Console.Write(trollRUnnerLetters[6, 8]); Console.Write(trollRUnnerLetters[6, 9]);
            Console.Write(trollRUnnerLetters[6, 10]); Console.Write(trollRUnnerLetters[6, 11]);
            Console.Write(trollRUnnerLetters[6, 12]); Console.Write(trollRUnnerLetters[6, 13]);
            Console.Write(trollRUnnerLetters[6, 14]); Console.Write(trollRUnnerLetters[6, 15]);
            Console.Write(trollRUnnerLetters[6, 16]); Console.Write(trollRUnnerLetters[6, 17]);
            Console.Write(trollRUnnerLetters[6, 18]); Console.Write(trollRUnnerLetters[6, 19]);
            Console.Write(trollRUnnerLetters[6, 20]); Console.Write(trollRUnnerLetters[6, 21]);
            Console.Write(trollRUnnerLetters[6, 22]); Console.Write(trollRUnnerLetters[6, 23]);
            Console.Write(trollRUnnerLetters[6, 24]); Console.Write(trollRUnnerLetters[6, 25]);
            Console.Write(trollRUnnerLetters[6, 26]); Console.Write(trollRUnnerLetters[6, 27]);
            Console.Write(trollRUnnerLetters[6, 28]); Console.Write(trollRUnnerLetters[6, 29]);
            Console.Write(trollRUnnerLetters[6, 30]); Console.Write(trollRUnnerLetters[6, 31]);
            Console.Write(trollRUnnerLetters[6, 32]); Console.Write(trollRUnnerLetters[6, 33]);
            Console.Write(trollRUnnerLetters[6, 34]); Console.Write(trollRUnnerLetters[6, 35]);
            Console.Write(trollRUnnerLetters[6, 36]); Console.Write(trollRUnnerLetters[6, 37]);
            Console.Write(trollRUnnerLetters[6, 38]); Console.Write(trollRUnnerLetters[6, 39]);
            Console.Write(trollRUnnerLetters[6, 40]); Console.Write(trollRUnnerLetters[6, 41]);
            Console.Write(trollRUnnerLetters[6, 42]); Console.Write(trollRUnnerLetters[6, 43]);
            Console.Write(trollRUnnerLetters[6, 44]); Console.Write(trollRUnnerLetters[6, 45]);
            Console.Write(trollRUnnerLetters[6, 46]); Console.Write(trollRUnnerLetters[6, 47]);
            Console.Write(trollRUnnerLetters[6, 48]); Console.Write(trollRUnnerLetters[6, 49]);
            Console.Write(trollRUnnerLetters[6, 50]); Console.Write(trollRUnnerLetters[6, 51]);
            Console.Write(trollRUnnerLetters[6, 52]); Console.Write(trollRUnnerLetters[6, 53]);
            Console.Write(trollRUnnerLetters[6, 54]); Console.Write(trollRUnnerLetters[6, 55]);
            Console.Write(trollRUnnerLetters[6, 56]); Console.Write(trollRUnnerLetters[6, 57]);
            Console.Write(trollRUnnerLetters[6, 58]); Console.Write(trollRUnnerLetters[6, 59]);
            Console.Write(trollRUnnerLetters[6, 60]); Console.Write(trollRUnnerLetters[6, 61]);
            Console.Write(trollRUnnerLetters[6, 62]); Console.Write(trollRUnnerLetters[6, 63]);
            Console.Write(trollRUnnerLetters[6, 64]); Console.Write(trollRUnnerLetters[6, 65]);
            Console.Write(trollRUnnerLetters[6, 66]); Console.Write(trollRUnnerLetters[6, 67]);
            Console.Write(trollRUnnerLetters[6, 68]); Console.Write(trollRUnnerLetters[6, 69]);
            Console.Write(trollRUnnerLetters[6, 70]); Console.Write(trollRUnnerLetters[6, 71]);
            Console.Write(trollRUnnerLetters[6, 72]); Console.Write(trollRUnnerLetters[6, 73]);
            Console.Write(trollRUnnerLetters[6, 74]); Console.Write(trollRUnnerLetters[6, 75]);
            Console.Write(trollRUnnerLetters[6, 76]); Console.Write(trollRUnnerLetters[6, 77]);
            Console.Write(trollRUnnerLetters[6, 78]); Console.Write(trollRUnnerLetters[6, 79]);
            Console.Write(trollRUnnerLetters[6, 80]); Console.Write(trollRUnnerLetters[6, 81]);
            Console.Write(trollRUnnerLetters[6, 82]); Console.Write(trollRUnnerLetters[6, 83]);
            Console.Write(trollRUnnerLetters[6, 84]); Console.Write(trollRUnnerLetters[6, 85]);
            Console.Write(trollRUnnerLetters[6, 86]); Console.Write(trollRUnnerLetters[6, 87]);
            Console.Write(trollRUnnerLetters[6, 88]); Console.Write(trollRUnnerLetters[6, 89]);
            Console.Write(trollRUnnerLetters[6, 90]); Console.WriteLine(trollRUnnerLetters[6, 91]);

            Console.Write(trollRUnnerLetters[7, 0]); Console.Write(trollRUnnerLetters[7, 1]);
            Console.Write(trollRUnnerLetters[7, 2]); Console.Write(trollRUnnerLetters[7, 3]);
            Console.Write(trollRUnnerLetters[7, 4]); Console.Write(trollRUnnerLetters[7, 5]);
            Console.Write(trollRUnnerLetters[7, 6]); Console.Write(trollRUnnerLetters[7, 7]);
            Console.Write(trollRUnnerLetters[7, 8]); Console.Write(trollRUnnerLetters[7, 9]);
            Console.Write(trollRUnnerLetters[7, 10]); Console.Write(trollRUnnerLetters[7, 11]);
            Console.Write(trollRUnnerLetters[7, 12]); Console.Write(trollRUnnerLetters[7, 13]);
            Console.Write(trollRUnnerLetters[7, 14]); Console.Write(trollRUnnerLetters[7, 15]);
            Console.Write(trollRUnnerLetters[7, 16]); Console.Write(trollRUnnerLetters[7, 17]);
            Console.Write(trollRUnnerLetters[7, 18]); Console.Write(trollRUnnerLetters[7, 19]);
            Console.Write(trollRUnnerLetters[7, 20]); Console.Write(trollRUnnerLetters[7, 21]);
            Console.Write(trollRUnnerLetters[7, 22]); Console.Write(trollRUnnerLetters[7, 23]);
            Console.Write(trollRUnnerLetters[7, 24]); Console.Write(trollRUnnerLetters[7, 25]);
            Console.Write(trollRUnnerLetters[7, 26]); Console.Write(trollRUnnerLetters[7, 27]);
            Console.Write(trollRUnnerLetters[7, 28]); Console.Write(trollRUnnerLetters[7, 29]);
            Console.Write(trollRUnnerLetters[7, 30]); Console.Write(trollRUnnerLetters[7, 31]);
            Console.Write(trollRUnnerLetters[7, 32]); Console.Write(trollRUnnerLetters[7, 33]);
            Console.Write(trollRUnnerLetters[7, 34]); Console.Write(trollRUnnerLetters[7, 35]);
            Console.Write(trollRUnnerLetters[7, 36]); Console.Write(trollRUnnerLetters[7, 37]);
            Console.Write(trollRUnnerLetters[7, 38]); Console.Write(trollRUnnerLetters[7, 39]);
            Console.Write(trollRUnnerLetters[7, 40]); Console.Write(trollRUnnerLetters[7, 41]);
            Console.Write(trollRUnnerLetters[7, 42]); Console.Write(trollRUnnerLetters[7, 43]);
            Console.Write(trollRUnnerLetters[7, 44]); Console.Write(trollRUnnerLetters[7, 45]);
            Console.Write(trollRUnnerLetters[7, 46]); Console.Write(trollRUnnerLetters[7, 47]);
            Console.Write(trollRUnnerLetters[7, 48]); Console.Write(trollRUnnerLetters[7, 49]);
            Console.Write(trollRUnnerLetters[7, 50]); Console.Write(trollRUnnerLetters[7, 51]);
            Console.Write(trollRUnnerLetters[7, 52]); Console.Write(trollRUnnerLetters[7, 53]);
            Console.Write(trollRUnnerLetters[7, 54]); Console.Write(trollRUnnerLetters[7, 55]);
            Console.Write(trollRUnnerLetters[7, 56]); Console.Write(trollRUnnerLetters[7, 57]);
            Console.Write(trollRUnnerLetters[7, 58]); Console.Write(trollRUnnerLetters[7, 59]);
            Console.Write(trollRUnnerLetters[7, 60]); Console.Write(trollRUnnerLetters[7, 61]);
            Console.Write(trollRUnnerLetters[7, 62]); Console.Write(trollRUnnerLetters[7, 63]);
            Console.Write(trollRUnnerLetters[7, 64]); Console.Write(trollRUnnerLetters[7, 65]);
            Console.Write(trollRUnnerLetters[7, 66]); Console.Write(trollRUnnerLetters[7, 67]);
            Console.Write(trollRUnnerLetters[7, 68]); Console.Write(trollRUnnerLetters[7, 69]);
            Console.Write(trollRUnnerLetters[7, 70]); Console.Write(trollRUnnerLetters[7, 71]);
            Console.Write(trollRUnnerLetters[7, 72]); Console.Write(trollRUnnerLetters[7, 73]);
            Console.Write(trollRUnnerLetters[7, 74]); Console.Write(trollRUnnerLetters[7, 75]);
            Console.Write(trollRUnnerLetters[7, 76]); Console.Write(trollRUnnerLetters[7, 77]);
            Console.Write(trollRUnnerLetters[7, 78]); Console.Write(trollRUnnerLetters[7, 79]);
            Console.Write(trollRUnnerLetters[7, 80]); Console.Write(trollRUnnerLetters[7, 81]);
            Console.Write(trollRUnnerLetters[7, 82]); Console.Write(trollRUnnerLetters[7, 83]);
            Console.Write(trollRUnnerLetters[7, 84]); Console.Write(trollRUnnerLetters[7, 85]);
            Console.Write(trollRUnnerLetters[7, 86]); Console.Write(trollRUnnerLetters[7, 87]);
            Console.Write(trollRUnnerLetters[7, 88]); Console.Write(trollRUnnerLetters[7, 89]);
            Console.Write(trollRUnnerLetters[7, 90]); Console.WriteLine(trollRUnnerLetters[7, 91]);
            Console.WriteLine();
            string game = "THE GAME".PadLeft(50);
            Console.WriteLine(game);
            string[] raz = new string[92];
            for (int i = 0; i < raz.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Please enter player name: ".PadLeft(30));
            string playerName = Console.ReadLine();

        }
    }
}
