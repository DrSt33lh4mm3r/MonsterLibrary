using Microsoft.Extensions.Configuration;

namespace MonsterLibrary.Configuration
{
    public class MongoConfiguration
    {
        private IConfiguration configuration;

        private string host;
        private int port;
        private string user;
        private string password;

        public MongoConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.host = configuration.GetValue(typeof(string), "MONGO_HOST") as string;
            this.port = int.Parse(configuration.GetValue(typeof(string), "MONGO_PORT") as string);
            this.user = configuration.GetValue(typeof(string), "MONGO_USER") as string;
            this.password = configuration.GetValue(typeof(string), "MONGO_PASSWORD") as string;
        }

        public string ConnectionString
        {
            get
            {
                return $"mongodb://{this.user}:{this.password}@{this.host}:{this.port}";
                // return $"mongodb://{this.host}:{this.port}";
            }
        }
    }
}