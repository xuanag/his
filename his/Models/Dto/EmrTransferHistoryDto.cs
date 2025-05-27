namespace his.Models.Dto
{
    public class EmrTransferHistoryDto
    {
        public string MEEmrNo { get; set; }
        public string FK_HRDepartmentToNo { get; set; }
        public string FK_HRDepartmentFromNo { get; set; }
        public string FK_HREmployeeToNo { get; set; }
        public string MEEmrTransferHistoryNo { get; set; }
        public string MEEmrTransferHistoryRemark { get; set; }
        public string AACreatedUser { get; set; }
        public string MEEmrTransferHistoryDate { get; set; }
    }
}
