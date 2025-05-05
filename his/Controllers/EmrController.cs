using Bogus;
using his.Helper;
using his.Models;
using his.Models.EMR;
using his.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

[Authorize]
[ApiController]
[Route("api/EMR.svc")]
public class EmrController : ControllerBase
{
    private readonly IPatientService _patientService;

    public EmrController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    [HttpPost("ThongTinBenhNhan")]
    public async Task<IActionResult> ThongTinBenhNhanAsync([FromBody] ThongTinBenhNhanRequest request)
    {
        if (string.IsNullOrEmpty(request.patientNo))
        {
            return BadRequest("Mã bệnh nhân không được để trống");
        }
        var patient = await _patientService.GetByNoAsync(request.patientNo);

        var benhNhan = new ThongTinBenhNhanResponse
        {
            buong = "P2",
            cmnd_so = patient.IdCardNo,
            dantoc = patient.Nation,
            dantoc_ma = patient.NationCode,
            denngaybhyt = "23/10/2025",
            diachi = patient.Address,
            diachinguoithan = "",
            doidangkybhyt = "75013",
            doituongbn_loai = "BHYT 4(80 %)",
            gioitinh = patient.Gender,
            giuong = "P2_G10",
            hochieu_so = "",
            hoten = patient.FullName,
            hotennguoithan = Helpers.RandomFullName(),
            khoa = patient.DepartmentName,
            khoa_matat = patient.DepartmentCode,
            lydotiepnhan = "Cấp cứu",
            mabhyt = patient.IssuranceNo,
            mayte = "25018958",
            ngaysinh = patient.DateOfBirth.ToString("yyyy/MM/dd"),
            nghenghiep = "Người quản lý công việc gia đình",
            nghenghiep_ma = "5152",
            ngoaikieu = "VN",
            ngoaikieu_ma = "",
            nhommau = "",
            noidangkykcbbd = "Trung tâm Y tế huyện Xuân Lộc",
            noigioithieu_loai = "Cơ quan y tế giới thiệu",
            noilamviec = "",
            quanhuyen = "Huyện Xuân Lộc",
            quanhuyen_ma = "Xuân Lộc",
            sodienthoai = patient.Phone,
            sodienthoainguoithan = "",
            soluutru = "",
            sonha = "",
            sovaovien = "25.021589",
            thongtinbn_ghichu = "",
            thonpho = " Khu Phố 6, ",
            tiensubenhtatcubanthan = "",
            tiensudiung = "",
            tinhthanh = "Đồng Nai",
            tinhthanh_ma = "Đồng Nai",
            tungaybhyt = "24 / 10 / 2024",
            tuoi = Helpers.CalAge(patient.DateOfBirth).ToString(),
            xaphuong = "Thị trấn Gia Ray",
            yeutorh = ""
        };

        if (benhNhan == null)
        {
            return NotFound("Không tìm thấy bệnh nhân");
        }

        return Ok(benhNhan);
    }

