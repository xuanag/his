using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public class AdmissionService : MongoService<Admission>, IAdmissionService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public AdmissionService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Admissions")
        {
            _cache = cache;
        }

        public async Task<List<Admission>> GetByPatientIdAsync(string patientId) =>
            await _collection.Find(x => x.PatientId == patientId)
                             .SortByDescending(x => x.AdmissionDate)
                             .ToListAsync();
    }
}
