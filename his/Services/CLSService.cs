using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;

namespace his.Services
{
    public class CLSService : MongoService<CanLamSang>, ICLSService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public CLSService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "CanLamSang")
        {
            _cache = cache;
        }

        public async Task<List<CanLamSang>> SearchDichVuClsAsync(string keyword)
        {
            var filter = Builders<CanLamSang>.Filter.Regex("Ten", new BsonRegularExpression(keyword, "i"));
            return await _collection.Find(filter).Limit(10).ToListAsync();
        }

        public async Task<CanLamSang> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            // Try to get from cache first
            if (_cache.TryGetValue(code, out CanLamSang cachedItem))
            {
                return cachedItem;
            }
            // If not in cache, fetch from database
            var item = await _collection.Find(m => m.Ma.Equals(code)).FirstOrDefaultAsync();

            if (item != null)
            {
                // Set cache
                _cache.Set(code, item, CacheDuration);
            }
            return item;
        }

        public async Task SeedAsync()
        {
            var count = await _collection.CountDocumentsAsync(Builders<CanLamSang>.Filter.Empty);
            if (count > 0) return; // đã seed rồi

            var data = new List<CanLamSang>
            {
                // Chẩn đoán hình ảnh
                new() { Ma = "MRI001", Ten = "Chụp MRI sọ não", Nhom = "Chẩn đoán hình ảnh", DonGia = 1500000 },
                new() { Ma = "CT001", Ten = "Chụp CT Scan ngực", Nhom = "Chẩn đoán hình ảnh", DonGia = 1200000 },
                new() { Ma = "NS001", Ten = "Nội soi dạ dày", Nhom = "Chẩn đoán hình ảnh", DonGia = 500000 },

                // Huyết học
                new() { Ma = "HH001", Ten = "Xét nghiệm huyết học tổng quát", Nhom = "Huyết học", DonGia = 150000 },

                // Sinh hóa
                new() { Ma = "SH001", Ten = "Xét nghiệm sinh hóa máu", Nhom = "Sinh hóa", DonGia = 180000 },

                // Vi sinh
                new() { Ma = "VS001", Ten = "Xét nghiệm vi sinh nước tiểu", Nhom = "Vi sinh", DonGia = 200000 },

                // Thăm dò chức năng
                new() { Ma = "ECG001", Ten = "Điện tâm đồ ECG", Nhom = "Thăm dò chức năng", DonGia = 100000 },
                new() { Ma = "EEG001", Ten = "Điện não đồ EEG", Nhom = "Thăm dò chức năng", DonGia = 200000 }
            };

            await _collection.InsertManyAsync(data);
        }
    }
}
