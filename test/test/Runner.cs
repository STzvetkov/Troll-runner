using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Runner : Obstacle
    {
        public const int NumberOfRows = 2;

        public const int NumberOfCols = 2;
        public char C { get; set; }

        public Runner(int x, int y, char c)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            this.C = 'O';

        }


        private void FillLandObstacle()
        {

        }

        private void PrintTrollOnPosition(int x, int y, char c)
        {

            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(c);

        }


        public void Jump()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Y++;
            }
        }
    }
}
    

