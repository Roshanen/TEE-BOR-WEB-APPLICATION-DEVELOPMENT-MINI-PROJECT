using MongoDB.Bson;

namespace WebApp.Models;

public class User
{
    public ObjectId UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePicture { get; set; }
}
