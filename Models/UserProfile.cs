using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string JoinDate { get; set; } = DateTime.Now.ToString();
    }
}
