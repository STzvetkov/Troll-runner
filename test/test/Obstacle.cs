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
        public int X { get; private set; }

        public int Y { get; private set; }

        protected virtual void PrintOnPosition(int cloudRows, int cloudCols)
        {
            if (this.X > 3)
            {
                for (int row = 0; row < cloudRows; row++)
                {
                    for (int col = 0; col < cloudCols; col++)
                    {
                        Console.SetCursorPosition(col + this.X, row + this.Y);
                        Console.Write(this.form[row, col]);
                    }
                }
            }
            else if (this.X <= 3)
            {
                for (int row = 0; row < cloudRows; row++)
                {
                    int moveCursor = 0;
                    for (int col = 4; col >= cloudCols; col--)
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
            if (this.X == Console.WindowWidth - 4)
            {
                PrintOnPosition(rows, cols - 1);
            }
            else if (this.X == Console.WindowWidth - 3)
            {
                PrintOnPosition(rows, cols - 2);
            }
            else if (this.X == Console.WindowWidth - 2)
            {
                PrintOnPosition(rows, cols - 3);
            }
            else if (this.X == Console.WindowWidth - 1)
            {
                PrintOnPosition(rows, cols - 4);
            }
            else if (this.X == 3)
            {
                PrintOnPosition(rows, 1);
            }
            else if (this.X == 2)
            {
                PrintOnPosition(rows, 2);
            }
            else if (this.X == 1)
            {
                PrintOnPosition(rows, 3);
            }
            else if (this.X == 0)
            {
                PrintOnPosition(rows, 4);
            }
            else
            {
                PrintOnPosition(rows, cols);
            }
        }

        public void MoveObstacle()
        {
            if (this.X > 0)
            {
                this.X--;
            }
        }
    }
}
