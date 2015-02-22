using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class LandObstacle : Obstacle
    {
        private const int NumberOfRows = 2;

        private const int NumberOfCols = 2;

        public LandObstacle(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            FillLandObstacle();
        }

        private void FillLandObstacle()
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfCols; col++)
                {
                    if (row == 0)
                    {
                        form[row, col] = '|';
                    }
                    else
                    {
                        form[row, col] = '^';
                    }
                }
            }
        }

        protected override void PrintOnPosition(int trapRows, int trapCols)
        {
            if (this.X > 1)
            {
                for (int row = 0; row < trapRows; row++)
                {
                    for (int col = 0; col < trapCols; col++)
                    {
                        Console.SetCursorPosition(col + this.X, this.Y - row);
                        Console.Write(this.form[row, col]);
                    }
                }
            }
            else if (this.X == 0)
            {
                Console.SetCursorPosition(0, this.Y + 1);
                Console.Write(' ');
            }
        }

        public override void DrawObstacle()
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
            else if (this.X == 0)
            {
                PrintOnPosition(rows, 1);
            }
            else
            {
                PrintOnPosition(rows, cols);
            }
        }
    }
}



//char[,] box = new char[2, 3];

//box[0, 0] = '^';
//box[0, 1] = '^';
//box[0, 2] = '^';
//box[1, 0] = '|';
//box[1, 1] = '|';
//box[1, 2] = '|';

//for (int i = 0; i < 2; i++)
//{
//    for (int j = 0; j < 3; j++)
//    {
//        Console.Write(box[i,j]);
//    }
//    Console.WriteLine();
//}