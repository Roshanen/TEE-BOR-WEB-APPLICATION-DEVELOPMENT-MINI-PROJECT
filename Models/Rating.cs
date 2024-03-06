using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Rating
{
    public ObjectId Id { get; set; }
    public ObjectId EventId { get; set; }
    public ObjectId UserId { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int? Score { get; set; }
}
