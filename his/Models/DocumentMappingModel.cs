using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class DocumentMappingModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string CodeHIS { get; set; } = default!;
        public string NameHIS { get; set; } = default!;
        public string CodeEMR { get; set; } = default!; // map Template
        public string NameEMR { get; set; } = default!;
        public string Type { get; set; } = default!; // filter, search...
        public bool IsActive { get; set; } = true;
    }
}
