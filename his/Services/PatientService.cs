using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace his.Services
{
    public class PatientService : MongoService<PatientInfo>, IPatientService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public PatientService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Patients")
        {
            _cache = cache;
        }

        public async Task<List<PatientInfo>> GetAllAsync(string keyword = "")
        {
            var filter = string.IsNullOrEmpty(keyword)
                ? Builders<PatientInfo>.Filter.Empty
                : Builders<PatientInfo>.Filter.Or(
                    Builders<PatientInfo>.Filter.Regex("FullName", new BsonRegularExpression(keyword, "i")),
                    Builders<PatientInfo>.Filter.Regex("PatientCode", new BsonRegularExpression(keyword, "i")));

            return await _collection.Find(filter).ToListAsync();
        }
    }
}
