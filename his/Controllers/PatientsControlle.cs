using his.Models;
using his.Services;
using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IAdmissionService _admissionService;

        public PatientsController(IPatientService patientService, IAdmissionService admissionService)
        {
            _patientService = patientService;
            _admissionService = admissionService;
        }

        public async Task<IActionResult> Index(string search = "")
        {
            var patients = await _patientService.GetAllAsync(search);
            ViewBag.Search = search;
            return View(patients);
        }

        public IActionResult Create() => View(new PatientInfo());

        [HttpPost]
        public async Task<IActionResult> Create(PatientInfo model)
        {
            if (!ModelState.IsValid) return View(model);

            await _patientService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();

            var admissions = await _admissionService.GetByPatientIdAsync(id);
            ViewBag.Admissions = admissions;
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmission(string patientId, Admission admission)
        {
            admission.PatientId = patientId;
            await _admissionService.CreateAsync(admission);
            return RedirectToAction("Details", new { id = patientId });
        }
    }
}
