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

        private const int MaxJumpHeight = 10;

        private bool hasJumped = false;
        private bool doubleJump = false;
        private int jumpHeight = 0;

        public Runner(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfCols, NumberOfRows];
            FillTroll();
        }


        private void FillTroll()
        {
            this.form = GraphicsManagement.GetGraphic("Troll");

        }

        private void PrintTrollOnPosition(int x, int y)
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfCols; col++)
                {
                    Console.SetCursorPosition(col + this.X, this.Y - row);
                    Console.Write(this.form[row, col]);
                }
            }
        }

        public void DrawTroll()
        {
            PrintTrollOnPosition(NumberOfRows, NumberOfCols);
        }


        private void MoveUp()
        {
            //if (this.Y > JumpHeight)
            //{
            this.Y--;
            this.jumpHeight++;
            //}  
        }

        private void MoveDown()
        {
            //if (this.Y < Start)
            //{
            this.Y++;
            this.jumpHeight--;
            //}
        }

        public void Move()
        {
            if (this.hasJumped)
            {
                if (this.jumpHeight < MaxJumpHeight && !doubleJump)
                {
                    this.MoveUp();
                }
                else if (this.jumpHeight == MaxJumpHeight || doubleJump)
                {
                    this.hasJumped = false;
                }
            }
            else
            {
                if (this.jumpHeight > 0)
                {
                    this.MoveDown();
                }
                else
                {
                    this.doubleJump = false;
                }
            }
        }

        public void Jump()
        {
            if (this.hasJumped)
            {
                this.doubleJump = true;
            }
            else
            {
                this.hasJumped = true;
            }
        }

        
    }
}
    

