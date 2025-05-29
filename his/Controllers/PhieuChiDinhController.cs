using Amazon.Runtime;
using his.Models;
using his.Models.Dto;
using his.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace his.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuChiDinhController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPhieuChiDinhService _service;
        private readonly ISettingService _settingService;

        public PhieuChiDinhController(IHttpClientFactory httpClientFactory, IPhieuChiDinhService service, ISettingService settingService)
        {
            _httpClientFactory = httpClientFactory;
            _service = service;
            _settingService = settingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PhieuChiDinhDto model)
        {
            var phieu = new PhieuChiDinhCls
            {
                BenhNhanId = model.BenhNhanId,
                NgayChiDinh = DateTime.Now,
                DichVu = model.DichVu.Select(x => new DichVuChiDinh
                {
                    Ma = x.Ma,
                    Ten = x.Ten,
                    SoLuong = x.SoLuong,
                    DonGia = x.DonGia,
                    GhiChu = x.GhiChu
                }).ToList()
            };

            await _service.CreateAsync(phieu);

            var document = new EmrDocumentDto
            {
                FK_MEEmrNo = model.AdmissionCode,
                FK_METemplateNo = "",
                MEEmrDocumentCode = "",
                Old_MEEmrDocumentCode = "",
                MEEmrDocumentDesc = "",
                FK_HRDepartmentNo = "",
                Data = new Newtonsoft.Json.Linq.JObject
                {
                    { "MaPhieu", phieu.Id },
                    { "MaBenhNhan", phieu.BenhNhanId },
                    { "NgayChiDinh", phieu.NgayChiDinh.ToString("yyyy-MM-dd") },
                    { "DichVu", Newtonsoft.Json.Linq.JArray.FromObject(phieu.DichVu) }
                },
                AACreatedUser = "system",
                Old_MEEmrDocumentID = null,
                IsBackgroundInit = true,
                MEEmrDocumentRefNo = phieu.Id,
                MEEmrDocumentCreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            #region CALL EMR API
            try
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Document(document);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("API error: " + ex.Message);
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            #endregion

            return Ok();
        }

        public async Task Document(EmrDocumentDto document)
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
                    var url = $"{urlSetting.Value}/api/services/vendor/his/v2/document";
                    string json = System.Text.Json.JsonSerializer.Serialize(document);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + error);
                    }
                }
            }
        }
    }
}