    [HttpPost("ThongTinBenhAnNoiTru")]
    public async Task<IActionResult> ThongTinBenhAnNoiTruAsync([FromBody] ThongTinBenhAnNoiTruRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new ThongTinBenhAnNoiTru()
        {
            bacsidieutri = "Mai Kiêm Toàn",
            bacsidieutrichinh = "Mai Kiêm Toàn",
            cdgiaiphaututhi = new ICD()
            {
                maicd = "",
                tenicd = ""
            },
            cdkhivaokhoadieutri = new ICD()
            {
                maicd = "E10.1",
                tenicd = "Bệnh đái tháo đường phụ thuộc insuline (Có nhiễm toan ceton)"
            },
            cdkkbcapcuu = new ICD()
            {
                maicd = "I64",
                tenicd = "Đột quị, không xác định do xuất huyết hay nhồi máu (Tai biến mạch máu não)"
            },
            cdnguyennhanchinhtuvong = new ICD()
            {
                maicd = "",
                tenicd = ""
            },
            cdnoichuyenden = new ICD()
            {
                maicd = "I69.4",
                tenicd = "Di chứng đột quỵ, không xác định là xuất huyết hay nhồi máu"
            },
            cdravien_benhchinh = new ICD()
            {
                maicd = "E10.1",
                tenicd = "Bệnh đái tháo đường phụ thuộc insuline (Có nhiễm toan ceton)"
            },
            cdravien_benhkemtheo = [
                new() {
                        maicd = "E11",
                        tenicd = "Bệnh đái tháo đường không phụ thuộc insuline"
                    },
                new() {
                        maicd = "I10",
                        tenicd = "Bệnh lý tăng huyết áp"
                }],
            cdravien_nguyennhan = new ICD()
            {
                maicd = "",
                tenicd = ""
            },
            chuyenvien = new ChuyenVien()
            {
                chuyenvien_loaituyen = "",
                chuyenvien_tenchuyenkhoa = ""
            },
            chuyenvienden = "",
            dschuyenkhoa = [],
            giaiphaubenh = "",
            ketquadieutri = "Không thay đổi",
            khamnghiemtuthi = "",
            khoa = "Khoa Hồi sức tích cực và chống độc",
            khoa_matat = "HSCC",
            khoavao = "Khoa Hồi sức tích cực và chống độc",
            loairavien = "",
            mayte = "25018958",
            ngaygioravien = "",
            ngaygiovaokhoa = "2025-05-05 13:00:02.000",
            ngaygiovaovien = "2025-05-05 11:17:20.000",
            noigioithieu = "Trung tâm Y tế huyện Xuân Lộc",
            phauthuthuat = [],
            sobenhan = "25.021589",
            soluutru = "",
            songaydieutrikhoavao = "",
            taibienbienchung = [],
            thongtinnhiemkhuan = new ThongTinNhiemKhuan()
            {
                loidancuabacsi = "",
                lydonhiemkhuan = "",
                ngayphathiennhiemkhuan = "",
                noinhiemkhuan = new NoiNhiemKhuan()
                {
                    noinhiemkhuan_lannay_mota = "",
                    noinhiemkhuan_loai = "",
                    noinhiemkhuan_tuyentruoc_mota = ""
                },
                phuongphapdieutrichinh = "",
                tacnhannhiemkhuan = "",
                thuthuatcanthiep = ""
            },
            thongtinsinhsan = new ThongTinSinhSan()
            {
                ds_thongtincon = [],
                phuongphapde = "",
                tinhtrangsanphusausinh = ""
            },
            thongtintainan = new ThongTinTaiNan()
            {
                loaitainan = "",
                noixayratainan = "",
                thoidiemtainan = ""
            },
            tinhhinhtuvong = new TinhHinhTuVong()
            {
                ngaygiotuvong = "",
                tinhhinhtuvong_khac_tentinhhinh = "",
                tinhhinhtuvong_loai = "",
                tinhhinhtuvongthoidiem = ""
            },
            tongsongaydieutri = "",
            tongsongaydieutri_chu = "",
            tructiepvao = "HSCC",
            vaoviendobenhnaylanthu = "1"
        };
        return Ok(thongTin);
    }

