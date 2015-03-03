using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class Path
    {
        public Path()
        {
        }
        
        public void DrawPath()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight - 2);
                Console.Write('.');
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
