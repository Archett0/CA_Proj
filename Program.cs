using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CA_Proj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Start hosts");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
