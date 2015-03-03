using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollRunner
{
    public class InvalidOperationException : System.Exception
    {
        public InvalidOperationException()
            : base()
        {

        }

        public InvalidOperationException(int rows, int cols)
            : base(String.Format("Invalid dimensions for the obstacle. Value of rows {0}, value of cols {1}", rows, cols))
        {

        }
    }
}
