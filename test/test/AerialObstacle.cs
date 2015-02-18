﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class AerialObstacle : Obstacle
    {
        private const int NumberOfRows = 2;
        private const int NumberOfCols = 5;

        public AerialObstacle(int x, int y)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            FillAerialObstacle();
        }

        private void FillAerialObstacle()
        {
            this.form[0, 0] = ' ';
            this.form[0, 1] = '.';
            this.form[0, 2] = '\'';
            this.form[0, 3] = ')';
            this.form[0, 4] = ' ';
            this.form[1, 0] = '(';
            this.form[1, 1] = '_';
            this.form[1, 2] = ' ';
            this.form[1, 3] = ' ';
            this.form[1, 4] = ')';
        }
    }
}