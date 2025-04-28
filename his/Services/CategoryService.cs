using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace his.Services
{
    public class CategoryService : MongoService<Category>, ICategoryService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public CategoryService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Categories")
        {
            _cache = cache;
        }

        public Task<List<Category>> CategoriesByType(string type)
        {
            return _collection.Find(m => m.Type.Equals(type)).ToListAsync();
        }
    }
}
