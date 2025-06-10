using Amazon.Runtime.Internal.Util;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Runtime.InteropServices;

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
                new() { Ma = "ngoai_tru", Ten = "Cận lâm sàng (Ngoại trú)", Nhom = "Chung", DonGia = 0 },
                new() { Ma = "noi_tru", Ten = "Cận lâm sàng (Nội trú)", Nhom = "Chung", DonGia = 0 },
                new() { Ma = "san_loc_ngoai_tru", Ten = "Cận lâm sàng sàng lọc trước sinh", Nhom = "Chung", DonGia = 0 },
                new() { Ma = "sieu_am", Ten = "Siêu âm", Nhom = "Chẩn đoán hình ảnh", DonGia = 100000 },
                new() { Ma = "x_quang", Ten = "X quang", Nhom = "Chẩn đoán hình ảnh", DonGia = 120000 },
                new() { Ma = "ct_scanner", Ten = "CT Scanner", Nhom = "Chẩn đoán hình ảnh", DonGia = 1200000 },
                new() { Ma = "xet_nghiem", Ten = "Xét nghiệm tổng hợp", Nhom = "Xét nghiệm", DonGia = 200000 },
                new() { Ma = "mri", Ten = "MRI", Nhom = "Chẩn đoán hình ảnh", DonGia = 2000000 },
                new() { Ma = "huyet_hoc", Ten = "Xét nghiệm Huyết học", Nhom = "Xét nghiệm", DonGia = 60000 },
                new() { Ma = "dong_mau", Ten = "Xét nghiệm Đông máu", Nhom = "Xét nghiệm", DonGia = 80000 },
                new() { Ma = "nhom_mau", Ten = "Xét nghiệm Nhóm máu", Nhom = "Xét nghiệm", DonGia = 45000 },
                new() { Ma = "hoa_sinh_mau", Ten = "Hóa sinh máu", Nhom = "Xét nghiệm", DonGia = 100000 },
                new() { Ma = "nong_do_con", Ten = "Xét nghiệm Nồng độ cồn", Nhom = "Xét nghiệm", DonGia = 70000 },
                new() { Ma = "hoa_sinh_nuoc_tieu", Ten = "Hóa sinh nước tiểu", Nhom = "Xét nghiệm", DonGia = 50000 },
                new() { Ma = "hoa_sinh", Ten = "Hóa sinh", Nhom = "Xét nghiệm", DonGia = 80000 },
                new() { Ma = "mien_dich", Ten = "Xét nghiệm Miễn dịch", Nhom = "Xét nghiệm", DonGia = 150000 },
                new() { Ma = "vi_sinh", Ten = "Xét nghiệm Vi sinh", Nhom = "Xét nghiệm", DonGia = 130000 },
                new() { Ma = "giai_phau_benh", Ten = "Giải phẫu bệnh", Nhom = "Giải phẫu bệnh", DonGia = 300000 },
                new() { Ma = "noi_soi", Ten = "Nội soi", Nhom = "Nội soi", DonGia = 800000 },
                new() { Ma = "dien_tim", Ten = "Điện tim", Nhom = "Thăm dò chức năng", DonGia = 100000 },
                new() { Ma = "dien_nao", Ten = "Điện não", Nhom = "Thăm dò chức năng", DonGia = 200000 },
                new() { Ma = "dien_co", Ten = "Điện cơ", Nhom = "Thăm dò chức năng", DonGia = 250000 },
                new() { Ma = "ho_hap_khi", Ten = "Hô hấp khí", Nhom = "Thăm dò chức năng", DonGia = 90000 },
                new() { Ma = "loang_xuong", Ten = "Đo loãng xương", Nhom = "Chẩn đoán hình ảnh", DonGia = 300000 },
                new() { Ma = "can_lam_sang_khac", Ten = "Cận lâm sàng khác", Nhom = "Khác", DonGia = 100000 },
                new() { Ma = "doppler", Ten = "Siêu âm Doppler", Nhom = "Chẩn đoán hình ảnh", DonGia = 350000 },
                new() { Ma = "holter_ecg", Ten = "Holter ECG", Nhom = "Thăm dò chức năng", DonGia = 600000 }
            };

            await _collection.InsertManyAsync(data);
        }
    }
}
