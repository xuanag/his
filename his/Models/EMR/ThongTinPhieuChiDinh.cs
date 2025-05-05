using System.Text;

namespace his.Models.EMR
{
    public class ThongTinPhieuChiDinh
    {
        public string Date { get; set; }
        public string bacsichidinh { get; set; }
        public string bacsichidinh_username { get; set; }
        public string chandoansobo { get; set; }
        public string doituongbn { get; set; }
        public List<ds_chidinh_item> ds_chidinh { get; set; }
        public string giogiaomau { get; set; }
        public string giolaymau { get; set; }
        public string giuong { get; set; }
        public string khoadieutri { get; set; }
        public string mucdochidinh { get; set; }
        public string nguoilaymau { get; set; }
        public string noichidinh { get; set; }
        public string phong { get; set; }
        public string sophieu { get; set; }
        public string tongcong { get; set; }
    }

    public class ds_chidinh_item
    {
        public string benhnhanphaitra { get; set; }
        public string dongia { get; set; }
        public string loaichidinh { get; set; }
        public string noithuchienchidinh { get; set; }
        public double soluong { get; set; }
        public string tenchidinh { get; set; }
        public string thanhtien { get; set; }
    }
}
