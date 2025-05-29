using his.Models;

namespace his.Services
{
    public interface ICLSService: IMongoService<CanLamSang>
    {
        Task<List<CanLamSang>> SearchDichVuClsAsync(string keyword);
        Task<CanLamSang> GetByCodeAsync(string code);
        Task SeedAsync();
    }
}
