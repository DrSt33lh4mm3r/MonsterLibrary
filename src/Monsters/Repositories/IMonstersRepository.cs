using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterLibrary.Monsters.Model;

namespace MonsterLibrary.Monsters.Repositories
{
    public interface IMonstersRepository
    {
        Task<IEnumerable<Monster>> GetMonstersAsync();
        
        Task<Monster> GetMonsterAsync(string name);

        Task CreateMonsterAsync(Monster monster);

        Task UpdateMonsterAsync(Monster monster);

        Task DeleteMonsterAsync(Guid id);
    }
}