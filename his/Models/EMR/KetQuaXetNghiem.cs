using System.Text;

namespace his.Models.EMR
{
    public class KetQuaXetNghiem
    {
        public string sophieu { get; set; }
        public string Date { get; set; }
        public string bacsichidinh { get; set; }
        public string chandoan { get; set; }
        public string doituongbn { get; set; }
        public string ghichu { get; set; }
        public string giolaymau { get; set; }
        public string khoa { get; set; }
        public string masobenhpham { get; set; }
        public string nguoithuchienxetnghiem { get; set; }
        public string noithuchienchidinh { get; set; }
        public List<PkqxnKetQua> pkqxn_ds_kqxn { get; set; }
    }

    public class PkqxnKetQua
    {
        public string donvi { get; set; }
        public string ketqua { get; set; }
        public string ketquabatthuong { get; set; }
        public string loaixetnghiem { get; set; }
        public string nhom { get; set; }
        public string noithuchienchidinh { get; set; }
        public string pkqxn_tenxetnghiem { get; set; }
        public string trisothamchieu { get; set; }
    }
}
