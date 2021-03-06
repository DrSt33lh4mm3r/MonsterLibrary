using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Collections.Generic;

using MonsterLibrary.Monsters.Model;
using MonsterLibrary.Monsters.Repositories;
using System.Threading.Tasks;

namespace MonsterLibrary.Monsters.Controllers 
{
    [ApiController]
    [Route("monsters")]
    public class MonsterController : ControllerBase
    {
        private readonly IMonstersRepository repository;

        public MonsterController(IMonstersRepository repository) 
        {
            this.repository = repository;
        }

        // GET /monsters
        [HttpGet]
        public async Task<IEnumerable<Monster>> GetMonstersAsync()
        {
            var monsters = (await repository.GetMonstersAsync()); 
            return monsters;
        }

        // GET /monsters/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<Monster>> GetMonsterAsync(string name)
        {
            var monster = await repository.GetMonsterAsync(name);

            if (monster is null)
            {
                return NotFound();
            }

            return Ok(monster);
        }

        // POST /monsters
        [HttpPost]
        public async Task<ActionResult<Monster>> CreateMonsterAsync(Monster newMonster)
        {
            Monster monster = newMonster with
            {
                Id = Guid.NewGuid()
            };

            await repository.CreateMonsterAsync(monster);

            return CreatedAtAction(nameof(GetMonsterAsync), new { name = monster.name}, monster);
        }
    }
}