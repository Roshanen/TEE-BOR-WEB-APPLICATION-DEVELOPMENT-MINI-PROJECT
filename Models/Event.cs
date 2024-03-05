using MongoDB.Bson;
using MongoDB.Driver;
namespace WebApp.Models;

public class Event
{
    public ObjectId Id { get; set; }
    public ObjectId PlaceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string Name { get; set; }
    public string Place { get; set; }
    public string Description { get; set; }
    public int MaxMember { get; set; }
    public int CurrentMember { get; set; }
    private IMongoCollection<Event> _collections;
    
    public Event(){
        var context = new MongoContext();
        _collections = context.GetCollection<Event>("events");
    }
}
