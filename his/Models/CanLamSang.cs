using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class CanLamSang
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Ten { get; set; }
        public string Nhom { get; set; }
        public string Ma { get; set; }
        public int DonGia { get; set; }
        public string DonVi { get; set; }
        public string MoTa { get; set; }
        public List<string> ChiSoLienQuan { get; set; } = new();
        public bool CoBaoHiem { get; set; }
        public bool Active { get; set; } = true;
    }
}
