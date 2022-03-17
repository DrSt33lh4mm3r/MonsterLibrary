using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MonsterLibrary.MonsterBot
{
    public class MonsterBotService : IHostedService    
    {
        private string botKey;

        public MonsterBotService(string botKey)
        {
            this.botKey = botKey;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bot = new MonsterBot(botKey, cancellationToken);    

            return Task.CompletedTask;  
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
