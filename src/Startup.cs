using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonsterLibrary.MonsterBot;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using MonsterLibrary.Configuration;
using MongoDB.Driver;
using MonsterLibrary.Monsters.Repositories;
using Microsoft.OpenApi.Models;

namespace MonsterLibrary
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private MongoConfiguration mongoConfiguration;
        public Startup(IConfiguration configuration)
        {
            // Configuration Source -> Environment Variables -> maybe something like .env file?  
            Configuration = configuration;
            mongoConfiguration = new MongoConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                return new MongoClient(mongoConfiguration.ConnectionString);
            });

            services.AddSingleton<IMonstersRepository, MongoMonstersRepository>();

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonsterLibrary", Version = "v1" });
            });

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
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonsterLibrary v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}