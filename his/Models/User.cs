using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace his.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

}
