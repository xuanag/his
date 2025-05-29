namespace his.Models.Dto
{
    public class DichVuChiDinhDto
    {
        public string Ma { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string? GhiChu { get; set; }
    }

    public class PhieuChiDinhDto
    {
        public string AdmissionCode { get; set; } = null!;
        public string DepartmentCode { get; set; } = string.Empty;
        public string BenhNhanId { get; set; } = null!;
        public string BenhNhanCode { get; set; } = null!;
        public List<DichVuChiDinhDto> DichVu { get; set; } = new();
    }
}
