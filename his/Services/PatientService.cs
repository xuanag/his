using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace his.Services
{
    public class PatientService : MongoService<Patient>, IPatientService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public PatientService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Patients")
        {
            _cache = cache;
        }

        public async Task<List<Patient>> GetAllAsync(string keyword = "")
        {
            var filter = string.IsNullOrEmpty(keyword)
                ? Builders<Patient>.Filter.Empty
                : Builders<Patient>.Filter.Or(
                    Builders<Patient>.Filter.Regex("FullName", new BsonRegularExpression(keyword, "i")),
                    Builders<Patient>.Filter.Regex("PatientCode", new BsonRegularExpression(keyword, "i")));

            return await _collection.Find(filter).SortByDescending(m => m.PatientCode).ToListAsync();
        }

        public async Task<string> GeneratePatientCodeAsync(string refix)
        {
            // Count patient in date.
            var today = DateTime.Today;
            var countToday = await _collection.CountDocumentsAsync(p =>
                p.AdmissionDate >= today && p.AdmissionDate < today.AddDays(1));

            var nextNumber = countToday + 1;
            var code = $"{refix}{DateTime.Now:yyMMdd}{nextNumber:D3}"; // sample: BN2404250001

            return code;
        }

        public async Task<Patient> GetByNoAsync(string no = "")
        {
            return await _collection.Find(x =>x.PatientCode.Equals(no)).FirstOrDefaultAsync();
        }
    }
}