    [HttpPost("ThongTinPhieuKhamBenhVaoVien")]
    public async Task<IActionResult> ThongTinPhieuKhamBenhVaoVienAsync([FromBody] ThongTinPhieuKhamBenhVaoVienRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new ThongTinPhieuKhamBenhVaoVienResponse()
        {
            amhoamdao = "",
            bacsikhambenh_username = "nghialv",
            batdauchuyendatu = "",
            cannang = "48.00",
            chandoancuanoigioithieu = "theo dõi đột quỵ não / nhồi máu não cũ",
            chandoansobo = "Đột quị, không xác định do xuất huyết hay nhồi máu(Tai biến mạch máu não)",
            chandoanvaovien = "Đột quị, không xác định do xuất huyết hay nhồi máu(Tai biến mạch máu não)(I64); Bệnh lý tăng huyết áp(I10); Bệnh đái tháo đường không phụ thuộc insuline(E11)",
            chieucao = "150.00",
            chieucaotc = "",
            chovaodieutritaikhoa = "Khoa Hồi sức tích cực và chống độc",
            chuky_hoten = "Lương Văn Nghĩa",
            chukykinhnguyet = "",
            cotucung = "",
            denkhambenhluc = "2025-05-05 11:16:11.000",
            dolot = "",
            giochuyenda_khongro = "0",
            hinhdangtucung = "",
            huyetap_tamthu = "160",
            huyetap_tamtruong = "80",
            kehoachhoagiadinh = "",
            khambenh_cacbophan = "tim nhịp nhanh\r\nphổi không ran\r\nbụng mềm",
            khambenh_chandoanvaovienmaicd = "",
            khambenh_chuy = "",
            khambenh_daxuly = "Điện giải đồ(Na, K, Cl)[Máu](SL: 1); Định lượng Creatinin(máu)(SL: 1); Định lượng Glucose[Máu](SL: 1); Định lượng Troponin T hs[Máu](SL: 1); Định lượng Urê máu[Máu] (SL: 1); Định nhóm máu hệ ABO, Rh(D)(kỹ thuật Scangel / Gelcard trên máy tự động)(SL: 1); Ghi điện tim cấp cứu tại giường(SL: 1); Khám Cấp Cứu(vp)(SL: 1); Thời gian máu chảy phương pháp Duke(SL: 1); Thời gian prothrombin(PT: Prothrombin Time), (Các tên khác: TQ; Tỷ lệ Prothrombin) bằng máy tự động(SL:1); Thời gian thromboplastin một phần hoạt hóa(APTT: Activated Partial Thromboplastin Time), (tên khác: TCK) bằng máy tự động(SL:1); Tổng phân tích tế bào máu ngoại vi(bằng máy đếm tổng trở) (SL: 1); Xét nghiệm đường máu mao mạch tại giường(một lần) (SL: 1); Xét nghiệm Khí máu[Máu] (SL: 1).",
            khambenh_hong = "",
            khambenh_mat = "",
            khambenh_mui = "",
            khambenh_rhm = "",
            khambenh_tai = "",
            khambenh_toanthan = "bệnh nhân lơ mơ, kích thích\r\nGCS 9 điểm E2V3M4\r\nđồng tử 2 bên đều\r\nda niêm kém hồng\r\nchi ấm, mạch rõ",
            khambenh_tomtatketquacls = "Glucose mao mạch: 24.4 mmol / L",
            khamthaitai = "",
            khungchau = "",
            kieuthe = "",
            kinhlancuoingay = "",
            laychongnam = "",
            luongkinh = "Nhiều",
            luongnuocoi = "Vỡ",
            lydovaovien = patient.Reason,
            mach = "110",
            matphai = new MatBenhNhan()
            {
                nhanapvaovien = "",
                thilucvaovien_cokinh = "",
                thilucvaovien_khongkinh = ""
            },
            mattrai = new MatBenhNhan()
            {
                nhanapvaovien = "",
                thilucvaovien_cokinh = "",
                thilucvaovien_khongkinh = ""
            },
            mausacoi = "",
            ngaydusinh = "",
            ngoithai = "",
            nhietdo = "37.0",
            nhiptho = "22",
            nhungbenhphukhoadadieutri = "",
            noilamviecthannhan = "Con: Trần Thị Thái An",
            noingoaikhoa = "",
            nuocoinhieuitmau = "",
            oivoluc = "",
            phauthuatkhac = "",
            phongkham = "PK Cấp Cứu",
            quatrinhbenhly = "người nhà(con gái) khai, sáng nay lúc 06h00 người nhà phát hiện bệnh nhân nằm li bì, ú ớ, lay gọi không trả lời, cấp cứu tại TTYT huyện Xuân Lộc, chẩn đoán: theo dõi đột quỵ não / nhồi máu não cũ, xử trí: oxy, dịch truyền, chuyển BV Long Khánh",
            songaythaykinh = "",
            tangsinhmon = "",
            tcsankhoa_connangnhat = "",
            tcsankhoa_cothai = "",
            tcsankhoa_dhkn = "",
            tcsankhoa_namconnhonhat = "",
            tcsankhoa_sanh = "",
            tcsankhoa_say = "",
            tcsankhoa_soconsong = "",
            tcsankhoa_taibien = "",
            thanhbung = "",
            thongkinh = "Không",
            tiensubenhbanthan = "THA, ĐTĐ type 2",
            tiensubenhgiadinh = "khỏe",
            timthai = "",
            tuoibatdaukinh = "",
            vmc = "",
            vmc_lydo = "",
            vmc_ngay = "",
            vmc_taibenhvien = ""
        };
        return Ok(thongTin);
    }

