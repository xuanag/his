using his.Helper;
using his.Models;
using his.Services;
using his.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace his.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPatientService _patientService;
        private readonly IAdmissionService _admissionService;
        private readonly IAdmissionSequenceService _admissionSequenceService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public PatientsController(IHttpClientFactory httpClientFactory,
            IPatientService patientService,
            IAdmissionService admissionService,
            IAdmissionSequenceService admissionSequenceService,
            ISettingService settingService,
            ICategoryService categoryService,
            IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _patientService = patientService;
            _admissionService = admissionService;
            _admissionSequenceService = admissionSequenceService;
            _settingService = settingService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string search = "")
        {
            await InitFirstData();
            var patients = await _patientService.GetAllAsync(search);
            var departments = await _categoryService.CategoriesByType("department");
            var emrTypes = await _categoryService.CategoriesByType("emrType");
            var viewModel = new PatientViewModel()
            {
                Patients = patients,
                Departments = departments,
                EmrTypes = emrTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Patients/Create")]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            patient.PatientCode = await _patientService.GeneratePatientCodeAsync("BN");

            // Nhap khoa / Ma Nhan Vien = Ma BA
            if (!string.IsNullOrEmpty(patient.DepartmentCode))
                patient.AdmissionCode = await _admissionSequenceService.GenerateAdmissionCodeAsync(patient.DepartmentCode);

            await _patientService.CreateAsync(patient);

            #region CALL EMR API
            try
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await AdmissionToEmr(patient);
                    }
                    catch (Exception ex)
                    {
                        // Nếu cần log lỗi
                        Console.WriteLine("API error: " + ex.Message);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            #endregion

            // Trả về URL cần redirect (danh sách bệnh nhân)
            return Json(new { redirectUrl = Url.Action("Index", "Patients") });
        }

        public async Task AdmissionToEmr(Patient patient)
        {
            var urlSetting = await _settingService.GetByKey("api-emr");
            if (urlSetting != null && !string.IsNullOrEmpty(urlSetting.Value))
            {
                var client = _httpClientFactory.CreateClient();
                var tokenSetting = await _settingService.GetByKey("api-emr-token");
                if (tokenSetting != null)
                {
                    var token = tokenSetting.Value;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = $"{urlSetting.Value}/api/services/vendor/his/v2/emrgenerators"; // URL API thật của bạn

                    var formatDate = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    var emrDto = new EmrDto()
                    {
                        MEEmrNo = patient.AdmissionCode,
                        MEEmrDesc = "Nhập từ HIS",
                        AACreatedUser = "emr",
                        MEPatientNo = patient.PatientCode,
                        MEPatientName = patient.FullName,
                        MEGender = patient.Gender,
                        MEPatientBirthday = patient.DateOfBirth.ToString(formatDate),
                        MEPatientContactCellPhone = patient.Phone,
                        MEPatientPermanentResidence = "",
                        MEPatientContactAddressLine = patient.Address,
                        MEPatientIDCard = patient.IdCardNo,
                        MEPatientIDCardDate = patient.IdCardDate?.ToString(formatDate),
                        MEMarital = patient.Marital.ToString(),
                        MEPatientIDCardStateProvinces = "",
                        FK_MEEmrTypeNo = patient.EmrTypeCode,
                        FK_HRDepartmentNo = patient.DepartmentCode,
                        MEPatientBirthdayOnlyYear = false,
                        AutoGenDocuments = true,
                        MEEmrSourceNo = string.Empty
                    };
                    // Convert to JSON
                    string json = System.Text.Json.JsonSerializer.Serialize(emrDto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //return Json(result);
                    }
                    else
                    {
                        // Thất bại
                        var error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + error);
                        //return BadRequest($"Không thể gọi API EMR. Error: {error}");
                    }
                }
            }
        }


        private async Task<string> GetAccessToken()
        {
            // Develop later
            var client = _httpClientFactory.CreateClient();
            var url = "http://localhost:6235/api/services/app/TokenAuth/Authenticate";
            var requestBody = new
            {
                userNameOrEmailAddress = "admin",
                password = "123456"
            };
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                return jsonResponse.result.accessToken;
            }
            else
            {
                throw new Exception("Failed to get access token");
            }
        }

        private async Task InitFirstData()
        {
            var initDataMode = await _settingService.GetByKey("init-data");
            if (initDataMode != null)
            {
                if (initDataMode.Value == "true")
                {
                    return;
                }
            }


            #region Setting
            var setting = new Setting()
            {
                Key = "init-data",
                Value = "true"
            };
            await _settingService.CreateAsync(setting);

            setting = new Setting()
            {
                Key = "api-emr",
                Value = "http://localhost:6235"
            };
            await _settingService.CreateAsync(setting);

            setting = new Setting()
            {
                Key = "api-emr-token",
                Value = "6wdaX3N9c-HJ8I1X03IN_YTDptpxGmWfwYEgMoxTUMQ2tKZNVdjb3YQzvCLcJg0FA_5OMcRoAnyq-zPwHElm3CTWpQNb1TmO7AYuLA_fXUHSo8ItL3z_a7pKlAVPl2j-TMePgLhLH6gsE36qWhiXQE-zLW0AzeYL8dNzDkGkZr0elIdya1h3ikL-80dgD3_2w0ozY8LPuSTTo_xTTksFZzztpLNFKKsbT9roteji26PRMNLVcU7YSNcU4UHtiFzAToXUWJhCWt1WiN7R8G4dP_Vwz4OKK2bd9wV05T8f5RnzgRGVbT371FrEbWT_yW5flGhN-_RyRxjH25qbwRpzvCJOddGEmiAZcu06cRcVOb1zUvHNi7mqkNjpUPG1pxdGLpGtMBax89SZTgjg26zqxWi-q8GGESXKeu0eybORJUj7C3BAP_ThUTdAseQaFFu_60UoWi85rs0p7_HgnVPr1ngt7EiDcIEg77oXDI9-DNuwGcXwlshBZEGaolSdIWWXTlG7WBTx4c2KEWzC-AWW7GHN8q46vynL2jCJiDnZ5I0LryrVVbtaIxUGeSHb7H2PdWiw7QRcR8sLNxuKkcvmWaHI-LGJh4LqXsh-LwxJKJU"
            };
            await _settingService.CreateAsync(setting);
            #endregion

            #region User
            var admin = new User
            {
                Username = "emr",
                PasswordHash = Helpers.ComputeSha256Hash("123")
            };
            await _userService.CreateAsync(admin);
            #endregion

            #region Category
            var type = "department";
            var categoriesSt = new List<string>()
            {
                "KB;Khoa Khám bệnh",
                "1;Hệ Thống",
                "2;Các Phòng Khám (Khoa Khám Bệnh)",
                "3;PK Nội Tiết 1",
                "4;Phòng Kế Hoạch Tổng Hợp",
                "5;PK Nội Tổng Quát 1",
                "6;PK Nhi 1",
                "9;PK Nhi 2",
                "10;PK Cấp Cứu",
                "11;PK Nội Tổng Quát 3",
                "15;Khoa Liên chuyên khoa (Mắt-TMH-RMH)",
                "16;Phòng Khám Mắt",
                "17;Phòng Khám Tai Mũi Họng",
                "18;Phòng Khám Răng Hàm Mặt",
                "20;Khoa Ngoại Tổng Hợp",
                "21;Phòng Khám Ngoại Tổng Hợp 1",
                "22;Khoa Nội tổng hợp",
                "23;Khoa Nhi",
                "25;Khoa Truyền nhiễm",
                "26;Khoa Sản phụ",
                "27;Phòng Khám Thai(P305)",
                "28;Khoa Y Học Cổ Truyền",
                "29;Phòng Khám Y Học Cổ Truyền (A013)",
                "30;Khoa Dược",
                "37;Khoa Hồi sức tích cực và chống độc",
                "39;Khoa Chẩn Đoán Hình Ảnh",
                "40;Khoa Xét Nghiệm",
                "48;Khoa Phẫu Thuật - Gây Mê Hồi Sức",
                "91;Phòng hồi sức",
                "112;Phòng HSCC",
                "113;Phòng cấp cứu",
                "122;Phòng Khám Tầm Soát Bệnh",
                "125;Kiểm Soát Nhiễm Khuẩn",
                "126;Khoa khám bệnh",
                "128;Phòng Khám Phụ Khoa(P307)",
                "129;PK Dinh Dưỡng",
                "130;Phòng Khám Phục Hồi Chức Năng(124)",
                "131;Khoa Khám Bệnh(105)",
                "133;Khoa Vật Lý Trị Liệu - Phục Hồi Chức Năng",
                "135;Phòng Khám Ngoại Tổng Hợp (Ngoài giờ)",
                "136;Phòng Khám Liên Chuyên Khoa",
                "137;Phòng Khám Sản [ngoài giờ]",
                "139;PK Nội Tiết 2",
                "142;Phòng Chạy Thận Nhân Tạo",
                "143;Khoa Dinh Dưỡng",
                "146;PK Nhiễm",
                "147;PK Nội Tổng Quát 2",
                "148;PK Hô Hấp",
                "149;PK Da Liễu",
                "150;PK Nội Tổng Quát 4",
                "153;Pk Nội Tổng Quát 5",
                "154;PK Nội Tổng Quát 6",
                "155;Khoa Chấn thương chỉnh hình",
                "156;PK CTCH - Bỏng",
                "158;PK CTCH (ngoài giờ)",
                "159;PK Viêm gan - Ký sinh trùng",
                "160;Khám nội tiêu hóa",
                "161;PK Cột sống - cơ xương khớp",
                "162;PK Mạch máu - tim mạch",
                "164;Khoa Nội Thần Kinh",
                "165;PK Nội Tim Mạch",
                "173;Khoa Ngoại thận - Tiết niệu ",
                "174;PK Ngoại thận - Tiết niệu",
                "197;PK Ngoại Tiết Niệu (ngoài giờ)",
                "286;Khoa Cấp Cứu",
                "287;Khoa Nội Tim Mạch - Lão học",
                "292;PK Nội Thần Kinh",
                "293;PK Sa Sút Trí Tuệ",
                "KHTH_KNgTQ;Kế hoạch tổng hợp -  Khoa ngoại tổng hợp",
                "KHTH_KNTQ;Kế hoạch tổng hợp - Khoa nội tổng hợp",
                "KHTH_KNh;Kế hoạch tổng hợp - Khoa Nhi",
                "KHTH_KNhiem;Kế hoạch tổng hợp - Khoa truyền nhiễm",
                "KHTH_KSP;Kế hoạch tổng hợp - Khoa sản phụ",
                "KHTH_YHCT;Kế hoạch tổng hợp - Khoa y học cổ truyền",
                "KHTH_HSCC;Kế hoạch tổng hợp - Khoa hồi sức cấp cứu",
                "KHTH_KPT;Kế hoạch tổng hợp -  Khoa phẫu thuật",
                "KHTH_VLTL;Kế hoạch tổng hợp - Khoa Vật Lý Trị Liệu",
                "KHTH_KNgoaiCTCH;Kế hoạch tổng hợp - Khoa Ngoại CTCH",
                "KHTH_KnoiTK;Kế hoạch tổng hợp - Khoa Nội thần kinh",
                "KHTH_KNgoaiTN;Kế hoạch tổng hợp - Khoa ngoại thận tiết niệu",
                "KHTH_KCCTH;Kế hoạch tổng hợp -  Khoa cấp cứu",
                "KHTH_KNOITML;Kế hoạch tổng hợp - Khoa nội tim mạch lão học",
                "KHTH_KLCK;Kế hoạch tổng hợp - Khoa Liên chuyên khoa",
                "KHTH_KCC;Kế hoạch tổng hợp - Khoa khám bệnh",
                "295;PK Nội Tổng Quát 7",
                "307;PK Nội Tiết 3",
                "KHTH_KNgTQ2;KHTH -  Khoa ngoại tổng hợp Số 2",
                "KHTH_KNTQ2;KHTH - Khoa nội tổng hợp Số 2",
                "KHTH_KNh2;KHTH - Khoa Nhi Số 2",
                "KHTH_KNhiem2;KHTH - Khoa truyền nhiễm Số 2",
                "KHTH_KSP2;KHTH - Khoa sản phụ Số 2",
                "KHTH_YHCT2;KHTH - Khoa y học cổ truyền Số 2",
                "KHTH_HSCC2;KHTH - Khoa hồi sức cấp cứu Số 2",
                "KHTH_KPT2;KHTH -  Khoa phẫu thuật Số 2",
                "KHTH_VLTL2;KHTH - Khoa Vật Lý Trị Liệu Số 2",
                "KHTH_KNgoaiCTCH2;KHTH - Khoa Ngoại CTCH Số 2",
                "KHTH_KnoiTK2;KHTH - Khoa Nội thần kinh Số 2",
                "KHTH_KNgoaiTN2;KHTH - Khoa ngoại thận tiết niệu Số 2",
                "KHTH_KCCTH2;KHTH -  Khoa cấp cứu Số 2",
                "KHTH_KNOITML2;KHTH - Khoa nội tim mạch lão học Số 2",
                "KHTH_KLCK2;KHTH - Khoa Liên chuyên khoa Số 2",
                "KHTH_KCC2;KHTH - Khoa khám bệnh Số 2",
                "BHXH;Giám định Bảo hiểm",
                "QLCL;Phòng Quản Lý Chất Lượng",
                "124;Phòng Nội Soi Tai Mũi Họng",
                "327;PK Nội Tổng Quát 8",
                "335;Khoa Điều Trị Covid-19",
                "352;PK Nhi [VIP]",
                "353;PK Nội Thần Kinh [VIP]",
                "354;PK Nội tim mạch [VIP]",
                "355;PK Ngoại thận - Tiết niệu [VIP]",
                "356;PK Nhi [VIP](Ngoài giờ)",
                "357;PK Nội Thần Kinh [VIP](Ngoài giờ)",
                "358;PK Nội tim mạch [VIP](Ngoài giờ)",
                "359;PK Ngoại thận - Tiết niệu [VIP](Ngoài giờ)",
                "360;PK Ngoại Tổng Quát [VIP]",
                "361;PK Ngoại Tổng Quát [VIP](Ngoài giờ)",
                "362;PK Nội Tổng Quát [VIP]",
                "363;PK Nội Tổng Quát [VIP](Ngoài giờ)",
                "364;PK Chấn thương chỉnh hình [VIP]",
                "365;PK Chấn thương chỉnh hình [VIP](Ngoài giờ)",
                "366;Phòng Khám Tư Vấn Sức Khỏe [VIP](Ngoài giờ)",
                "367;PK Sản [VIP]",
                "368;PK Sản [VIP](Ngoài giờ)",
                "369;Phòng Khám Phụ Khoa [VIP]",
                "370;Phòng Khám Phụ Khoa [VIP](Ngoài giờ)",
                "371;Phòng Khám Răng Hàm Mặt [VIP]",
                "373;Phòng Khám Răng Hàm Mặt [VIP](Ngoài giờ)",
                "460;Đơn vị điều trị Covid-19 T2",
                "463;PK Nội Tim mạch - Lão học",
                "464;PK Nội Tổng Hợp 9",
                "465;Phòng Khám Ngoại tổng hợp 2",
                "1111;Hệ thống",
                "467;PK Nội Tổng Hợp 10",
                "468;PK Nội Tổng Hợp 11",
                "580;PK Nội Tổng Hợp 12",
                "469;Phòng Khám Ngoại Tổng Hợp 3",
                "326;PK Nhi sơ sinh",
                "466;Phòng Khám Tai Mũi Họng 2"
            };
            foreach (var categorySt in categoriesSt)
            {
                var category = new Category()
                {
                    Type = type,
                    Code = categorySt.Split(";")[0],
                    Value = categorySt.Split(";")[1]
                };
                await _categoryService.CreateAsync(category);
            }

            type = "emrType";
            var categoriesEmrType = new List<string>()
            {
                "BBO;Bệnh án Bỏng",
                "BDI;Bệnh án điều dưỡng và phục hồi chức năng",
                "BMA;Bệnh án Mắt",
                "BMG;Bệnh án mắt (Glôcôm)",
                "BML;Bệnh án mắt (lác, sụp mi)",
                "BMT;Bệnh án mắt (trẻ em)",
                "BNG;Bệnh án Ngoại khoa",
                "BNH;Bệnh án Nhi khoa",
                "BNO;Bệnh án Nội khoa",
                "BPH;Bệnh án Phụ khoa",
                "BRA;Bệnh án Răng Hàm Mặt",
                "BSA;Bệnh án Sản khoa",
                "BSO;Bệnh án Sơ sinh",
                "BTA;Bệnh án Tai Mũi Họng",
                "BTR;Bệnh án Truyền nhiễm",
                "NNT;Bệnh án ngoại trú",
                "BNPT;Bệnh án phá thai",
                "NTMAT;Bệnh án ngoại trú chuyên khoa Mắt",
                "NTTMH;Bệnh án ngoại trú chuyên khoa Tai Mũi Họng",
                "NTRHM;Bệnh án ngoại trú chuyên khoa Răng Hàm Mặt",
                "BBDY;Bệnh án ngoại trú Y học cổ truyền",
                "BBN;Bệnh án YHCT Ban ngày",
                "BMD;Bệnh án mắt (Đáy mắt)",
                "BMCT;Bệnh án mắt (Chấn thương)",
                "BMBP;Bệnh án mắt (Bán phần trước)",
                "BTCM;Bệnh án Tay chân miệng",
                "BNDY;Bệnh án nội trú Y học cổ truyền",
                "BCC;Bệnh án cấp cứu",
                "NTYHCT;Bệnh án ngoại trú Y học cổ truyền",
                "BOOK;Sổ sách"
            };
            foreach (var typeSt in categoriesEmrType)
            {
                var category = new Category()
                {
                    Type = type,
                    Code = typeSt.Split(";")[0],
                    Value = typeSt.Split(";")[1]
                };
                await _categoryService.CreateAsync(category);
            }
            #endregion
        }
    }
}
