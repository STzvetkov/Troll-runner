using System;
using System.Collections.Generic;
using System.Threading;

class Collections
{
    static char[,] cloud;
    static int x = 2;
    static int y = 5;
    static int position = Console.WindowWidth - 1;
    static void Main()
    {
        cloud = new char[x, y];

        cloud[0, 0] = ' ';
        cloud[0, 1] = '.';
        cloud[0, 2] = '\'';
        cloud[0, 3] = ')';
        cloud[0, 4] = ' ';
        cloud[1, 0] = '(';
        cloud[1, 1] = '_';
        cloud[1, 2] = ' ';
        cloud[1, 3] = ' ';
        cloud[1, 4] = ')';

        while (true)
        {

            MoveCloud();
            Console.Clear();
            DrawCloud();

            Thread.Sleep(100);
        }
    }

    static void PrintOnPosition(int cloudRows, int cloudCols)
    {
        if (position > 3)
        {
            for (int row = 0; row < cloudRows; row++)
            {
                for (int col = 0; col < cloudCols; col++)
                {
                    Console.SetCursorPosition(col + position, row);
                    Console.Write(cloud[row, col]);
                }
            }
        }
        else if (position <= 3)
        {
            for (int row = 0; row < cloudRows; row++)
            {
                int moveCursor = 0;
                for (int col = 4; col >= cloudCols; col--)
                {
                    Console.SetCursorPosition(position - moveCursor, row);
                    Console.Write(cloud[row, col]);
                    moveCursor++;
                }
            }
        }
        if (position == 0)
        {
            Console.SetCursorPosition(0,1);
            Console.Write(' ');
        }
    }

    static void DrawCloud()
    {

        if (position == Console.WindowWidth - 4)
        {
            PrintOnPosition(x, y - 1);
        }
        else if (position == Console.WindowWidth - 3)
        {
            PrintOnPosition(x, y - 2);
        }
        else if (position == Console.WindowWidth - 2)
        {
            PrintOnPosition(x, y - 3);
        }
        else if (position == Console.WindowWidth - 1)
        {
            PrintOnPosition(x, y - 4);
        }
        else if (position == 3)
        {
            PrintOnPosition(x, 1);
        }
        else if (position == 2)
        {
            PrintOnPosition(x, 2);
        }
        else if (position == 1)
        {
            PrintOnPosition(x, 3);
        }
        else if (position == 0)
        {
            PrintOnPosition(x, 4);
        }
        else
        {
            PrintOnPosition(x, y);
        }


    }

    static void MoveCloud()
    {
        if (position > 0)
        {
            position--;
        }
    }
}