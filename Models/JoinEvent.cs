using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class JoinEvent
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public ObjectId EventId { get; set; }
    public int BringFriends { get; set; }
    public DateTime JoinDate { get; set; }
}
