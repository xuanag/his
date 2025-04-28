using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // department, typeEMR,...

        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public string InitAction { get; set; } = string.Empty; // department create init typeEMR,...

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Created { get; set; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; } = string.Empty;
    }
}
