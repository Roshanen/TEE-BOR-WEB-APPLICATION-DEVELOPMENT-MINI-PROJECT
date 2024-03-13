using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;
using System.ComponentModel.DataAnnotations;

public class Rating
{
    public ObjectId Id { get; set; }
    public ObjectId EventId { get; set; }
    public ObjectId UserId { get; set; }
    public DateTime LastModifiedDate { get; set; }
    [Range(1, 5)]
    public int Score { get; set; }
    [StringLength(248, MinimumLength = 16)]
    public String Comment { get; set; }

    public Rating()
    {
        Id = new ObjectId();
        EventId = new ObjectId();
        UserId = new ObjectId();
        LastModifiedDate = DateTime.Now;
        Score = 0;
        Comment = string.Empty;
    }
}
