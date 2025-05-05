using his.Helper;
using System.Text;

namespace his.Models.EMR
{
    public class ThongTinBenhAnNoiTru
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

    public class ICD
    {
        public string maicd { get; set; }
        public string tenicd { get; set; }
    }

    public class ChuyenVien
    {
        public string chuyenvien_loaituyen { get; set; }
        public string chuyenvien_tenchuyenkhoa { get; set; }
    }

    public class ThongTinNhiemKhuan
    {
        public string loidancuabacsi { get; set; }
        public string lydonhiemkhuan { get; set; }
        public string ngayphathiennhiemkhuan { get; set; }
        public NoiNhiemKhuan noinhiemkhuan { get; set; }
        public string phuongphapdieutrichinh { get; set; }
        public string tacnhannhiemkhuan { get; set; }
        public string thuthuatcanthiep { get; set; }
    }

    public class NoiNhiemKhuan
    {
        public string noinhiemkhuan_lannay_mota { get; set; }
        public string noinhiemkhuan_loai { get; set; }
        public string noinhiemkhuan_tuyentruoc_mota { get; set; }
    }

    public class ThongTinSinhSan
    {
        public List<string> ds_thongtincon { get; set; }
        public string phuongphapde { get; set; }
        public string tinhtrangsanphusausinh { get; set; }
    }

    public class ThongTinTaiNan
    {
        public string loaitainan { get; set; }
        public string noixayratainan { get; set; }
        public string thoidiemtainan { get; set; }
    }

    public class TinhHinhTuVong
    {
        public string ngaygiotuvong { get; set; }
        public string tinhhinhtuvong_khac_tentinhhinh { get; set; }
        public string tinhhinhtuvong_loai { get; set; }
        public string tinhhinhtuvongthoidiem { get; set; }
    }
}