    [HttpPost("KetQuaXetNghiem")]
    public async Task<IActionResult> KetQuaXetNghiemAsync([FromBody] XetNghiemRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new KetQuaXetNghiem()
        {
            Date = "2025-05-05 12:21:17.720",
            bacsichidinh = "Lương Văn Nghĩa",
            chandoan = "Đột quị, không xác định do xuất huyết hay nhồi máu (Tai biến mạch máu não); Bệnh lý tăng huyết áp; Bệnh đái tháo đường không phụ thuộc insuline",
            doituongbn = "BHYT 4 (80%)",
            ghichu = "",
            giolaymau = "2025-05-05 11:36:08.430",
            khoa = "Khoa Cấp cứu",
            masobenhpham = "050525-8397",
            nguoithuchienxetnghiem = "Lê Thị Thanh Huyền",
            noithuchienchidinh = "Khoa Xét nghiệm",
            sophieu = "25.XNHH.038822",
            pkqxn_ds_kqxn = new List<PkqxnKetQua>()
            {
                new PkqxnKetQua()
                {
          donvi = "T/l",
    ketqua = "4.76",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Hồng cầu (RBC)",
    trisothamchieu = "3.9 - 5.4"
            },
  new PkqxnKetQua(){
          donvi = "g/L",
    ketqua = "143",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "HGB(Hemoglobin)",
    trisothamchieu = "125 - 145"
  },
  new PkqxnKetQua(){
          donvi = "L/L",
    ketqua = "0.438",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Hct (hematocrit)",
    trisothamchieu = "0.35 - 0.47"
  },
  new PkqxnKetQua(){
          donvi = "fL",
    ketqua = "92",
    ketquabatthuong = "1",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "MCV",
    trisothamchieu = "83 - 91"
  },
  new PkqxnKetQua(){
          donvi = "pg",
    ketqua = "30",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "MCH",
    trisothamchieu = "27 - 31"
  },
  new PkqxnKetQua(){
          donvi = "g/l",
    ketqua = "326",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "MCHC",
    trisothamchieu = "320 - 351"
  },
  new PkqxnKetQua(){
          donvi = "%CV",
    ketqua = "13.2",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "RDW-CV",
    trisothamchieu = "9 - 15.5"
  },
  new PkqxnKetQua(){
          donvi = "fL",
    ketqua = "45.6",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "RDW-SD",
    trisothamchieu = "35 - 46"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "0.00",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "NRBC#",
    trisothamchieu = "0 - 0.02"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "0.0",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "NRBC%",
    trisothamchieu = "0 - 0.4"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "194",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Tiểu cầu (PLT)",
    trisothamchieu = "150 - 400"
  },
  new PkqxnKetQua(){
          donvi = "fL",
    ketqua = "9.6",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "MPV",
    trisothamchieu = "7.2 - 11.1"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "0.19",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "PCT",
    trisothamchieu = "0.16 - 0.36"
  },
  new PkqxnKetQua(){
          donvi = "fL",
    ketqua = "10.4",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "PDW",
    trisothamchieu = "9 - 17"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "10.64",
    ketquabatthuong = "1",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Bạch cầu (WBC)",
    trisothamchieu = "4 - 10"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "7.31",
    ketquabatthuong = "1",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Neutrophils",
    trisothamchieu = "1.7 - 7"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "1.97",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Lymphocyte",
    trisothamchieu = "1 - 4"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "0.89",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Monocytes",
    trisothamchieu = "0.1 - 1"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "0.10",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Eosinophils",
    trisothamchieu = "0.05 - 0.7"
  },
  new PkqxnKetQua(){
          donvi = "G/L",
    ketqua = "0.04",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "Basophils",
    trisothamchieu = "0.05"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "68.7",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "NEUT%",
    trisothamchieu = "42 - 70"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "18.5",
    ketquabatthuong = "1",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "LYM%",
    trisothamchieu = "25 - 40"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "8.4",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "MONO%",
    trisothamchieu = "2.5 - 10"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "0.9",
    ketquabatthuong = "1",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "ESO%",
    trisothamchieu = "1.25 - 7"
  },
  new PkqxnKetQua(){
          donvi = "%",
    ketqua = "0.4",
    ketquabatthuong = "0",
    loaixetnghiem = "XN Huyết Học",
    nhom = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
    noithuchienchidinh = "Khoa Xét nghiệm",
    pkqxn_tenxetnghiem = "BASO%",
    trisothamchieu = "1"
  }
            }
        };
        return Ok(thongTin);
    }

