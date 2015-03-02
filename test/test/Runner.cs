using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Runner : Obstacle
    {
        public const int NumberOfRows = 3;

        public const int NumberOfCols = 3;

        public Runner(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
        }


        private void FillTroll()
        {
            this.form = GraphicsManagement.GetGraphic("Troll");

        }

        private void PrintTrollOnPosition(int x, int y)
        {
            Console.Write(this.form[3, 3]);
        }

        public void DrawTroll()
        {
            int rows = this.form.GetLength(0);
            int cols = this.form.GetLength(1);
            PrintOnPosition(rows, cols);
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
    

