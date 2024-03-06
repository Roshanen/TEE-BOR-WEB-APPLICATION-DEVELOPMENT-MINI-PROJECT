using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class JoinEvent
{
    public ObjectId JoinId { get; set; }
    public string UserId { get; set; }
    public string EventId { get; set; }
    public DateTime JoinDate { get; set; }
}
