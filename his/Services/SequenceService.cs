using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public class SequenceService : MongoService<SequenceModel>, ISequenceService
    {
        public SequenceService(IMongoDatabase database) : base(database, "Sequences")
        {
            // Đảm bảo chỉ mục duy nhất key + year
            var indexKeys = Builders<SequenceModel>.IndexKeys.Ascending(s => s.Key).Ascending(s => s.Year);
            var indexModel = new CreateIndexModel<SequenceModel>(indexKeys, new CreateIndexOptions { Unique = true });
            _collection.Indexes.CreateOne(indexModel);
        }

        public async Task<int> GetNextSequenceAsync(string key, int year)
        {
            var filter = Builders<SequenceModel>.Filter.Eq(s => s.Key, key) &
                     Builders<SequenceModel>.Filter.Eq(s => s.Year, year);

            var update = Builders<SequenceModel>.Update.Inc(s => s.Sequence, 1);
            var options = new FindOneAndUpdateOptions<SequenceModel>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync(filter, update, options);
            return result.Sequence;
        }
    }
}
