using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace his.Services
{
    public interface ISequenceService : IMongoService<SequenceModel>
    {
        Task<int> GetNextSequenceAsync(string key, int year);
    }
}
