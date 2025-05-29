using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class DichVuChiDinh
    {
        public string Ma { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string? GhiChu { get; set; }
    }

    public class PhieuChiDinhCls
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string BenhNhanId { get; set; } = null!;
        public string mabenhan { get; set; } = null!;
        public DateTime NgayChiDinh { get; set; } = DateTime.UtcNow;

        public List<DichVuChiDinh> DichVu { get; set; } = new();
    }
}
