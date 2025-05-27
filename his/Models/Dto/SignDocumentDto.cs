namespace his.Models.Dto
{
    public class SignDocumentDto
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public int DocumentId { get; set; }
        public bool IsBackgroundSign { get; set; }
        public string Role { get; set; }
        public bool PdfResult { get; set; }
        public bool IgnorePwCheck { get; set; }
        public string DepartmentNo { get; set; }

        public string DocumentCode { get; set; }
        public string HIS_ID { get; set; }
        public string FK_METemplateNo { get; set; }
    }
}
