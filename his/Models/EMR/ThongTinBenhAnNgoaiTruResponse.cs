using his.Helper;
using System.Text;

namespace his.Models.EMR
{
    public class ThongTinBenhAnNgoaiTru
    {
        public string bacsidieutri { get; set; }
        public string bacsidieutrichinh { get; set; }
        public ICD cdgiaiphaututhi { get; set; }
        public ICD cdkhivaokhoadieutri { get; set; }
        public ICD cdkkbcapcuu { get; set; }
        public ICD cdnguyennhanchinhtuvong { get; set; }
        public ICD cdnoichuyenden { get; set; }
        public ICD cdravien_benhchinh { get; set; }
        public List<ICD> cdravien_benhkemtheo { get; set; }
        public ICD cdravien_nguyennhan { get; set; }
        public ChuyenVien chuyenvien { get; set; }
        public string chuyenvienden { get; set; }
        public List<string> dschuyenkhoa { get; set; }
        public string giaiphaubenh { get; set; }
        public string ketquadieutri { get; set; }
        public string khamnghiemtuthi { get; set; }
        public string khoa { get; set; }
        public string khoa_matat { get; set; }
        public string khoavao { get; set; }
        public string loairavien { get; set; }
        public string mayte { get; set; }
        public string ngaygioravien { get; set; }
        public string ngaygiovaokhoa { get; set; }
        public string ngaygiovaovien { get; set; }
        public string noigioithieu { get; set; }
        public List<string> phauthuthuat { get; set; }
        public string sobenhan { get; set; }
        public string soluutru { get; set; }
        public string songaydieutrikhoavao { get; set; }
        public List<string> taibienbienchung { get; set; }
        public ThongTinNhiemKhuan thongtinnhiemkhuan { get; set; }
        public ThongTinSinhSan thongtinsinhsan { get; set; }
        public ThongTinTaiNan thongtintainan { get; set; }
        public TinhHinhTuVong tinhhinhtuvong { get; set; }
        public string tongsongaydieutri { get; set; }
        public string tongsongaydieutri_chu { get; set; }
        public string tructiepvao { get; set; }
        public string vaoviendobenhnaylanthu { get; set; }
    } 
}
