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
            this.form = GraphicsManagement.GetGraphic("cloud1");
        }
    }
}
