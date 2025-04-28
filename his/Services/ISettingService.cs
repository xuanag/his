using his.Models;

namespace his.Services
{
    public interface ISettingService: IMongoService<Setting>
    {
        Task<Setting> GetByKey(string key);
    }
}
