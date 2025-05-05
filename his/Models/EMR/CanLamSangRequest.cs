using System.Text;

namespace his.Models.EMR
{
    public class CanLamSangRequest
    {
        public string patientNo { get; set; } = string.Empty;
        public string emrNo { get; set; } = string.Empty;
        public string documentNo { get; set; } = string.Empty;
        public string documentDate { get; set; } = string.Empty;
        public string tid { get; set; } = string.Empty;
        public string gid { get; set; } = string.Empty;
        public string caseNo { get; set; } = string.Empty;
        public string maketqua { get; set; } = string.Empty;
    }
}
