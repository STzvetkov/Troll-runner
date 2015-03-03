using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class AerialObstacle : Obstacle
    {
        public const int NumberOfRows = 2;
        public const int NumberOfCols = 5;

        public AerialObstacle(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            FillAerialObstacle();
        }

        private void FillAerialObstacle()
        {
            if (GraphicsManagement.GetGraphic("cloud1").GetLength(0) != NumberOfRows ||
                GraphicsManagement.GetGraphic("cloud1").GetLength(1) != NumberOfCols)
            {
                throw new InvalidOperationException(GraphicsManagement.GetGraphic("cloud1").GetLength(0)
                    , GraphicsManagement.GetGraphic("cloud1").GetLength(1));
            }
            else
            {
                this.form = GraphicsManagement.GetGraphic("cloud1");
            }
        }

        protected override void PrintOnPosition(int obstacleRows, int obstacleCols)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (this.X > 3)
            {
                for (int row = 0; row < obstacleRows; row++)
                {
                    for (int col = 0; col < obstacleCols; col++)
                    {
                        Console.SetCursorPosition(col + this.X, row + this.Y);
                        Console.Write(this.form[row, col]);
                    }
                }
            }
            else if (this.X <= 3)
            {
                for (int row = 0; row < obstacleRows; row++)
                {
                    int moveCursor = 0;
                    for (int col = 4; col >= obstacleCols; col--)
                    {
                        Console.SetCursorPosition(this.X - moveCursor, row + this.Y);
                        Console.Write(this.form[row, col]);
                        moveCursor++;
                    }
                }
            }
            if (this.X == 0)
            {
                Console.SetCursorPosition(0, this.Y + 1);
                Console.Write(' ');
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
