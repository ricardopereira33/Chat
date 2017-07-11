using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ChatRoom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostUrl = "http://0.0.0.0:3333";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl)
				.UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
