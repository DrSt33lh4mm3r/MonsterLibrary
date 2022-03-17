using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonsterLibrary.MonsterBot;

namespace MonsterLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Configuration Source -> Environment Variables -> maybe something like .env file?  
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<MonsterBotService>(serviceProvider => 
            {
                string telegramBotKey = Configuration.GetValue(typeof(string), "TELEGRAM_BOT_KEY") as string;
                return new MonsterBotService(telegramBotKey);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }
        }
    }
}