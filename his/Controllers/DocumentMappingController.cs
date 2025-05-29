using ClosedXML.Excel;
using his.Models;
using his.Services;
using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class DocumentMappingController : Controller
    {
        private readonly IDocumentMappingService _service;

        public DocumentMappingController(IDocumentMappingService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var mappings = await _service.GetAllAsync();
            return View(mappings);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DocumentMappingModel mapping)
        {
            if (ModelState.IsValid)
            {
                var existing = await _service.GetByCodeHISAsync(mapping.CodeHIS);
                if (existing != null)
                {
                    ModelState.AddModelError("CodeHIS", "Mã HIS đã tồn tại.");
                    return View(mapping);
                }

                await _service.CreateAsync(mapping);
                return RedirectToAction("Index");
            }
            return View(mapping);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var mapping = await _service.GetByIdAsync(id);
            return mapping == null ? NotFound() : View(mapping);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, DocumentMappingModel mapping)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, mapping);
                return RedirectToAction("Index");
            }
            return View(mapping);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var mapping = await _service.GetByIdAsync(id);
            return mapping == null ? NotFound() : View(mapping);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ImportExcel() => View();

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Vui lòng chọn file Excel.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var codeHIS = row.Cell(1).GetString();
                var existing = await _service.GetByCodeHISAsync(codeHIS);
                if (existing != null) continue; // bỏ qua mã trùng

                var mapping = new DocumentMappingModel
                {
                    CodeHIS = codeHIS,
                    NameHIS = row.Cell(2).GetString(),
                    CodeEMR = row.Cell(3).GetString(),
                    NameEMR = row.Cell(4).GetString(),
                    Type = row.Cell(5).GetString(),
                    IsActive = row.Cell(6).GetBoolean()
                };
                await _service.CreateAsync(mapping);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            var data = await _service.GetAllAsync();
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Mapping");
            worksheet.Cell(1, 1).Value = "HIS Code";
            worksheet.Cell(1, 2).Value = "HIS Name";
            worksheet.Cell(1, 3).Value = "EMR Code";
            worksheet.Cell(1, 4).Value = "EMR Name";
            worksheet.Cell(1, 5).Value = "Loại";
            worksheet.Cell(1, 6).Value = "Active";

            for (int i = 0; i < data.Count; i++)
            {
                var row = i + 2;
                worksheet.Cell(row, 1).Value = data[i].CodeHIS;
                worksheet.Cell(row, 2).Value = data[i].NameHIS;
                worksheet.Cell(row, 3).Value = data[i].CodeEMR;
                worksheet.Cell(row, 4).Value = data[i].NameEMR;
                worksheet.Cell(row, 5).Value = data[i].Type;
                worksheet.Cell(row, 6).Value = data[i].IsActive;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DocumentMappingModel.xlsx");
        }
    }
}
