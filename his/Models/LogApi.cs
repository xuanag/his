using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace his.Models
{
    public class LogApi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // IN/OUT

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Created { get; set; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; } = string.Empty;
    }
}
