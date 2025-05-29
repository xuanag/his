using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace his.Services
{
    public class MongoService<T> : IMongoService<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoService(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)), entity);
            return result.IsAcknowledged && result.ModifiedCount > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
