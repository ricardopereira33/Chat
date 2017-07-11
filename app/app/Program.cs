using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = "localhost";   // update me
			builder.UserID = "sa";              // update me
			builder.Password = "Idryl33.";      // update me
			builder.InitialCatalog = "chat";

            Console.WriteLine(builder.ToString());


			Connection c = new Connection();

            Thread thread = new Thread(new ThreadStart(c.Connect));

            thread.Start();
            thread.Join();
        }
    }
}
