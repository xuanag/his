using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace his.Models
{
    public class Admission
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string AdmissionCode { get; set; } = string.Empty; // EmrNo
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? AdmissionDate { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string EmrTypeName { get; set; } = string.Empty;
        public string EmrTypeCode { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string HisEncounterId { get; set; } = string.Empty;
    }
}
