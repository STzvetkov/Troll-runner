using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class Runner : Obstacle
    {
        public const int NumberOfRows = 3;

        public const int NumberOfCols = 3;

        private const int MaxJumpHeight = 12;
        private const int MaxJumpStage = 7;

        private bool hasJumped = false;
        private bool isFalling = false;
        private int jumpHeight = 0;
        private int jumpStage;

        public Runner(int x, int y) : base(x, y)
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
            this.jumpStage++;
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
            if (this.hasJumped && !this.isFalling)
            {
                if (this.jumpHeight < MaxJumpHeight && this.jumpStage < MaxJumpStage)
                {
                    this.MoveUp();
                }
                else
                {
                    this.isFalling = true;
                    this.jumpStage = 0;
                }
            }
            else
            {
                if (this.isFalling && this.jumpHeight > 0)
                {
                    this.MoveDown();
                }
                else if (this.jumpHeight == 0)
                {
                    this.isFalling = false;
                    this.hasJumped = false;
                }
            }
        }

        public void Jump()
        {
            if (this.hasJumped)
            {
                this.jumpStage = 0;
            }
            else
            {
                this.hasJumped = true;
                this.jumpStage = 0;
            }
        }
    }
}