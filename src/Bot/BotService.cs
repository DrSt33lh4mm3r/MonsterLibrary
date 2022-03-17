using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MonsterLibrary.Bot
{
    public class BotService : IHostedService    
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bot = new MonsterBot(cancellationToken);    

            return Task.CompletedTask;  
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
