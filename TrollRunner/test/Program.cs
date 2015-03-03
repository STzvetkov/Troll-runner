using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace TrollRunner
{
    class Game  
    {
        public const int MinSpeed = 1;
        public const int MaxSpeed = 3;
        public const int InitialLifes = 3;
        public const int SleepTime = 50;
        public const int StartScreenWidth = 92;
        public const int StartScreenHeight = 8;
        public const int GameOverScreenWidth = 68;
        public const int GameOverScreenHeight = 8;

        public static string playerName;
        public static int gameSpeed = MinSpeed;
        public static int acceleration = 0;
        public static int currentLifes = InitialLifes;
        public static bool ableToFire = false;
        public static Runner troll;
        static int currentScore = 0;
        static public StreamReader reader = new StreamReader(@"..\..\data\HighScores.txt");
        static public int highScore = int.Parse(reader.ReadLine());

        public static Random generator = new Random();
       
        static void Main()
        {
            Console.Title = ("Troll runner");            

            GraphicsManagement.InitializeGraphics();  // Load graphics from the corresponding files
            
            List<AerialObstacle> cloudsContainer = new List<AerialObstacle>();
            List<LandObstacle> trapsContainer = new List<LandObstacle>();
            List<Pickup> pickupContainer = new List<Pickup>();
            troll = new Runner(10, 20);
            Path trollPath = new Path();
            SetFieldSize();
            int distanceBetweenObstacles = 0;
            int distanceBetweenPickups = 0;
            bool playing = true;

            Start();
            
            while (playing)
            {
                int chance = generator.Next(1, 201);

                if (chance >= 30 && chance <= 32 && distanceBetweenPickups > 5)
                {
                    switch (chance)
                    {
                        case 30:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Fire));
                            break;
                        case 31:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Life));
                            break;
                        case 32:
                            pickupContainer.Add(new Pickup(Console.WindowWidth - 1, 8, Pickup.PickupType.Slow));
                            break;
                    }
                    distanceBetweenPickups = 0;
                }

                if (chance < 20)
                {
                    int secondChance = generator.Next(0, 4);
                    cloudsContainer.Add(new AerialObstacle(Console.WindowWidth - 1, secondChance));
                }

                if (chance < 20 && distanceBetweenObstacles > 10)
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

                troll.Move();
               
                Console.Clear();
               
                Score(currentScore);
                trollPath.DrawPath();
                troll.DrawTroll();
                
                for (int i = 0; i < cloudsContainer.Count; i++)
                {
                    cloudsContainer[i].DrawObstacle();

                    if (cloudsContainer[i].X == 0)
                        cloudsContainer.RemoveAt(i);
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
                        * 
                        }*/
                    }
                    else
                    {
                        pickupContainer[i].Expire();
                    }
                }

                ManageInput(playing);

                int pickupIndex = DetectCollisionWithPickup(pickupContainer, troll);
                if (pickupIndex >= 0)
                {
                    pickupContainer[pickupIndex].Activate();
                }
                int trapIndex = DetectCollisionWithTrap(trapsContainer, troll);
                if (trapIndex >= 0)
                {
                    currentLifes--;
                    trapsContainer.RemoveAt(trapIndex);
                    playing = currentLifes > 0;
                    if (!playing)
                        Gameover();
                }

                distanceBetweenPickups++;
                distanceBetweenObstacles++;
                acceleration += SleepTime;
                gameSpeed = Math.Min(gameSpeed + acceleration / (SleepTime*1000), MaxSpeed);
                currentScore += 10;
                Thread.Sleep(SleepTime);             
            }

            Console.CursorVisible = false;
        }

        static void Score(int result)
        {
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write(" Score: {0}", result);

            Console.SetCursorPosition((17 - highScore.ToString().Length - 1), Console.BufferHeight - 1);
            Console.Write("/ {0}", highScore);

            Console.SetCursorPosition((62 - highScore.ToString().Length - 1), Console.BufferHeight - 1);
            Console.Write("Press ESC to pause");

            Console.SetCursorPosition((89 - highScore.ToString().Length - 1), Console.BufferHeight - 1);
            Console.Write("Lives: {0}", currentLifes);

            Console.SetCursorPosition((108 - highScore.ToString().Length - 1), Console.BufferHeight - 1);
            Console.Write("Player: {0}", playerName);
        }

        static void ManageInput(bool play)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo press = Console.ReadKey(true);
                switch (press.Key)
                {
                    case ConsoleKey.Escape:
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
                        break;
                    case ConsoleKey.Spacebar:
                        troll.Jump();
                        break;
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
            char[,] trollRunnerLetters;
            trollRunnerLetters = GraphicsManagement.GetGraphic("startmenu");
            int shift = (Console.WindowWidth - StartScreenWidth) / 2;
            for (int row = 0; row < StartScreenHeight; row++)
            {
                for (int col = 0; col < StartScreenWidth; col++)
                { 
                    Console.SetCursorPosition(col + shift, row);
                    Console.Write(trollRunnerLetters[row, col]);
                }
            }
           
            Console.WriteLine();
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            string game = "THE GAME".PadLeft(Console.WindowWidth / 2);
            Console.WriteLine(game);
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Please enter player name: ".PadLeft(30));
            playerName = Console.ReadLine();
        }

        static void Gameover()
        {
            Console.Clear();
            char[,] gameover;
            gameover = GraphicsManagement.GetGraphic("gameover");
            int shift = (Console.WindowWidth - GameOverScreenWidth) / 2;
            for (int row = 0; row < GameOverScreenHeight; row++)
            {
                for (int col = 0; col < GameOverScreenWidth; col++)
                {
                    Console.SetCursorPosition(col + shift, row + 2);
                    Console.Write(gameover[row, col]);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            HighScore();
            
        }
        static void HighScore()
        {

            reader.Close();
            
            Console.SetCursorPosition(21, 12);
            if (currentScore >= highScore)
            {
                var writer = new StreamWriter(@"..\..\data\HighScores.txt");           
                Console.WriteLine("Congratulations {0} you set a new highscore of:{1} points", playerName, currentScore);
                writer.WriteLine(currentScore);
                writer.Close();
            }
            else
            {                
                Console.WriteLine("{0} you were unable to beat the current highscore! Your score is:{1} points", playerName, currentScore);
            }
            
            Console.ReadLine();
        }

        public static int DetectCollisionWithTrap(List<LandObstacle> traps, Runner troll)
        {
            int index = 0;
            foreach (LandObstacle trap in traps)
            {
                if ((troll.X + Runner.NumberOfCols - 1 >= trap.X) &&
                    (troll.X <= trap.X + LandObstacle.NumberOfCols - 1) &&
                    (troll.Y >= trap.Y - LandObstacle.NumberOfRows + 1))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int DetectCollisionWithPickup(List<Pickup> pickups, Runner troll)
        {
            int index = 0;
            foreach (Pickup bonus in pickups)
            {
                if ((troll.X + Runner.NumberOfCols - 1 >= bonus.X) &&
                    (troll.X <= bonus.X + Pickup.NumberOfCols - 1) &&
                    ((troll.Y - bonus.Y <= Runner.NumberOfRows - 1) ||
                     (bonus.Y - troll.Y <= 0)) &&
                    !bonus.IsActive)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}