using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game60s
{
    public static class Program
    {
        public static void Main()
        {
            object x = 1;
            if  (x is var x2)
            {
                Console.WriteLine(x2);
            }
        }
    }
}
