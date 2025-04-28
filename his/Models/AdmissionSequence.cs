using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace his.Models
{
    public class AdmissionSequence
    {
        public string Id { get; set; } = string.Empty; // Format: NOI-20250426
        public int Sequence { get; set; }
    }
}
