using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public class DocumentMappingService : MongoService<DocumentMappingModel>, IDocumentMappingService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public DocumentMappingService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "DocumentMapping")
        {
            _cache = cache;
        }

        public Task<DocumentMappingModel> GetByCodeEMRAsync(string codeEMR)
        {
            return _collection.Find(x => x.CodeEMR == codeEMR).FirstOrDefaultAsync();
        }

        public Task<DocumentMappingModel> GetByCodeHISAsync(string codeHIS)
        {
            return _collection.Find(x => x.CodeHIS == codeHIS).FirstOrDefaultAsync();
        }
    }
}
