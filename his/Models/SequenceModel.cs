using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    // use gen EMRNO, AdmissionCode, etc.
    // Rule: 1 year, 1 sequence
    // Sample: 2023.00001, 2023.00002, ... (year.sequence)
    public class SequenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Key { get; set; } = string.Empty; // extension for type, e.g., "EMRNO", "AdmissionCode", etc.
        public int Year { get; set; }
        public int Sequence { get; set; }
    }
}
