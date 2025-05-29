using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty; // auto gen
        public string FullName { get; set; } = string.Empty;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public bool? Marital { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdCardNo { get; set; } = string.Empty;
        public string IdCardPlace { get; set; } = string.Empty;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? IdCardDate { get; set; }
        public string IssuranceNo { get; set; } = string.Empty;
        public string IssuranceDate { get; set; } = string.Empty;
        public string Nation { get; set; } = string.Empty; //Dan Toc
        public string NationCode { get; set; } = string.Empty; //Ma Dan Toc

        // Move to Admission.cs later
        // Emr
        public string AdmissionCode { get; set; } = string.Empty; // EmrNo
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? AdmissionDate { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string EmrTypeName { get; set; } = string.Empty;
        public string EmrTypeCode { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;

        // Extension fields

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Created { get; set; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Modified { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; } = string.Empty;
    }
}
