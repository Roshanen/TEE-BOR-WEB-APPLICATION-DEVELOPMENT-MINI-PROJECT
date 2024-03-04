using MongoDB.Bson;

namespace WebApp.Models;

public class User
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Img { get; set; }
}
