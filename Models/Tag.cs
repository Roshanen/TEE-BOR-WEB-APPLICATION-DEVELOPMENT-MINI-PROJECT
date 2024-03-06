using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Rating
{
    public ObjectId Id { get; set; }
    public string? TagName { get; set; }
}
