using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public class PhieuChiDinhService : MongoService<PhieuChiDinhCls>, IPhieuChiDinhService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public PhieuChiDinhService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "PhieuChiDinhCls")
        {
            _cache = cache;
        }

    }
}
