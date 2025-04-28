using Amazon.Runtime.Internal.Util;
using his.Helper;
using his.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace his.Services
{
    public class UserService : MongoService<User>, IUserService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10); // Adjust the cache duration as needed

        public UserService(IMongoDatabase database,
            IMemoryCache cache) : base(database, "Users")
        {
            _cache = cache;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _collection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null) return null;

            var passwordHash = Helpers.ComputeSha256Hash(password);
            if (user.PasswordHash != passwordHash) return null;

            return user;
        }
    }
}
