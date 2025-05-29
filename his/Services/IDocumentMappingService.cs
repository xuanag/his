using his.Models;

namespace his.Services
{
    public interface IDocumentMappingService : IMongoService<DocumentMappingModel>
    {
        Task<DocumentMappingModel> GetByCodeHISAsync(string codeHIS);
        Task<DocumentMappingModel> GetByCodeEMRAsync(string codeEMR);
    }
}
