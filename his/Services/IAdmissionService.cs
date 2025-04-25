using his.Models;

namespace his.Services
{
    public interface IAdmissionService : IMongoService<Admission>
    {
        Task<List<Admission>> GetByPatientIdAsync(string patientId);
    }
}
