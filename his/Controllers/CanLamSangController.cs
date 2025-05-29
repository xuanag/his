using his.Models;
using his.Models.Dto;
using his.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanLamSangController : ControllerBase
    {
        private readonly ICLSService _clsService;
        public CanLamSangController(ICLSService clsService)
        {
            _clsService = clsService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            var allItems = await _clsService.GetAllAsync(); // Await the Task to get the IEnumerable
            var results = allItems.Select(x => new { ma = x.Ma, ten = x.Ten, dongia = x.DonGia}).ToList(); // Use LINQ's Select instead of Project
            return Ok(results);
        }


        [HttpGet("detail/{ma}")]
        public async Task<IActionResult> Detail(string ma)
        {
            await _clsService.SeedAsync(); // Đảm bảo dữ liệu đã được seed
            var dichvu = await _clsService.GetByCodeAsync(ma);
            if (dichvu == null) return NotFound();
            return Ok(dichvu);
        }
    }
}
