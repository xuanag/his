using his.Models;

namespace his.Services
{
    public interface IPatientService: IMongoService<PatientInfo>
    {
        Task<List<PatientInfo>> GetAllAsync(string keyword = "");
    }
}
