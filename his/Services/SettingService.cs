using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace his.Services
{
    public class SettingService : MongoService<Setting>, ISettingService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public SettingService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Settings")
        {
            _cache = cache;
        }

        public Task<Setting> GetByKey(string key)
        {
            return _collection.Find(m => m.Key.Equals(key)).FirstOrDefaultAsync();
        }
    }
}
