using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MonsterLibrary.Monsters.Model;
using Microsoft.Extensions.Configuration;


namespace MonsterLibrary.Monsters.Repositories
{
    class MongoMonstersRepository : IMonstersRepository
    {
        private const string COLLECTION_NAME = "Monsters";

        private readonly FilterDefinitionBuilder<Monster> filterBuilder = Builders<Monster>.Filter;
        private readonly IMongoCollection<Monster> monstersCollection;

        public MongoMonstersRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            string databaseName = configuration.GetValue(typeof(string), "DATBASE_NAME") as string;

            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            monstersCollection = database.GetCollection<Monster>(COLLECTION_NAME);
        }

        public async Task CreateMonsterAsync(Monster monster)
        {
            await monstersCollection.InsertOneAsync(monster);
        }

        public async Task DeleteMonsterAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingMonster => existingMonster.Id, id);
            await monstersCollection.DeleteOneAsync(filter);
        }

        public async Task<Monster> GetMonsterAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingMonster => existingMonster.Id, id);
            return await monstersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Monster>> GetMonstersAsync()
        {
            return await monstersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateMonsterAsync(Monster monster)
        {
            var filter = filterBuilder.Eq(existingMonster => existingMonster.Id, monster.Id);
            await monstersCollection.ReplaceOneAsync(filter, monster);
        }
    }
}