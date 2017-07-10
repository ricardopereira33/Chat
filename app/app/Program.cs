using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection c = new Connection();

            Thread thread = new Thread(new ThreadStart(c.Connect));

            thread.Start();
            thread.Join();
        }
    }
}
