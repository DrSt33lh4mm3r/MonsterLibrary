using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MonsterLibrary.Monsters.Repositories;

namespace MonsterLibrary.MonsterBot
{
    public class MonsterBotService : IHostedService    
    {
        private string botKey;
        private IMonstersRepository monsterRepo;

        public MonsterBotService(string botKey, IMonstersRepository monsterRepo)
        {
            this.botKey = botKey;
            this.monsterRepo = monsterRepo;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bot = new MonsterBot(botKey, cancellationToken, this.monsterRepo);    

            return Task.CompletedTask;  
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
