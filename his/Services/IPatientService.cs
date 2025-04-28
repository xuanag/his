using his.Models;

namespace his.Services
{
    public interface IPatientService: IMongoService<Patient>
    {
        Task<List<Patient>> GetAllAsync(string keyword = "");
        Task<string> GeneratePatientCodeAsync(string refix);
    }
}
