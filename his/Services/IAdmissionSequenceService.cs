using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public interface IAdmissionSequenceService : IMongoService<AdmissionSequence>
    {
        Task<string> GenerateAdmissionCodeAsync(string departmentCode);
    }
}
