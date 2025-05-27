namespace his.Models.Dto
{
    public class DocumentDto
    {
        public string FK_MEEmrNo { get; set; }
        public string FK_METemplateNo { get; set; }
        public string MEEmrDocumentCode { get; set; }
        public string Old_MEEmrDocumentCode { get; set; }
        public string MEEmrDocumentDesc { get; set; }
        public string FK_HRDepartmentNo { get; set; }
        public Newtonsoft.Json.Linq.JObject Data { get; set; }
        public string AACreatedUser { get; set; }
        public int? Old_MEEmrDocumentID { get; set; }
        public bool IsBackgroundInit { get; set; }
        public string MEEmrDocumentRefNo { get; set; }
        public string MEEmrDocumentCreatedDate { get; set; }
    }
}
