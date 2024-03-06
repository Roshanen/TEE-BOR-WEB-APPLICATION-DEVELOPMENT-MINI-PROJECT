using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Category
{
    public ObjectId Id { get; set; }
    public string? TagName { get; set; }
}
