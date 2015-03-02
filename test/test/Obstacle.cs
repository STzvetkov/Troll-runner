using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public abstract class Obstacle
    {
        protected char[,] form;

        public Obstacle(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; protected set; }

        public int Y { get; protected set; }

        protected virtual void PrintOnPosition(int obstacleRows, int obstacleCols)
        {
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
        }

        public virtual void DrawObstacle()
        {
            int rows = this.form.GetLength(0);
            int cols = this.form.GetLength(1);
            if ((this.X >= Console.WindowWidth - 4) && (this.X <= Console.WindowWidth - 1))
            {
                PrintOnPosition(rows, cols - (5 - Console.WindowWidth + this.X));
            }
            else if ((this.X >= 0) && (this.X <= 3))
            {
                PrintOnPosition(rows, 4 - this.X);
            }
            else
            {
                PrintOnPosition(rows, cols);
            }
        }

        public void MoveObstacle(int speed)
        {
            if (this.X > 0)
            {
                this.X -= speed;
            }
        }
    }
}