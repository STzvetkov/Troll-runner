using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class LandObstacle : Obstacle
    {
        public const int NumberOfRows = 2;

        public const int NumberOfCols = 2;

        public LandObstacle(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            FillLandObstacle();
        }

        private void FillLandObstacle()
        {
            if (GraphicsManagement.GetGraphic("obstacle1").GetLength(0) != NumberOfRows ||
                GraphicsManagement.GetGraphic("obstacle1").GetLength(1) != NumberOfCols)
            {
                throw new InvalidOperationException(GraphicsManagement.GetGraphic("obstacle1").GetLength(0)
                    , GraphicsManagement.GetGraphic("obstacle1").GetLength(1));
            }
            else
            {
                this.form = GraphicsManagement.GetGraphic("obstacle1");
            }
            
        }

        protected override void PrintOnPosition(int trapRows, int trapCols)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
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
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override void DrawObstacle()
        {
            int rows = this.form.GetLength(0);
            int cols = this.form.GetLength(1);
            PrintOnPosition(rows, cols);
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