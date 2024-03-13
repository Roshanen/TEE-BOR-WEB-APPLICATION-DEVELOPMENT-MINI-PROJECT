using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Models;

public class UserProfile
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
    public string Address { get; set; }
    public string Bio { get; set; }
    public string JoinDate { get; set; }

    public UserProfile()
    {
        Id = string.Empty;
        UserName = string.Empty;
        Email = string.Empty;
        ProfilePicture = string.Empty;
        Address = string.Empty;
        Bio = string.Empty;
        JoinDate = DateTime.Now.ToString();
    }
}

