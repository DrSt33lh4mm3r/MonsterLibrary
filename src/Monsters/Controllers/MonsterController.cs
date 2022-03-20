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
        public async Task<IEnumerable<Monster>> GetPeopleAsync()
        {
            var monsters = (await repository.GetMonstersAsync()); 
            return monsters;
        }

        // GET /monsters/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Monster>> GetPersonAsync(Guid id)
        {
            var monster = await repository.GetMonsterAsync(id);

            if (monster is null)
            {
                return NotFound();
            }

            return Ok(monster);
        }

        // POST /monsters
        [HttpPost]
        public async Task<ActionResult<Monster>> CreatePersonAsync(Monster newMonster)
        {
            Monster monster = newMonster with
            {
                Id = Guid.NewGuid()
            };

            await repository.CreateMonsterAsync(monster);

            return CreatedAtAction(nameof(GetPersonAsync), new { id = monster.Id}, monster);
        }
    }
}