using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace his.Models
{
    public class EmrDto
    {
        public string MEEmrNo { get; set; } = string.Empty; // AdmissionCode
        public string MEEmrDesc { get; set; } = string.Empty;
        public string AACreatedUser { get; set; } = string.Empty;
        public string MEPatientNo { get; set; } = string.Empty;
        public string MEPatientName { get; set; } = string.Empty;
        public string MEGender { get; set; } = string.Empty;
        public string MEPatientBirthday { get; set; } = string.Empty; //
        public string MEPatientContactCellPhone { get; set; } = string.Empty;
        public string MEPatientPermanentResidence { get; set; } = string.Empty;
        public string MEPatientContactAddressLine { get; set; } = string.Empty;
        public string MEPatientIDCard { get; set; } = string.Empty;
        public string MEPatientIDCardDate { get; set; } = string.Empty;
        public string MEMarital { get; set; } = string.Empty;
        public string MEPatientIDCardStateProvinces { get; set; } = string.Empty;
        public string FK_MEEmrTypeNo { get; set; } = string.Empty;
        public string FK_HRDepartmentNo { get; set; } = string.Empty;
        public bool MEPatientBirthdayOnlyYear { get; set; } = false;
        public bool AutoGenDocuments { get; set; } = false;
        public string MEEmrSourceNo { get; set; } = string.Empty;
    }
}
