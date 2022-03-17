using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MonsterLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.ReadEnv(".env");
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
