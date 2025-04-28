using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public class AdmissionSequenceService : MongoService<AdmissionSequence>, IAdmissionSequenceService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public AdmissionSequenceService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "AdmissionSequences")
        {
            _cache = cache;
        }

        public async Task<string> GenerateAdmissionCodeAsync(string departmentCode)
        {
            var today = DateTime.UtcNow.ToString("yyyyMMdd");
            var id = $"{departmentCode}-{today}";

            var filter = Builders<AdmissionSequence>.Filter.Eq(s => s.Id, id);
            var update = Builders<AdmissionSequence>.Update.Inc(s => s.Sequence, 1);
            var options = new FindOneAndUpdateOptions<AdmissionSequence>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var sequenceDoc = await _collection.FindOneAndUpdateAsync(filter, update, options);

            string stt = sequenceDoc.Sequence.ToString("D4"); // 001, 002, ...
            return $"E.{today}{stt}";
        }
    }
}