    [HttpPost("KetQuaCanLamSang")]
    public async Task<IActionResult> KetQuaCanLamSangAsync([FromBody] CanLamSangRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new KetQuaCanLamSang()
        {
            Date = "2025-05-05 11:53:44.000",
            bacsichidinh = "Lương Văn Nghĩa",
            bacsithuchien = "",
            chandoan = "Đột quị, không xác định do xuất huyết hay nhồi máu (Tai biến mạch máu não)(I64); Bệnh lý tăng huyết áp(I10); Bệnh đái tháo đường không phụ thuộc insuline(E11)",
            hinhanh = [],
            kqcls_denghi = "",
            kqcls_ketluan = "- Hiện không thấy bất thường tín hiệu nhu mô não.\u000d\u000a- Dày niêm mạc các xoang cạnh mũi. ",
            kqcls_linkpacs = "http:///192.168.1.10/clinicalstudio/integration/viewer?mrn=25018958&Accession=250213001530",
            kqcls_mota = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fprq2\\fcharset0 Tahoma;}{\\f1\\fswiss\\fprq2\\fcharset163 Tahoma;}{\\f2\\fnil\\fcharset0 Tahoma;}}\u000d\u000a{\\colortbl ;\\red139\\green0\\blue0;\\red0\\green0\\blue0;}\u000d\u000a\\viewkind4\\uc1\\pard\\cf1\\f0\\fs24 * K\\u7928? THU\\u7852?T KH\\u7842?O S\\'c1T:\\par\u000d\u000a- Kh\\u7843?o s\\'e1t s\\u7885? n\\'e3o v\\u7899?i c\\'e1c chu\\u7895?i xung: Axial T1W, T2W, FLAIR.\\par\u000d\u000a* M\\'d4 T\\u7842? H\\'ccNH \\u7842?NH :\\par\u000d\u000a- Kh\\'f4ng th\\u7845?y b\\u7845?t th\\f1\\'fd\\f0\\u7901?ng t\\'edn hi\\u7879?u v\\'e0 h\\'ecnh d\\u7841?ng c\\u7845?u tr\\'fac b\\'e1n c\\u7847?u \\u273?\\u7841?i n\\'e3o hai b\\'ean.\\par\u000d\u000a- Kh\\'f4ng th\\u7845?y b\\u7845?t th\\f1\\'fd\\f0\\u7901?ng t\\'edn hi\\u7879?u v\\'e0 h\\'ecnh d\\u7841?ng c\\'e1c c\\u7845?u tr\\'fac h\\'e0nh, c\\u7847?u, cu\\u7889?ng n\\'e3o v\\'e0 hai b\\'e1n c\\u7847?u ti\\u7875?u n\\'e3o.\\par\u000d\u000a- \\u272?\\u432?\\u7901?ng gi\\u7919?a kh\\'f4ng l\\u7879?ch, h\\u7879? th\\u7889?ng n\\'e3o th\\u7845?t tr\\'ean v\\'e0 d\\u432?\\u7899?i l\\u7873?u trong gi\\u7899?i h\\u7841?n b\\'ecnh th\\u432?\\u7901?ng.\\par\u000d\u000a\\pard\\sa200\\sl276\\slmult1 - D\\'e0y ni\\'eam m\\u7841?c c\\'e1c xoang c\\u7841?nh m\\u361?i. \\par\u000d\u000a\\pard\\cf2\\f2\\par\u000d\u000a}\u000d\u000a",
            kythuatvien1 = "",
            kythuatvien2 = "",
            loaiphim = "35 x 43 1,5 Tesla",
            loaithuoc = "",
            ngaychidinh = "2025-05-05 11:37:22.467",
            ngaythuchien = "2025-05-05 11:53:44.000",
            nhanxet = "",
            noichidinh = "Khoa Cấp cứu",
            noithuchienchidinh = "X-Quang",
            soluongphim = "3.00",
            soluongthuoccanquan = "",
            sophieu = "25.0213.001530",
            tenchidinh = "Chụp cộng hưởng từ não- mạch não không tiêm chất tương phản (0.2-1.5T)",
            thietbi = "HX",
            tiemcanquan = "Không",
            trangthai = "DangThucHien"
        };
        return Ok(thongTin);
    }

    [HttpPost("KetQuaCanLamSangKB")]
    public async Task<IActionResult> KetQuaCanLamSangKBAsync([FromBody] CanLamSangRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new KetQuaCanLamSang()
        {
            Date = "2025-05-05 11:53:44.000",
            bacsichidinh = "Lương Văn Nghĩa",
            bacsithuchien = "",
            chandoan = "Đột quị, không xác định do xuất huyết hay nhồi máu (Tai biến mạch máu não)(I64); Bệnh lý tăng huyết áp(I10); Bệnh đái tháo đường không phụ thuộc insuline(E11)",
            hinhanh = [],
            kqcls_denghi = "",
            kqcls_ketluan = "- Hiện không thấy bất thường tín hiệu nhu mô não.\u000d\u000a- Dày niêm mạc các xoang cạnh mũi. ",
            kqcls_linkpacs = "http:///192.168.1.10/clinicalstudio/integration/viewer?mrn=25018958&Accession=250213001530",
            kqcls_mota = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fswiss\\fprq2\\fcharset0 Tahoma;}{\\f1\\fswiss\\fprq2\\fcharset163 Tahoma;}{\\f2\\fnil\\fcharset0 Tahoma;}}\u000d\u000a{\\colortbl ;\\red139\\green0\\blue0;\\red0\\green0\\blue0;}\u000d\u000a\\viewkind4\\uc1\\pard\\cf1\\f0\\fs24 * K\\u7928? THU\\u7852?T KH\\u7842?O S\\'c1T:\\par\u000d\u000a- Kh\\u7843?o s\\'e1t s\\u7885? n\\'e3o v\\u7899?i c\\'e1c chu\\u7895?i xung: Axial T1W, T2W, FLAIR.\\par\u000d\u000a* M\\'d4 T\\u7842? H\\'ccNH \\u7842?NH :\\par\u000d\u000a- Kh\\'f4ng th\\u7845?y b\\u7845?t th\\f1\\'fd\\f0\\u7901?ng t\\'edn hi\\u7879?u v\\'e0 h\\'ecnh d\\u7841?ng c\\u7845?u tr\\'fac b\\'e1n c\\u7847?u \\u273?\\u7841?i n\\'e3o hai b\\'ean.\\par\u000d\u000a- Kh\\'f4ng th\\u7845?y b\\u7845?t th\\f1\\'fd\\f0\\u7901?ng t\\'edn hi\\u7879?u v\\'e0 h\\'ecnh d\\u7841?ng c\\'e1c c\\u7845?u tr\\'fac h\\'e0nh, c\\u7847?u, cu\\u7889?ng n\\'e3o v\\'e0 hai b\\'e1n c\\u7847?u ti\\u7875?u n\\'e3o.\\par\u000d\u000a- \\u272?\\u432?\\u7901?ng gi\\u7919?a kh\\'f4ng l\\u7879?ch, h\\u7879? th\\u7889?ng n\\'e3o th\\u7845?t tr\\'ean v\\'e0 d\\u432?\\u7899?i l\\u7873?u trong gi\\u7899?i h\\u7841?n b\\'ecnh th\\u432?\\u7901?ng.\\par\u000d\u000a\\pard\\sa200\\sl276\\slmult1 - D\\'e0y ni\\'eam m\\u7841?c c\\'e1c xoang c\\u7841?nh m\\u361?i. \\par\u000d\u000a\\pard\\cf2\\f2\\par\u000d\u000a}\u000d\u000a",
            kythuatvien1 = "",
            kythuatvien2 = "",
            loaiphim = "35 x 43 1,5 Tesla",
            loaithuoc = "",
            ngaychidinh = "2025-05-05 11:37:22.467",
            ngaythuchien = "2025-05-05 11:53:44.000",
            nhanxet = "",
            noichidinh = "Khoa Cấp cứu",
            noithuchienchidinh = "X-Quang",
            soluongphim = "3.00",
            soluongthuoccanquan = "",
            sophieu = "25.0213.001530",
            tenchidinh = "Chụp cộng hưởng từ não- mạch não không tiêm chất tương phản (0.2-1.5T)",
            thietbi = "HX",
            tiemcanquan = "Không",
            trangthai = "DangThucHien"
        };
        return Ok(thongTin);
    }

    [HttpPost("ThongTinPhieuChiDinh")]
    public async Task<IActionResult> ThongTinPhieuChiDinhAsync([FromBody] PhieuChiDinhRequest request)
    {
        var patient = await _patientService.GetByNoAsync(request.patientNo);
        var thongTin = new ThongTinPhieuChiDinh()
        {
            Date = "2025-05-05 11:17:55.000",
            bacsichidinh = "Lương Văn Nghĩa",
            bacsichidinh_username = "nghialv",
            chandoansobo = "Đột quị, không xác định do xuất huyết hay nhồi máu (Tai biến mạch máu não); Bệnh lý tăng huyết áp; Bệnh đái tháo đường không phụ thuộc insuline",
            doituongbn = "BHYT 4 (80%)",
            ds_chidinh = new List<ds_chidinh_item>()
            {
                new ds_chidinh_item()
                {
                      benhnhanphaitra = "8,700.00",
                      dongia = "43,500.00",
                      loaichidinh = "XN Huyết Học",
                      noithuchienchidinh = "Khoa Xét nghiệm",
                      soluong = 1.0,
                      tenchidinh = "Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)",
                      thanhtien = "43,500.00"
                }
            },
            giogiaomau = "",
            giolaymau = "",
            giuong = "P2_G10",
            khoadieutri = "Khoa Hồi sức tích cực và chống độc",
            mucdochidinh = "",
            nguoilaymau = "",
            noichidinh = "PK Cấp Cứu",
            phong = "P2",
            sophieu = "25.XNHH.038822",
            tongcong = "8,700.00"
        };
        return Ok(thongTin);
    }
}
