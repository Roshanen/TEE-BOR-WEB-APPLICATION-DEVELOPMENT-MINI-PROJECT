using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Rating
{
    public ObjectId Id { get; set; }
    public ObjectId Event { get; set; }
    public int? Score { get; set; }
}
