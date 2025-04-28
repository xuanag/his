using his.Models;
using his.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Patient>>> Get() =>
        await _patientService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> Get(string id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient == null) return NotFound();
        return patient;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Patient patient)
    {
        await _patientService.CreateAsync(patient);
        return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Patient updatedPatient)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null) return NotFound();

        updatedPatient.Id = patient.Id;
        await _patientService.UpdateAsync(id, updatedPatient);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient == null) return NotFound();

        await _patientService.DeleteAsync(id);
        return NoContent();
    }
}
