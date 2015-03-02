using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Runner : Obstacle
    {

        public char C { get; set; }

        protected char[,] form;

        public Runner(int x, int y, char c)
            : base(x, y)
        {

            this.C = 'O';

        }


        private void FillLandObstacle()
        {

        }

        protected virtual void PrintTrollOnPosition(int x, int y, char c)
        {

            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(c);

        }


        protected virtual void Jump()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Y++;
            }
        }
    }
}
    

