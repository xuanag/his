using ClosedXML.Excel;
using his.Models;
using his.Services;
using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingService _service;

        public SettingController(ISettingService service)
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
        public async Task<IActionResult> Create(Setting model)
        {
            if (ModelState.IsValid)
            {
                //var existing = await _service.GetByCodeHISAsync(mapping.CodeHIS);
                //if (existing != null)
                //{
                //    ModelState.AddModelError("CodeHIS", "Mã HIS đã tồn tại.");
                //    return View(mapping);
                //}

                await _service.CreateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.GetByIdAsync(id);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Setting model)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var model = await _service.GetByIdAsync(id);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
